using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RollCall.MVC.Migrations
{
    public partial class addCodeGenTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeGeneratedTime",
                table: "SchoolClasses",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeGeneratedTime",
                table: "SchoolClasses");
        }
    }
}
