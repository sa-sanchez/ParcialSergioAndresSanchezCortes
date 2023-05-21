using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.DAL.Entities
{
    public class Tickets
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? UseDate { get; set; }
        public bool? isUsed { get; set; }
        public string? EntranceGate { get; set; }
    }
}
