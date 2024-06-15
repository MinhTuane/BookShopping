using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopping.Migrations
{
    /// <inheritdoc />
    public partial class addCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Books_BookId",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_ShoppingCarts_ShoppingCartId",
                table: "CartDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetail",
                table: "CartDetail");

            migrationBuilder.RenameTable(
                name: "CartDetail",
                newName: "CartDetails");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_ShoppingCartId",
                table: "CartDetails",
                newName: "IX_CartDetails_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_BookId",
                table: "CartDetails",
                newName: "IX_CartDetails_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Books_BookId",
                table: "CartDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_ShoppingCarts_ShoppingCartId",
                table: "CartDetails",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Books_BookId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_ShoppingCarts_ShoppingCartId",
                table: "CartDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartDetails",
                table: "CartDetails");

            migrationBuilder.RenameTable(
                name: "CartDetails",
                newName: "CartDetail");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_ShoppingCartId",
                table: "CartDetail",
                newName: "IX_CartDetail_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_BookId",
                table: "CartDetail",
                newName: "IX_CartDetail_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartDetail",
                table: "CartDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Books_BookId",
                table: "CartDetail",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_ShoppingCarts_ShoppingCartId",
                table: "CartDetail",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
