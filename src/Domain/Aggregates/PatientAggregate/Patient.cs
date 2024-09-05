using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.PatientAggregate
{
    public class Patient : EntityBase
    {
        public string FirstName { get; private set; }

        public string MiddleName { get; private set; }

        public string LastName { get; private set; }

        public string Address { get; private set; }

        public DateOnly BirthDate { get; private set; }

        public string Sex { get; private set; }

        public long AreaId { get; private set; }

        public Patient(string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, long areaId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            BirthDate = birthDate;
            Sex = sex;
            AreaId = areaId;
        }

        public void SetInfo(string firstName, string middleName, string lastName, string address, DateOnly birthDate, string sex, long areaId)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            BirthDate = birthDate;
            Sex = sex;
            AreaId = areaId;
        }
    }
}
