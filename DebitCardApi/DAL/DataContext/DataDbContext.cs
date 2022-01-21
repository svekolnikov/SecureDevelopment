using DebitCardApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DebitCardApi.DAL.DataContext
{
    public class DataDbContext : DbContext
    {
        public DbSet<DebitCard> DebitCards { get; set; }

        public DataDbContext()
        {}

        public DataDbContext(DbContextOptions options)
        : base(options)
        {}
    }
}
