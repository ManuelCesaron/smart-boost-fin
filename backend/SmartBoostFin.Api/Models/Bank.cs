namespace SmartBoostFin.Api.Models;

public class Bank
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public decimal Rate10 { get; set; }
    public decimal Rate20 { get; set; }
    public decimal Rate30 { get; set; }

    public decimal MaxRiskLoan { get; set; }
}
