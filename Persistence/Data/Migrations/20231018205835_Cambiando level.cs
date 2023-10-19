using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cambiandolevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_level_LevelId",
                table: "user");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "user",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_user_level_LevelId",
                table: "user",
                column: "LevelId",
                principalTable: "level",
                principalColumn: "level_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_level_LevelId",
                table: "user");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_user_level_LevelId",
                table: "user",
                column: "LevelId",
                principalTable: "level",
                principalColumn: "level_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
