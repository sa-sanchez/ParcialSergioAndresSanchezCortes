using Microsoft.EntityFrameworkCore;
using TicketingSystem.DAL.Entities;

namespace TicketingSystem.DAL
{
    public class DataBaseContext: DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
