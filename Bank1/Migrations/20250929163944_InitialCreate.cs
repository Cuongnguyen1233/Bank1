using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbAccount",
                columns: table => new
                {
                    AccountName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Pincode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAccount", x => x.AccountName);
                });

            migrationBuilder.CreateTable(
                name: "tbTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Withdraw = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTransaction", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbAccount");

            migrationBuilder.DropTable(
                name: "tbTransaction");
        }
    }
}
