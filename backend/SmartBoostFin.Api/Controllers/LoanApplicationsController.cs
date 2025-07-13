using Microsoft.AspNetCore.Mvc;
using SmartBoostFin.Api.Dtos;
using SmartBoostFin.Api.Models;
using SmartBoostFin.Api.Services;

namespace SmartBoostFin.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanApplicationsController : ControllerBase
{
    private readonly LoanApplicationService _service;

    public LoanApplicationsController(LoanApplicationService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Apply([FromBody] LoanApplicationRequestDto dto)
    {
        var resp = await _service.SubmitAsync(dto);
        if (resp is null) return NotFound("Customer not found");

        return Ok(resp);
    }

}
