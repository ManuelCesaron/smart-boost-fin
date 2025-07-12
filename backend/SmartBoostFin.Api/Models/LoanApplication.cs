namespace SmartBoostFin.Api.Models;

public enum LoanStatus { Pending, Approved, Rejected }

public class LoanApplication
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int TermMonths { get; set; }
    public decimal MonthlyInstallment { get; set; }
    public LoanStatus Status { get; set; }

    // Foreign Key
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}
