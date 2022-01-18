using Lesson1.DAL.Interfaces.Base;

namespace Lesson1.DAL.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
