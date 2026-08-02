namespace SupportTicket.Api.DTOs.Tickets;

public class TicketResponse
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}