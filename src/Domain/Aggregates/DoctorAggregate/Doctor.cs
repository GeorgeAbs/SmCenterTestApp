using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.DoctorAggregate
{
    public class Doctor : EntityBase
    {
        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public long RoomId { get; private set; }
        
        public long SpecializationId { get; private set; }

        public long AreaId { get; private set; }

        public Doctor(string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            RoomId = roomId;
            SpecializationId = specializationId;
            AreaId = areaId;
        }

        public void SetInfo(string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            RoomId = roomId;
            SpecializationId = specializationId;
            AreaId = areaId;
        }
    }
}
