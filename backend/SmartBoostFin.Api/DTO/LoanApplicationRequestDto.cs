using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api.Dtos;

public class LoanApplicationRequestDto
{
    // ★ dati che arrivano dal form
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
    public int TermMonths { get; set; }
    public LoanPurpose Purpose { get; set; }
    public decimal? PropertyValue { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public decimal ExistingLoanMonthly { get; set; }

    public LoanApplication ToEntity() => new()
    {
        Amount = Amount,
        TermMonths = TermMonths,
        Purpose = Purpose,
        PropertyValue = PropertyValue,
        EmploymentType = EmploymentType,
        ExistingLoanMonthly = ExistingLoanMonthly,
        Status = LoanStatus.Pending,
        CustomerId = CustomerId      // FK verso il cliente
    };
}
