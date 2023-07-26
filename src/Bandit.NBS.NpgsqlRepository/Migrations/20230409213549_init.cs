using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bandit.NBS.NpgsqlRepository.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DebitBank = table.Column<string>(type: "text", nullable: false),
                    CreditBank = table.Column<string>(type: "text", nullable: false),
                    MerchantName = table.Column<string>(type: "text", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientGender = table.Column<string>(type: "text", nullable: false),
                    ClientBirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientAge = table.Column<int>(type: "integer", nullable: false),
                    ClientMaritalStatus = table.Column<string>(type: "text", nullable: false),
                    ClientMonthlySalary = table.Column<double>(type: "double precision", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MerchantId = table.Column<string>(type: "text", nullable: false),
                    MerchantActivity = table.Column<string>(type: "text", nullable: false),
                    AuthenticationMethod = table.Column<string>(type: "text", nullable: false),
                    TransferredAmount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
