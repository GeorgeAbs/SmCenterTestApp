using Microsoft.EntityFrameworkCore;
using SmCenterTestApp.Application.Common;
using SmCenterTestApp.Domain.Aggregates.AreaAggregate;
using SmCenterTestApp.Domain.Aggregates.RoomAggregate;
using SmCenterTestApp.Domain.Aggregates.SpecializationAggregate;
using SmCenterTestApp.Infrastructure;

namespace SmCenterTestApp.Application.Services
{
    public class AppDbSeed : IDbSeed<AppDbContext>
    {
        public AppDbSeed() { }

        public async Task SeedAsync(AppDbContext context)
        {
            if (!await context.Areas.AnyAsync() &&
                !await context.Rooms.AnyAsync() &&
                !await context.Specializations.AnyAsync())
            {
                List<Area> areas = [];
                List<Room> rooms = [];
                List<Specialization> specializations = [];

                for (int i = 1; i < 51; i++)
                {
                    areas.Add(new Area($"Участок {i}"));
                    rooms.Add(new Room($"Кабинет {i}"));
                    specializations.Add(new Specialization($"Специализация {i}"));
                }

                context.Areas.AddRange(areas);
                context.Rooms.AddRange(rooms);
                context.Specializations.AddRange(specializations);

                int max = 50;
                for (int i = 0; i < 50; i++)
                {
                    int currentInverted = 50 - i;
                    context.Doctors.Add(new($"first {i}", $"middle {currentInverted}", $"last {currentInverted}", currentInverted, currentInverted, currentInverted));
                }
                    

                await context.SaveChangesAsync();
            }
        }
    }
}
