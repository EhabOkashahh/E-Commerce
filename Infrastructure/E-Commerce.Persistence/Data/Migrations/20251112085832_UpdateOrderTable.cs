using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "DeliveryMethods",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntenetId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntenetId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DeliveryMethods",
                newName: "price");
        }
    }
}
