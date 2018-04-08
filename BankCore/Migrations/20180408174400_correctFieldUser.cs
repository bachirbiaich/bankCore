using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BankCore.Migrations
{
    public partial class correctFieldUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Users",
                newName: "firstname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Users",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Users",
                newName: "firstName");
        }
    }
}
