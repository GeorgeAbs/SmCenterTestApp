namespace SmCenterTestApp.Domain.Aggregates.DoctorAggregate.DTO
{
    public record DoctorResponse
    {
        public long Id { get; private set; }

        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public long RoomId { get; private set; }

        public long SpecializationId { get; private set; }

        public long AreaId { get; private set; }

        public DoctorResponse(long id, string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            RoomId = roomId;
            SpecializationId = specializationId;
            AreaId = areaId;
        }
    }
}
