using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.Server.Database.NewSpaceMigrations
{
    /// <inheritdoc />
    public partial class NewSpaceExternal_NewColumnAndDataTypeChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Funding",
                table: "NewSpaceExternalListItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFunding",
                table: "NewSpaceExternalListItems",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasFunding",
                table: "NewSpaceExternalListItems");

            migrationBuilder.AlterColumn<string>(
                name: "Funding",
                table: "NewSpaceExternalListItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
