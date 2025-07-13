using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api.Services;

public class LoanCalculator
{
    // ───── Costanti di business ─────
    private const decimal TaxDeductionRate = 0.35m;   // -35 % trattenute sul lordo
    private const decimal BaseIncomeRatio = 0.30m;   // rata ≤ 30 % del netto mensile
    private const decimal FixedTermPenalty = 0.80m;   // -20 % di capacità se contratto TD

    /// <summary>
    /// Calcola rata e decisione finale (Approved/Rejected).
    /// </summary>
    public (LoanStatus Status, decimal MonthlyInstallment) Evaluate(
        Customer customer,
        LoanApplication draft,
        Bank bank)
    {
        // 1) Netto mensile stimato dal lordo annuo (RAL)
        decimal netAnnual = customer.AnnualGrossSalary * (1 - TaxDeductionRate);
        decimal netMonthly = netAnnual / 12;

        // 2) Sottrai eventuali rate di prestiti in corso
        netMonthly -= draft.ExistingLoanMonthly;

        // 3) Penalità per contratto a termine
        if (draft.EmploymentType == EmploymentType.FixedTerm)
            netMonthly *= FixedTermPenalty;

        // 4) Massima rata sostenibile
        decimal maxAffordableInstallment = netMonthly * BaseIncomeRatio;

        // 5) Tasso in base alla durata richiesta
        decimal annualRate = draft.TermMonths switch
        {
            <= 120 => bank.Rate10,
            <= 240 => bank.Rate20,
            _ => bank.Rate30
        };
        decimal monthlyRate = annualRate / 12 / 100;

        // 6) Ammortamento francese (semplificato)
        decimal installment = (decimal)(
            (double)draft.Amount * (double)monthlyRate /
            (1 - Math.Pow(1 + (double)monthlyRate, -draft.TermMonths))
        );

        // 7) Regola prima casa: importo ≤ 80 % del valore immobile
        bool propertyRule = draft.Purpose == LoanPurpose.FirstHomePurchase
            ? draft.Amount <= 0.8m * (draft.PropertyValue ?? 0)
            : true;

        // 8) Limite di rischio fissato dalla banca
        bool riskRule = draft.Amount <= bank.MaxRiskLoan;

        // 9) Decisione
        bool sustainable = installment <= maxAffordableInstallment
                           && propertyRule
                           && riskRule;

        return (sustainable ? LoanStatus.Approved : LoanStatus.Rejected,
                Math.Round(installment, 2));
    }
}
