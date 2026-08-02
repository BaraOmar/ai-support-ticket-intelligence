using Microsoft.AspNetCore.Mvc;
using SupportTicket.Api.Data;
using SupportTicket.Api.DTOs.Tickets;
using SupportTicket.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace SupportTicket.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TicketsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<TicketResponse>> CreateTicket(
        CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Description = request.Description.Trim(),
            Status = "New",
            CreatedAt = DateTime.UtcNow
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        var response = new TicketResponse
        {
            Id = ticket.Id,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt
        };

        return CreatedAtAction(
            nameof(GetTicketById),
            new { id = ticket.Id },
            response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketResponse>> GetTicketById(int id)
    {
        var ticket = await _context.Tickets
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (ticket is null)
        {
            return NotFound();
        }

        var response = new TicketResponse
        {
            Id = ticket.Id,
            Description = ticket.Description,
            Status = ticket.Status,
            CreatedAt = ticket.CreatedAt
        };

        return Ok(response);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketResponse>>> GetTickets()
    {
        var tickets = await _context.Tickets
            .AsNoTracking()
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TicketResponse
            {
                Id = t.Id,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            })
            .ToListAsync();

        return Ok(tickets);
    }
}