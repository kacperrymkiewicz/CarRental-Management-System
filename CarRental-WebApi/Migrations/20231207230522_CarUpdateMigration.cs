using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental_WebApi.Migrations
{
    /// <inheritdoc />
    public partial class CarUpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoorsNumber",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gearbox",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Cars",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "SeatsNumber",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrunkCapacity",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoorsNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Gearbox",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "SeatsNumber",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "TrunkCapacity",
                table: "Cars");
        }
    }
}
