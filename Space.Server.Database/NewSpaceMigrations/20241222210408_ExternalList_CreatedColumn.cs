using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Space.Server.Database.NewSpaceMigrations
{
    /// <inheritdoc />
    public partial class ExternalList_CreatedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "HasFunding",
                table: "NewSpaceExternalListItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "NewSpaceExternalListItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "NewSpaceExternalListItems");

            migrationBuilder.AlterColumn<bool>(
                name: "HasFunding",
                table: "NewSpaceExternalListItems",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
