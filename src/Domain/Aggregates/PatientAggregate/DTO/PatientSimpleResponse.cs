namespace SmCenterTestApp.Domain.Aggregates.PatientAggregate.DTO
{
    public record PatientSimpleResponse
    {
        public long Id { get; private set; }

        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public string Address { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public string Sex { get; private set; }

        public string Area { get; private set; }

        public PatientSimpleResponse(long id, string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, string area)
        {
            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            BirthDate = birthDate;
            Sex = sex;
            Area = area;
        }
    }
}
