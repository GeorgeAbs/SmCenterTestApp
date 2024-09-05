using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmCenterTestApp.Application.Services;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate;
using SmCenterTestApp.Domain.Aggregates.PatientAggregate;
using SmCenterTestApp.Infrastructure;

namespace SmCenterTestApp.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));

            builder.Services.AddScoped(typeof(IDoctorService), typeof(DoctorService));
            builder.Services.AddScoped(typeof(IPatientService), typeof(PatientService));

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(o =>
                {
                    o.InvalidModelStateResponseFactory = _ =>
                    {
                        var newDetails = new ValidationProblemDetails()
                        {
                            Status = StatusCodes.Status400BadRequest
                        };

                        return new BadRequestObjectResult(newDetails);
                    };
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => c.EnableAnnotations());

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                appDbContext.Database.EnsureDeleted();
                appDbContext.Database.Migrate();
            }

                if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    var seed = new AppDbSeed();

                    await seed.SeedAsync(appDbContext);
                }

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.MapControllers();

            app.Run();
        }
    }
}
