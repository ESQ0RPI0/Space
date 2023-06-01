using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.Server.Database.NewSpaceMigrations
{
    /// <inheritdoc />
    public partial class NewSpaceListItemDbTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewSpaceExternalListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Launcher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Founded = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    FirstLaunch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Launches = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Perfomance = table.Column<int>(type: "int", nullable: true),
                    PricePerKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Funding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewSpaceExternalListItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewSpaceExternalListItems");
        }
    }
}
