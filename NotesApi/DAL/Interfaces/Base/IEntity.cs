namespace NotesApi.DAL.Interfaces.Base
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
