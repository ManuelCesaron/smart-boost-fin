using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartBoostFin.Api.Dtos;
using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly FinContext _ctx;
    public CustomersController(FinContext ctx) => _ctx = ctx;

    /* ───────── POST /api/customers ───────── */
    [HttpPost]
    public async Task<ActionResult<Customer>> Create([FromBody] CustomerCreateDto dto)
    {
        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            AnnualGrossSalary = dto.AnnualGrossSalary
        };

        _ctx.Customers.Add(customer);
        await _ctx.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
    }

    /* ───────── GET /api/customers/{id} ────── */
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Customer>> GetById(int id)
    {
        var cust = await _ctx.Customers
                             .Include(c => c.LoanApplications)
                             .ThenInclude(l => l.Bank)   // carica anche la banca
                             .AsNoTracking()
                             .FirstOrDefaultAsync(c => c.Id == id);

        return cust is null ? NotFound() : Ok(cust);
    }

    /* ───────── GET /api/customers ─────────── */
    [HttpGet]
    public async Task<IEnumerable<Customer>> GetAll() =>
        await _ctx.Customers
                  .Include(c => c.LoanApplications)
                  .ThenInclude(l => l.Bank)
                  .AsNoTracking()
                  .ToListAsync();
}
