namespace TicketingSystem.DAL.Entities
{
    public class DbSeeder
    {
        private readonly DataBaseContext _context;
        public DbSeeder(DataBaseContext context)
        {
            _context = context;
        }

        public async Task Seeder()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateTickets();

            await _context.SaveChangesAsync();
        }

        private async Task PopulateTickets()
        {
            if (!_context.Tickets.Any())
            {
                var tickets_number = 50000;
                for (int i = 0; i < tickets_number; i++)
                {
                    _context.Tickets.Add(new Ticket { UseDate = null, isUsed = false, EntranceGate = null});
                }
            }
        }
    }
}
