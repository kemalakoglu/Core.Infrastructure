using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Domain.Context.Migrations
{
    public partial class BaseEntityIsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "RefValue",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RefValue",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "RefValue",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertDate",
                table: "RefType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "RefType",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "RefType",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "RefValue");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RefValue");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "RefValue");

            migrationBuilder.DropColumn(
                name: "InsertDate",
                table: "RefType");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "RefType");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "RefType");
        }
    }
}
