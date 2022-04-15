using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonApp.DAL.Migrations
{
    public partial class persontIdRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_People_PersonId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "PersontId",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2022, 4, 15, 16, 25, 18, 547, DateTimeKind.Local).AddTicks(5846));

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_People_PersonId",
                table: "Contacts",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_People_PersonId",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Contacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PersontId",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2022, 4, 7, 9, 15, 12, 157, DateTimeKind.Local).AddTicks(4259));

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_People_PersonId",
                table: "Contacts",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
