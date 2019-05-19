using Microsoft.EntityFrameworkCore.Migrations;

namespace FileServerManager.Migrations
{
    public partial class addbackupservertofiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBackup",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "BackupServer",
                table: "Files",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackupServer",
                table: "Files");

            migrationBuilder.AddColumn<bool>(
                name: "HasBackup",
                table: "Files",
                nullable: false,
                defaultValue: false);
        }
    }
}
