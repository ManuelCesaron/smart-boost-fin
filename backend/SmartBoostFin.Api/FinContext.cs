using Microsoft.EntityFrameworkCore;
using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api;

public class FinContext : DbContext
{
    public FinContext(DbContextOptions<FinContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
}
