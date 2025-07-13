using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBoostFin.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBanksAndLoanFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmploymentType",
                table: "LoanApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ExistingLoanMonthly",
                table: "LoanApplications",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PropertyValue",
                table: "LoanApplications",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Purpose",
                table: "LoanApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Rate10 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rate20 = table.Column<decimal>(type: "TEXT", nullable: false),
                    Rate30 = table.Column<decimal>(type: "TEXT", nullable: false),
                    MaxRiskLoan = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "ExistingLoanMonthly",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "PropertyValue",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "Purpose",
                table: "LoanApplications");
        }
    }
}
