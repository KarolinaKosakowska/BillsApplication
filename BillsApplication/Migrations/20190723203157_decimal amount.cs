using Microsoft.EntityFrameworkCore.Migrations;

namespace BillsApplication.Migrations
{
    public partial class decimalamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Limit",
                table: "Budgets",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Budgets",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Limit",
                table: "Budgets",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Budgets",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
