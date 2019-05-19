using Microsoft.EntityFrameworkCore.Migrations;

namespace FileServerManager.Migrations
{
    public partial class addfilebackuppath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BackupPath",
                table: "Files",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackupPath",
                table: "Files");
        }
    }
}
