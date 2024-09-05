using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.AreaAggregate
{
    public class Area : EntityBase
    {
        public string Number { get; private set; }

        public Area (string number)
        {
            Number = number;
        }
    }
}
