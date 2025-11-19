using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterportCargo.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerResponseToQuotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerResponse",
                table: "Quotations",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerResponse",
                table: "Quotations");
        }
    }
}
