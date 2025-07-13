using Microsoft.EntityFrameworkCore;
using SmartBoostFin.Api;
using SmartBoostFin.Api.Models;
using SmartBoostFin.Api.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ───── DbContext + SQLite ─────
builder.Services.AddDbContext<FinContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FinDb")));

// Registrazioni dei servizi applicativi
builder.Services.AddScoped<LoanCalculator>();
builder.Services.AddScoped<LoanApplicationService>();

// ───── Servizi API standard ─────
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ───── Pipeline HTTP ─────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ───── Seed banche di test (solo se il DB è vuoto) ─────
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<FinContext>();

    if (!ctx.Banks.Any())
    {
        ctx.Banks.AddRange(
            new Bank
            {
                Name = "AlphaBank",
                Rate10 = 4.2m,
                Rate20 = 3.1m,
                Rate30 = 2.4m,
                MaxRiskLoan = 250_000m
            },
            new Bank
            {
                Name = "BetaCredit",
                Rate10 = 3.9m,
                Rate20 = 3.4m,
                Rate30 = 2.9m,
                MaxRiskLoan = 300_000m
            },
            new Bank
            {
                Name = "GammaFinance",
                Rate10 = 4.3m,
                Rate20 = 3.3m,
                Rate30 = 2.8m,
                MaxRiskLoan = 220_000m
            },
            new Bank
            {
                Name = "DeltaMutui",
                Rate10 = 3.8m,
                Rate20 = 3.5m,
                Rate30 = 2.3m,
                MaxRiskLoan = 350_000m
            }
        );
        ctx.SaveChanges();
    }
}


app.Run();
