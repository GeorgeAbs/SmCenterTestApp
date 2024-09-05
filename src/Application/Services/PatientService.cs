using Microsoft.EntityFrameworkCore;
using SmCenterTestApp.Domain.Aggregates.AreaAggregate;
using SmCenterTestApp.Domain.Aggregates.PatientAggregate;
using SmCenterTestApp.Domain.Aggregates.PatientAggregate.DTO;
using SmCenterTestApp.Domain.Common;
using SmCenterTestApp.Infrastructure;

namespace SmCenterTestApp.Application.Services
{
    public class PatientService : ServiceBase, IPatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context) => _context = context;

        public async Task<PatientResponse?> GetPatientForChangeAsync(long id)
        {
            var patient = await _context.Patients
                .Where(x => x.Id == id)
                .Select(x => new PatientResponse(x.Id, x.FirstName, x.MiddleName, x.LastName, x.Address, x.BirthDate,x.Sex, x.AreaId))
                .FirstOrDefaultAsync();

            if (patient is null)
            {
                ValidationState.AddError("Patient error");
                return null;
            }

            return patient;
        }

        public async Task<IEnumerable<PatientSimpleResponse>?> GetPatientsAsync(int pageNumber = 1, int pageSize = 20, string? fieldSortBy = "")
        {
            if (pageNumber < 1) pageNumber = 1;

            if (pageSize < 1) pageSize = 20;

            var patientsQuery = (from p in _context.Patients
                                join a in _context.Areas on p.AreaId equals a.Id
                                select new { 
                                    id = p.Id,
                                    firstName = p.FirstName,
                                    middlename =  p.MiddleName,
                                    lastName =  p.LastName,
                                    address = p.Address,
                                    birthdate = p.BirthDate,
                                    sex = p.Sex,
                                    areaNumber = a.Number });

            patientsQuery = fieldSortBy switch
            {
                "first_name" => patientsQuery.OrderBy(x => x.firstName),
                "middle_name" => patientsQuery.OrderBy(x => x.middlename),
                "last_name" => patientsQuery.OrderBy(x => x.lastName),
                "address" => patientsQuery.OrderBy(x => x.address),
                "birthdate" => patientsQuery.OrderBy(x => x.birthdate),
                "sex" => patientsQuery.OrderBy(x => x.sex),
                "area" => patientsQuery.OrderBy(x => x.areaNumber),
                _ => patientsQuery
            };

            var patientsResponse = await patientsQuery
                                  .Skip((pageNumber - 1) * pageSize)
                                  .Take(pageSize)
                                  .Select(x => new PatientSimpleResponse(x.id,x.firstName,x.middlename,x.lastName,x.address,x.birthdate,x.sex,x.areaNumber))
                                  .ToListAsync();

            return patientsResponse;
        }

        public async Task CreateAsync(string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, long areaId)
        {
            var area = await _context.FindAsync<Area>(areaId);

            if (area is null) ValidationState.AddError("Area is not found");

            if (string.IsNullOrEmpty(firstName)) ValidationState.AddError("First name must be specified");
            if (string.IsNullOrEmpty(middleName)) ValidationState.AddError("Middle name must be specified");
            if (string.IsNullOrEmpty(lastName)) ValidationState.AddError("Last name must be specified");
            if (string.IsNullOrEmpty(address)) ValidationState.AddError("Address must be specified");
            if (string.IsNullOrEmpty(sex)) ValidationState.AddError("Sex must be specified");
            if (birthDate.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) >= 0) ValidationState.AddError("Birthdate must be earlier then now");


            if (!ValidationState.IsValid) return;

            var patient = new Patient(firstName, middleName, lastName, address, birthDate, sex, areaId);

            try
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }
            catch
            {
                ValidationState.AddError("Patient creation error");
            }
        }

        public async Task UpdateAsync(long id, string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, long areaId)
        {
            if (string.IsNullOrEmpty(firstName)) ValidationState.AddError("First name must be specified");
            if (string.IsNullOrEmpty(middleName)) ValidationState.AddError("Middle name must be specified");
            if (string.IsNullOrEmpty(lastName)) ValidationState.AddError("Last name must be specified");
            if (string.IsNullOrEmpty(address)) ValidationState.AddError("Address must be specified");
            if (string.IsNullOrEmpty(sex)) ValidationState.AddError("Sex must be specified");
            if (birthDate.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) >= 0) ValidationState.AddError("Birthdate must be earlier then now");

            var patient = await _context.FindAsync<Patient>(id);
            var area = await _context.FindAsync<Area>(areaId);

            if (patient is null) ValidationState.AddError("Patient update error");
            if (area is null) ValidationState.AddError("Area is not found");

            

            if (!ValidationState.IsValid) return;

            patient!.SetInfo(firstName, middleName, lastName, address, birthDate, sex, areaId);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ValidationState.AddError("Patient update error");
            }
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                var affectedRows = await _context.Patients
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();

                if (affectedRows == 0) ValidationState.AddError("Patient delete error");
            }
            catch
            {
                ValidationState.AddError("Patient delete error");
            }
        }
    }
}
