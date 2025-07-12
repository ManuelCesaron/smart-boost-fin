using Microsoft.EntityFrameworkCore;
using SmartBoostFin.Api;

var builder = WebApplication.CreateBuilder(args);

// ───── DbContext + SQLite ─────
builder.Services.AddDbContext<FinContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FinDb")));

// ───── Servizi API standard ─────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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

app.Run();
