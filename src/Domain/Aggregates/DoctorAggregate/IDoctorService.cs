using SmCenterTestApp.Domain.Aggregates.DoctorAggregate.DTO;
using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.DoctorAggregate
{
    public interface IDoctorService : IValidationStateService
    {
        public Task<DoctorResponse?> GetDoctorForChangeAsync(long id);

        public Task<IEnumerable<DoctorSimpleResponse>?> GetDoctorsAsync(int pageNumber, int pageSize, string? fieldSortBy);

        public Task CreateAsync(string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId);

        public Task UpdateAsync(long id, string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId);

        public Task DeleteAsync(long id);
    }
}
