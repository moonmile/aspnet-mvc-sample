using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleCFModelMvc.Data.Migrations
{
    public partial class ChangePerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.CreateTable(
    name: "Perfecture",
    columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfecture", x => x.Id);
                });

            migrationBuilder.AddColumn<int>(
                name: "PerfectureId",
                table: "Person",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Person_PerfectureId",
                table: "Person",
                column: "PerfectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Perfecture_PerfectureId",
                table: "Person",
                column: "PerfectureId",
                principalTable: "Perfecture",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Perfecture_PerfectureId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_PerfectureId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PerfectureId",
                table: "Person");

            migrationBuilder.DropTable(
                name: "Perfecture");
        }
    }
}
