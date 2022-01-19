using NotesApi.DAL.Interfaces.Base;
using NotesApi.DAL.Interfaces.Repositories;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services
{
    public class NotesManager<T> : INotesManager<T> where T : class,IEntity

    {
    public NotesManager(IRepositoryEf<T> repositoryEf, INotesRepositoryDapper notesRepositoryDapper)
    {
        NotesEf = repositoryEf;
        NotesDapper = notesRepositoryDapper;
    }

    public IRepositoryEf<T> NotesEf { get; }
    public INotesRepositoryDapper NotesDapper { get; }
    }
}
