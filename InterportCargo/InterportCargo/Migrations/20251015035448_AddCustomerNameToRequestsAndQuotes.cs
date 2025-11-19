using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterportCargo.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerNameToRequestsAndQuotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Quotations",
                type: "TEXT",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "QuotationRequests",
                type: "TEXT",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Quotations");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "QuotationRequests");
        }
    }
}
