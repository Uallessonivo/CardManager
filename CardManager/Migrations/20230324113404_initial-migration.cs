using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CardManager.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<Guid>(type: "uuid", nullable: false),
                    CardSerial = table.Column<string>(type: "text", nullable: true),
                    CardOwnerName = table.Column<string>(type: "text", nullable: true),
                    CardOwnerCpf = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
