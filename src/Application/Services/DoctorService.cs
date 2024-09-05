using Microsoft.EntityFrameworkCore;
using SmCenterTestApp.Domain.Aggregates.AreaAggregate;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate;
using SmCenterTestApp.Domain.Aggregates.DoctorAggregate.DTO;
using SmCenterTestApp.Domain.Aggregates.RoomAggregate;
using SmCenterTestApp.Domain.Aggregates.SpecializationAggregate;
using SmCenterTestApp.Domain.Common;
using SmCenterTestApp.Infrastructure;

namespace SmCenterTestApp.Application.Services
{
    public class DoctorService : ServiceBase, IDoctorService
    {
        private readonly AppDbContext _context;
        public DoctorService(AppDbContext appDbContext) => _context = appDbContext;

        public async Task<DoctorResponse?> GetDoctorForChangeAsync(long id)
        {
            var doctor = await _context.Doctors
                .Where(x => x.Id == id)
                .Select(x => new DoctorResponse(x.Id, x.FirstName, x.MiddleName, x.LastName, x.RoomId, x.SpecializationId, x.AreaId))
                .FirstOrDefaultAsync();

            if (doctor is null)
            {
                ValidationState.AddError("User error");
                return null;
            }

            return doctor;
        }

        public async Task<IEnumerable<DoctorSimpleResponse>?> GetDoctorsAsync(int pageNumber = 1, int pageSize = 20, string? fieldSortBy = "")
        {
            if (pageNumber < 1) pageNumber = 1;

            if (pageSize < 1) pageSize = 20;



            var doctorsQuery = (from d in _context.Doctors
                                   join r in _context.Rooms on d.RoomId equals r.Id
                                   join a in _context.Areas on d.AreaId equals a.Id
                                   join s in _context.Specializations on d.SpecializationId equals s.Id
                                   select new {
                                       id = d.Id,
                                       firstName =  d.FirstName,
                                       middleName =  d.MiddleName,
                                       lastName = d.LastName,
                                       roomNumber = r.Number,
                                       title =  s.Title,
                                       areaNumber = a.Number}
                                   );
            

            doctorsQuery = fieldSortBy switch
            {
                "first_name" => doctorsQuery.OrderBy(x => x.firstName),
                "middle_name" => doctorsQuery.OrderBy(x => x.middleName),
                "last_name" => doctorsQuery.OrderBy(x => x.lastName),
                "room" => doctorsQuery.OrderBy(x => x.roomNumber),
                "area" => doctorsQuery.OrderBy(x => x.areaNumber),
                "specialization" => doctorsQuery.OrderBy(x => x.title),
                _ => doctorsQuery.OrderBy(x => x.firstName)
            };

            var doctorsResponse = await doctorsQuery
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Select(x => new DoctorSimpleResponse(x.id, x.firstName, x.middleName, x.lastName,x.roomNumber, x.title, x.areaNumber))
                                  .ToListAsync();

            return doctorsResponse;
        }

        public async Task CreateAsync(string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId)
        {
            if (string.IsNullOrEmpty(firstName)) ValidationState.AddError("First name must be specified");
            if (string.IsNullOrEmpty(middleName)) ValidationState.AddError("Middle name must be specified");
            if (string.IsNullOrEmpty(lastName)) ValidationState.AddError("Last name must be specified");

            var room = await _context.FindAsync<Room>(roomId);
            var area = await _context.FindAsync<Area>(areaId);
            var specialization = await _context.FindAsync<Specialization>(specializationId);

            if (room is null) ValidationState.AddError("Room is not found");
            if (area is null) ValidationState.AddError("Area is not found");
            if (specialization is null) ValidationState.AddError("Specialization is not found");

            if (!ValidationState.IsValid) return;

            var doctor = new Doctor(firstName, middleName, lastName, roomId, specializationId, areaId);

            try
            {
                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
            }
            catch 
            {
                ValidationState.AddError("Doctor creation error");
            }
        }

        public async Task UpdateAsync(long id, string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId)
        {
            var doctor = await _context.FindAsync<Doctor>(id);
            var room = await _context.FindAsync<Room>(roomId);
            var area = await _context.FindAsync<Area>(areaId);
            var specialization = await _context.FindAsync<Specialization>(specializationId);

            if (doctor is null) ValidationState.AddError("Doctor update error");
            if (room is null) ValidationState.AddError("Room is not found");
            if (area is null) ValidationState.AddError("Area is not found");
            if (specialization is null) ValidationState.AddError("Specialization is not found");

            if (string.IsNullOrEmpty(firstName)) ValidationState.AddError("First name must be specified");
            if (string.IsNullOrEmpty(middleName)) ValidationState.AddError("Middle name must be specified");
            if (string.IsNullOrEmpty(lastName)) ValidationState.AddError("Last name must be specified");

            if (!ValidationState.IsValid) return;

            doctor!.SetInfo(firstName, middleName, lastName, roomId, specializationId, areaId);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ValidationState.AddError("Doctor update error");
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                var affectedRows = await _context.Doctors
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();

                if (affectedRows == 0) ValidationState.AddError("Doctor delete error");
            }
            catch
            {
                ValidationState.AddError("Doctor delete error");
            }
        }
    }
}
