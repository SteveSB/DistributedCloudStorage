using Microsoft.EntityFrameworkCore.Migrations;

namespace FileServerManager.Migrations
{
    public partial class serverIdfieldinfiletable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Servers_ServerId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "ServerId",
                table: "Files",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Servers_ServerId",
                table: "Files",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Servers_ServerId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "ServerId",
                table: "Files",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Servers_ServerId",
                table: "Files",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
