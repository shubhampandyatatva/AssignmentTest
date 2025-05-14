using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesForeignKeyToUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Createdat",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Createdby",
                table: "Roles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updatedat",
                table: "Roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updatedby",
                table: "Roles",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Createdat",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Createdby",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Updatedat",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Updatedby",
                table: "Roles");
        }
    }
}
