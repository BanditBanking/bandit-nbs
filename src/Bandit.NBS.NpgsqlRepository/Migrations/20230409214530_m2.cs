using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bandit.NBS.NpgsqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "BD_OPER_TRANSAC");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BD_OPER_TRANSAC",
                table: "BD_OPER_TRANSAC",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BD_OPER_TRANSAC",
                table: "BD_OPER_TRANSAC");

            migrationBuilder.RenameTable(
                name: "BD_OPER_TRANSAC",
                newName: "Transactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");
        }
    }
}
