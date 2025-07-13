using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBoostFin.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaxRiskLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxRiskLoan",
                table: "Banks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MaxRiskLoan",
                table: "Banks",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
