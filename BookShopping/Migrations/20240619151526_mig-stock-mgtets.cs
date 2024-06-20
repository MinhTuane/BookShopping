using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopping.Migrations
{
    /// <inheritdoc />
    public partial class migstockmgtets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genre",
                newName: "GenreName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genre",
                newName: "Name");
        }
    }
}
