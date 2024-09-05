using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.SpecializationAggregate
{
    public class Specialization : EntityBase
    {
        public string Title { get; private set; }

        public Specialization(string title)
        {
            Title = title;
        }


    }
}
