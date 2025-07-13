namespace SmartBoostFin.Api.Models;

public enum LoanStatus { Pending, Approved, Rejected }

public class LoanApplication
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public int TermMonths { get; set; }
    public decimal MonthlyInstallment { get; set; }
    public LoanStatus Status { get; set; }

    public LoanPurpose Purpose { get; set; }
    public decimal? PropertyValue { get; set; }        // obbligatoria solo per prima casa
    public EmploymentType EmploymentType { get; set; }
    public decimal ExistingLoanMonthly { get; set; }

    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public int BankId { get; set; }
    public Bank? Bank { get; set; }
}
