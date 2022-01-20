using DebitCardApi.DAL.Interfaces.Base;

namespace DebitCardApi.DAL.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
