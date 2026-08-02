using System.ComponentModel.DataAnnotations;

namespace SupportTicket.Api.DTOs.Tickets;

public class CreateTicketRequest
{
    [Required]
    public string Description { get; set; } = string.Empty;
}