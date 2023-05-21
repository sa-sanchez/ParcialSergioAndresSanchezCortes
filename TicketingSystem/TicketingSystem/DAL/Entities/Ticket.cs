using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.DAL.Entities
{
    public class Ticket
    {
        [Key]
        [Required(ErrorMessage = "Campo obligatorio.")]
        public Guid Id { get; set; }
        public DateTime? UseDate { get; set; }
        public bool? isUsed { get; set; }
        public string? EntranceGate { get; set; }
    }
}
