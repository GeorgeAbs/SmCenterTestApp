using System.ComponentModel.DataAnnotations;

namespace SmCenterTestApp.Domain.Common
{
    public abstract class EntityBase
    {
        [Key]
        public long Id { get; private set; }
    }
}
