using NotesApi.DAL.Interfaces.Base;
using NotesApi.DAL.Interfaces.Repositories;

namespace NotesApi.Services.Interfaces
{
    public interface INotesManager<T> where T : class, IEntity
    {
        IRepositoryEf<T> NotesEf { get; }
        INotesRepositoryDapper NotesDapper { get; }
    }
}
