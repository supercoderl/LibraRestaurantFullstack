using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Interfaces.Repositories;
using LibraRestaurant.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraRestaurant.Infrastructure.Background
{
    public class ReservationBackgroundService : BaseService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReservationBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                bool shutDownProcess = false;

                /*
                 *retrive the staus from DB and dynamically start or stop this service without redeploying it
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var accountContext = scope.ServiceProvider.GetRequiredService<EssentialProductsDbContext>();
                    var shutDownProcessSetting = await accountContext.AppSettings.SingleOrDefaultAsync(s => s.AppSettingKey.Equals("ProductBGService"));
                    shutDownProcess = (shutDownProcessSetting != null && Convert.ToBoolean(shutDownProcessSetting.AppSettingValue));
                }

                */

                if (!shutDownProcess)
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

                        var reservations = await reservationRepository.GetByStatusAsync(Domain.Enums.ReservationStatus.Cleaning);

                        if(reservations.Any())
                        {
                            await CheckReservationStatusAsync(reservations, reservationRepository, dbContext, stoppingToken);
                        }
                    }
                }


                await Task.Delay(5000, stoppingToken);
            }
        }

        private bool ShouldBeUpdatedToAvailable(DateTime? cleaningTime)
        {
            return cleaningTime.HasValue && (DateTime.Now - cleaningTime.Value).TotalMinutes > 5;
        }

        private async Task CheckReservationStatusAsync(
            List<Reservation> reservations, 
            IReservationRepository reservationRepository, 
            ApplicationDbContext context,
            CancellationToken stoppingToken
        )
        {
            bool anyUpdates = false;

            foreach (var reservation in reservations)
            {
                if (ShouldBeUpdatedToAvailable(reservation.CleaningTime))
                {
                    reservation.SetStatus(Domain.Enums.ReservationStatus.Available);
                    reservation.SetCleaningTime(null);
                    reservationRepository.Update(reservation);
                    anyUpdates = true;
                }
            }

            if (anyUpdates)
            {
                await context.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
