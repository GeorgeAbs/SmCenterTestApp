using SmCenterTestApp.Domain.Common;

namespace SmCenterTestApp.Domain.Aggregates.RoomAggregate
{
    public class Room : EntityBase
    {
        public string Number { get; private set; }

        public Room(string number)
        {
            Number = number;
        }
    }
}
