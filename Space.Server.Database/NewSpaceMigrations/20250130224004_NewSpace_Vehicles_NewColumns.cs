using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.Server.Database.NewSpaceMigrations
{
    /// <inheritdoc />
    public partial class NewSpace_Vehicles_NewColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "NewSpaceLaunchVehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "NewSpaceLaunchVehicles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "NewSpaceLaunchVehicles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "NewSpaceLaunchVehicles");
        }
    }
}
