namespace SmCenterTestApp.API.DTO
{
    public record DoctorUpdateRequest
    {
        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public long RoomId { get; private set; }

        public long SpecializationId { get; private set; }

        public long AreaId { get; private set; }

        public DoctorUpdateRequest(string firstName, string middleName, string lastName, long roomId, long specializationId, long areaId)
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
