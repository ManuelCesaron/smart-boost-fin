using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBoostFin.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameNetToGross : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NetAnnualSalary",
                table: "Customers",
                newName: "AnnualGrossSalary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnnualGrossSalary",
                table: "Customers",
                newName: "NetAnnualSalary");
        }
    }
}
