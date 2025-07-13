namespace SmartBoostFin.Api.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    //RAL lordo annuo
    public decimal AnnualGrossSalary { get; set; }

    public ICollection<LoanApplication>? LoanApplications { get; set; }
}
