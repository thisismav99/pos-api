using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PosAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactionModel_ProductModel_ProductId",
                table: "ProductTransactionModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransactionModel_TransactionModel_TransactionId",
                table: "ProductTransactionModel");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionModel_CardModel_CardId",
                table: "TransactionModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionModel",
                table: "TransactionModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTransactionModel",
                table: "ProductTransactionModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductModel",
                table: "ProductModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CardModel",
                table: "CardModel");

            migrationBuilder.RenameTable(
                name: "TransactionModel",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "ProductTransactionModel",
                newName: "ProductTransaction");

            migrationBuilder.RenameTable(
                name: "ProductModel",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "CardModel",
                newName: "Card");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionModel_CardId",
                table: "Transaction",
                newName: "IX_Transaction_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTransactionModel_TransactionId",
                table: "ProductTransaction",
                newName: "IX_ProductTransaction_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTransactionModel_ProductId",
                table: "ProductTransaction",
                newName: "IX_ProductTransaction_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTransaction",
                table: "ProductTransaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransaction_Product_ProductId",
                table: "ProductTransaction",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransaction_Transaction_TransactionId",
                table: "ProductTransaction",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Card_CardId",
                table: "Transaction",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransaction_Product_ProductId",
                table: "ProductTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTransaction_Transaction_TransactionId",
                table: "ProductTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Card_CardId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTransaction",
                table: "ProductTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "TransactionModel");

            migrationBuilder.RenameTable(
                name: "ProductTransaction",
                newName: "ProductTransactionModel");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "ProductModel");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "CardModel");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CardId",
                table: "TransactionModel",
                newName: "IX_TransactionModel_CardId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTransaction_TransactionId",
                table: "ProductTransactionModel",
                newName: "IX_ProductTransactionModel_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTransaction_ProductId",
                table: "ProductTransactionModel",
                newName: "IX_ProductTransactionModel_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionModel",
                table: "TransactionModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTransactionModel",
                table: "ProductTransactionModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductModel",
                table: "ProductModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CardModel",
                table: "CardModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactionModel_ProductModel_ProductId",
                table: "ProductTransactionModel",
                column: "ProductId",
                principalTable: "ProductModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTransactionModel_TransactionModel_TransactionId",
                table: "ProductTransactionModel",
                column: "TransactionId",
                principalTable: "TransactionModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionModel_CardModel_CardId",
                table: "TransactionModel",
                column: "CardId",
                principalTable: "CardModel",
                principalColumn: "Id");
        }
    }
}
