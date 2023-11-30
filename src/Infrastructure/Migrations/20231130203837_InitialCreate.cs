using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zearain.AoC23.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdventDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayNumber = table.Column<int>(type: "integer", nullable: false),
                    HasInput = table.Column<bool>(type: "boolean", nullable: false),
                    Input = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartSolutions",
                columns: table => new
                {
                    PartNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdventDayId = table.Column<Guid>(type: "uuid", nullable: false),
                    Solution = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartSolutions", x => new { x.PartNumber, x.AdventDayId });
                    table.ForeignKey(
                        name: "FK_PartSolutions_AdventDays_AdventDayId",
                        column: x => x.AdventDayId,
                        principalTable: "AdventDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartSolutions_AdventDayId",
                table: "PartSolutions",
                column: "AdventDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartSolutions");

            migrationBuilder.DropTable(
                name: "AdventDays");
        }
    }
}
