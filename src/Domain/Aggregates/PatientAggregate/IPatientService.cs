using SmCenterTestApp.Domain.Aggregates.PatientAggregate.DTO;
using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.PatientAggregate
{
    public interface IPatientService : IValidationStateService
    {
        public Task<PatientResponse?> GetPatientForChangeAsync(long id);

        public Task<IEnumerable<PatientSimpleResponse>?> GetPatientsAsync(int pageNumber, int pageSize, string? fieldSortBy);

        public Task CreateAsync(string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, long areaId);

        public Task UpdateAsync(long id, string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, long areaId);

        public Task DeleteAsync(long id);
    }
}
