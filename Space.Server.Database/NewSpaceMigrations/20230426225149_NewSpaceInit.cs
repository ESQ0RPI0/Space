using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.Server.Database.NewSpaceMigrations
{
    /// <inheritdoc />
    public partial class NewSpaceInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewSpaceCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewSpaceCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewSpaceLaunchVehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewSpaceLaunchVehicles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewSpaceCompanies");

            migrationBuilder.DropTable(
                name: "NewSpaceLaunchVehicles");
        }
    }
}
