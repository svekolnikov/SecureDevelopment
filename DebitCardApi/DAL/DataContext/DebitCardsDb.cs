using DebitCardApi.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DebitCardApi.DAL.DataContext
{
    public class DebitCardsDb : DbContext
    {
        public DbSet<DebitCard> DebitCards { get; set; }

        public DebitCardsDb()
        {}

        public DebitCardsDb(DbContextOptions options)
        : base(options)
        {}
    }
}
