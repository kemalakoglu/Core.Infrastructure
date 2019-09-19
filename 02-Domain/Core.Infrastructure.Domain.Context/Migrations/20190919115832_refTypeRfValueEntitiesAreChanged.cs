using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Infrastructure.Domain.Context.Migrations
{
    public partial class refTypeRfValueEntitiesAreChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "RefValue",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "RefType",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_RefType_ParentId",
                table: "RefType",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefType_RefType_ParentId",
                table: "RefType",
                column: "ParentId",
                principalTable: "RefType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefType_RefType_ParentId",
                table: "RefType");

            migrationBuilder.DropIndex(
                name: "IX_RefType_ParentId",
                table: "RefType");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "RefValue");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "RefType");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
