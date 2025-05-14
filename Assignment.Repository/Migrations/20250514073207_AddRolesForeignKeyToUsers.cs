using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesForeignKeyToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Roleid",
                table: "Users",
                column: "Roleid");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Roleid",
                table: "Users",
                column: "Roleid",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Roleid",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Roleid",
                table: "Users");
        }
    }
}
