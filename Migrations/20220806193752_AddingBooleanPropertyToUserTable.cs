using Microsoft.EntityFrameworkCore.Migrations;

namespace cineweb_user_api.Migrations
{
    public partial class AddingBooleanPropertyToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");

            migrationBuilder.AddColumn<bool>(
                name: "AdminUser",
                table: "user",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminUser",
                table: "user");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "varchar(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(64)");
        }
    }
}
