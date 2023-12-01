using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Zearain.AoC23.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePartSolutionToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PartSolutions",
                table: "PartSolutions");

            migrationBuilder.AlterColumn<int>(
                name: "PartNumber",
                table: "PartSolutions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PartSolutions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartSolutions",
                table: "PartSolutions",
                columns: new[] { "Id", "AdventDayId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PartSolutions",
                table: "PartSolutions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PartSolutions");

            migrationBuilder.AlterColumn<int>(
                name: "PartNumber",
                table: "PartSolutions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartSolutions",
                table: "PartSolutions",
                columns: new[] { "PartNumber", "AdventDayId" });
        }
    }
}
