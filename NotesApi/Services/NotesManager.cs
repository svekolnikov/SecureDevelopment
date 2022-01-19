using Lesson1.DAL.Models;
using NotesApi.DAL.Interfaces.Base;
using NotesApi.DAL.Interfaces.Repositories;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services
{
    public class NotesManager<T> : INotesManager<T> where T : class,IEntity

    {
    public NotesManager(IRepository<T> repositoryEf, IRepository<T> repositoryDapper)
    {
        NotesEf = repositoryEf;
        NotesDapper = repositoryDapper;
    }

    public IRepository<T> NotesEf { get; }
    public IRepository<T> NotesDapper { get; }
    }
}
