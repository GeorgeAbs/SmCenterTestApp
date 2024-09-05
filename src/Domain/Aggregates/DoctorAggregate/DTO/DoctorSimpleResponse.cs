namespace SmCenterTestApp.Domain.Aggregates.DoctorAggregate.DTO
{
    public record DoctorSimpleResponse
    {
        public long Id { get; private set; }
        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public string Room { get; private set; }

        public string Specialization { get; private set; }

        public string Area { get; private set; }

        public DoctorSimpleResponse(long id, string firstName, string middleName, string lastName, string room, string specialization, string area)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Room = room;
            Specialization = specialization;
            Area = area;
        }
    }
}
