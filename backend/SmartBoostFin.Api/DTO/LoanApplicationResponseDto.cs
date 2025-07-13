using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api.Dtos;

public class LoanApplicationResponseDto
{
    public LoanStatus Status { get; set; }
    public IEnumerable<LoanOfferDto> Offers { get; set; } = [];
}
