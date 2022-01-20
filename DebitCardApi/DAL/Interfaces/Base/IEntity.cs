namespace DebitCardApi.DAL.Interfaces.Base
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
