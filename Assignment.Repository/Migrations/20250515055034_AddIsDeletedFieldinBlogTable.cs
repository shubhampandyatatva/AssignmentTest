using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedFieldinBlogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isdeleted",
                table: "Blogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isdeleted",
                table: "Blogs");
        }
    }
}
