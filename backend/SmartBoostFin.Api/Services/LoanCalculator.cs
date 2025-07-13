using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api.Services;

public class LoanCalculator
{
    private const decimal TaxDeductionRate = 0.35m;
    private const decimal BaseIncomeRatio = 0.30m;
    private const decimal FixedTermPenalty = 0.80m;

    public (LoanStatus Status, decimal MonthlyInstallment) Evaluate(
        Customer customer,
        LoanApplication draft,
        Bank bank)
    {
        decimal netAnnual = customer.AnnualGrossSalary * (1 - TaxDeductionRate);
        decimal netMonthly = netAnnual / 12 - draft.ExistingLoanMonthly;

        if (draft.EmploymentType == EmploymentType.FixedTerm)
            netMonthly *= FixedTermPenalty;

        decimal maxAffordableInstallment = netMonthly * BaseIncomeRatio;

        decimal annualRate = draft.TermMonths switch
        {
            <= 120 => bank.Rate10,
            <= 240 => bank.Rate20,
            _ => bank.Rate30
        };
        decimal monthlyRate = annualRate / 12 / 100;

        decimal installment = (decimal)(
            (double)draft.Amount * (double)monthlyRate /
            (1 - Math.Pow(1 + (double)monthlyRate, -draft.TermMonths))
        );

        bool propertyRule = draft.Purpose == LoanPurpose.FirstHomePurchase
            ? draft.Amount <= 0.8m * (draft.PropertyValue ?? 0)
            : true;

        bool sustainable = installment <= maxAffordableInstallment
                           && propertyRule;          

        return (sustainable ? LoanStatus.Approved : LoanStatus.Rejected,
                Math.Round(installment, 2));
    }
}
