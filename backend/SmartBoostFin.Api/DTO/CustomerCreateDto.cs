namespace SmartBoostFin.Api.Dtos;

public class CustomerCreateDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public decimal AnnualGrossSalary { get; set; }
}
