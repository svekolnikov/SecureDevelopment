using Lesson1.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson1.DAL.DataContext
{
    public class NotesDb : DbContext
    {
        public DbSet<Note> Notes { get; set; }
    }
}
