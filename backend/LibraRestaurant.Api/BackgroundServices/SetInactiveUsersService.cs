using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using LibraRestaurant.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LibraRestaurant.Api.BackgroundServices;

public sealed class SetInactiveEmployeesService : BackgroundService
{
    private readonly ILogger<SetInactiveEmployeesService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SetInactiveEmployeesService(
        IServiceProvider serviceProvider,
        ILogger<SetInactiveEmployeesService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            IList<Employee> inactiveEmployees = Array.Empty<Employee>();

            try
            {
                var cutoffDate = DateTimeOffset.UtcNow.AddDays(-30);

                inactiveEmployees = await context.Employees
                    .Where(employee =>
                        employee.LastLoggedinDate < cutoffDate &&
                        employee.Status == UserStatus.Active)
                    .Take(250)
                    .ToListAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving employees to set inactive");
            }

            foreach (var employee in inactiveEmployees)
            {
                employee.SetStatus(UserStatus.Inactive);
            }

            try
            {
                await context.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while setting employees to inactive");
            }

            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}