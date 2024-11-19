using LibraRestaurant.Api.BackgroundServices;
using LibraRestaurant.Api.Extensions;
using LibraRestaurant.Application.Extensions;
using LibraRestaurant.Application.gRPC;
using LibraRestaurant.Domain.Extensions;
using LibraRestaurant.Domain.Rabbitmq.Extensions;
using LibraRestaurant.Infrastructure.Database;
using LibraRestaurant.Infrastructure.Extensions;
using HealthChecks.ApplicationStatus.DependencyInjection;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;
using LibraRestaurant.Application.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>()
    .AddApplicationStatus();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

if (builder.Environment.IsProduction())
{
    var rabbitHost = builder.Configuration["RabbitMQ:Host"];
    var rabbitPort = builder.Configuration["RabbitMQ:Port"];
    var rabbitUser = builder.Configuration["RabbitMQ:Username"];
    var rabbitPass = builder.Configuration["RabbitMQ:Password"];

    builder.Services
        .AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!)
        .AddRedis(builder.Configuration["RedisHostName"]!, "Redis")
        .AddRabbitMQ(
            $"amqps://{rabbitUser}:{rabbitPass}@{rabbitHost}/{rabbitUser}",
            name: "RabbitMQ");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseLazyLoadingProxies();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("LibraRestaurant.Infrastructure"));
});

builder.Services.AddCors(builder =>
{
    builder.AddPolicy("policy", x => 
        x.SetIsOriginAllowed(x => _ = true)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials()
    );
});

builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration, "LibraRestaurant.Infrastructure");
builder.Services.AddQueryHandlers();
builder.Services.AddServices();
builder.Services.AddSortProviders();
builder.Services.AddCommandHandlers();
builder.Services.AddNotificationHandlers();
builder.Services.AddApiEmployee();
builder.Services.AddSignalR();

builder.Services.AddRabbitMqHandler(builder.Configuration, "RabbitMQ");

builder.Services.AddHostedService<SetInactiveEmployeesService>();

builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly); });

builder.Services.AddLogging(x => x.AddSimpleConsole(console =>
{
    console.TimestampFormat = "[yyyy-MM-ddTHH:mm:ss.fff] ";
    console.IncludeScopes = true;
}));

if (builder.Environment.IsProduction() || !string.IsNullOrWhiteSpace(builder.Configuration["RedisHostName"]))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration["RedisHostName"];
        options.InstanceName = "LibraRestaurant";
    });
}
else
{
    builder.Services.AddDistributedMemoryCache();
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var appDbContext = services.GetRequiredService<ApplicationDbContext>();
    var storeDbContext = services.GetRequiredService<EventStoreDbContext>();
    var domainStoreDbContext = services.GetRequiredService<DomainNotificationStoreDbContext>();

    appDbContext.EnsureMigrationsApplied();
    storeDbContext.EnsureMigrationsApplied();
    domainStoreDbContext.EnsureMigrationsApplied();
}

/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGrpcReflectionService();
}*/

app.UseSwagger();
app.UseSwaggerUI();
app.MapGrpcReflectionService();

app.UseCors("policy");
/*app.UseHttpsRedirection();*/

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapControllers();
app.MapGrpcService<EmployeesApiImplementation>();
app.MapHub<TrackerHub>("/tracker");

app.Run();

// Needed for integration tests web application factory
public partial class Program
{
}