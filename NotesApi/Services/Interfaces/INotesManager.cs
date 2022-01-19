using NotesApi.DAL.Interfaces.Base;
using NotesApi.DAL.Interfaces.Repositories;

namespace NotesApi.Services.Interfaces
{
    public interface INotesManager<T> where T : class, IEntity
    {
        IRepository<T> NotesEf { get; }
        IRepository<T> NotesDapper { get; }
    }
}
