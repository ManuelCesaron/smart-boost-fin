using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBoostFin.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAndFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "LoanApplications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoanApplications_BankId",
                table: "LoanApplications",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanApplications_Banks_BankId",
                table: "LoanApplications",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanApplications_Banks_BankId",
                table: "LoanApplications");

            migrationBuilder.DropIndex(
                name: "IX_LoanApplications_BankId",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "LoanApplications");
        }
    }
}
