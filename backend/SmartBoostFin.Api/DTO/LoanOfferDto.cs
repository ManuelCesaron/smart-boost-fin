namespace SmartBoostFin.Api.Dtos;

public class LoanOfferDto
{
    public string BankName { get; set; } = null!;
    public decimal AnnualRate { get; set; }
    public int TermMonths { get; set; }
    public decimal MonthlyInstallment { get; set; }
}
