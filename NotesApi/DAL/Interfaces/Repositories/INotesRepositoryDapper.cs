using Lesson1.DAL.Models;
using NotesApi.DAL.Interfaces.Base;

namespace NotesApi.DAL.Interfaces.Repositories
{
    public interface INotesRepositoryDapper : IRepository<Note>
    {
    }
}
