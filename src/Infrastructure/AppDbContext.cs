using Microsoft.EntityFrameworkCore;
using SmCenterTestApp.Domain.Aggregates.AreaAggregate;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate;
using SmCenterTestApp.Domain.Aggregates.PatientAggregate;
using SmCenterTestApp.Domain.Aggregates.RoomAggregate;
using SmCenterTestApp.Domain.Aggregates.SpecializationAggregate;

namespace SmCenterTestApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext() { }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Area> Areas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("test");

            /*builder.Entity<Doctor>()
                .HasIndex(x => new { x.FirstName,x.MiddleName, x.LastName })
                .IsUnique();*/

            builder.Entity<Room>()
                .HasIndex(x => x.Number)
                .IsUnique();

            builder.Entity<Specialization>()
                .HasIndex(x => x.Title)
                .IsUnique();

            builder.Entity<Area>()
                .HasIndex(x => x.Number)
                .IsUnique();

        }
    }
}
