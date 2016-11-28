using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleAnoModelMvc.Data.Migrations
{
    public partial class Anotate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Person",
                maxLength: 20,
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Hireate",
                table: "Person",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Person",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Hireate",
                table: "Person",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Person",
                nullable: false);
        }
    }
}
