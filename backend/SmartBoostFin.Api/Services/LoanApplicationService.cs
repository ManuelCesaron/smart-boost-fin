using Microsoft.EntityFrameworkCore;
using SmartBoostFin.Api.Dtos;
using SmartBoostFin.Api.Models;

namespace SmartBoostFin.Api.Services;

public class LoanApplicationService
{
    private readonly FinContext _ctx;
    private readonly LoanCalculator _calc;

    public LoanApplicationService(FinContext ctx, LoanCalculator calc)
    {
        _ctx = ctx;
        _calc = calc;
    }

    public async Task<LoanApplicationResponseDto?> SubmitAsync(LoanApplicationRequestDto dto)
    {
        var customer = await _ctx.Customers.FindAsync(dto.CustomerId);
        if (customer is null) return null;

        var draft = dto.ToEntity();
        var banks = await _ctx.Banks.AsNoTracking().ToListAsync();
        var offers = new List<LoanOfferDto>();

        foreach (var bank in banks)
        {
            var (status, installment) = _calc.Evaluate(customer, draft, bank);
            if (status == LoanStatus.Approved)
            {
                var annualRate = draft.TermMonths switch
                {
                    <= 120 => bank.Rate10,
                    <= 240 => bank.Rate20,
                    _ => bank.Rate30
                };

                offers.Add(new LoanOfferDto
                {
                    BankName = bank.Name,
                    AnnualRate = annualRate,
                    TermMonths = draft.TermMonths,
                    MonthlyInstallment = installment
                });
            }
        }

        return new LoanApplicationResponseDto
        {
            Status = offers.Any() ? LoanStatus.Approved : LoanStatus.Rejected,
            Offers = offers
        };
    }
}
