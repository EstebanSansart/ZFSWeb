using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguraciondeimagenesEventos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventoId",
                table: "image",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "event",
                columns: new[] { "event_id", "event_capacity", "event_date", "event_points", "event_name", "event_sponsorship", "event_state" },
                values: new object[] { 1, 200, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "Hallowen", "Frysby", true });

            migrationBuilder.UpdateData(
                table: "image",
                keyColumn: "image_id",
                keyValue: 1,
                columns: new[] { "EventoId", "image_url" },
                values: new object[] { 1, "https://www.tooltyp.com/wp-content/uploads/2014/10/1900x920-8-beneficios-de-usar-imagenes-en-nuestros-sitios-web.jpg" });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "user_cc", "user_age", "CompanyId", "user_contact", "GenderId", "is_new", "LevelId", "user_name", "user_password", "Points" },
                values: new object[] { "123", "19", 1, "Brayan@gmail.com", 1, true, 1, "Brayan", "123", 0 });

            migrationBuilder.InsertData(
                table: "image",
                columns: new[] { "image_id", "EventoId", "image_url" },
                values: new object[] { 2, 1, "https://images.ctfassets.net/hrltx12pl8hq/5KiKmVEsCQPMNrbOE6w0Ot/341c573752bf35cb969e21fcd279d3f9/hero-img_copy.jpg?fit=fill&w=600&h=400" });

            migrationBuilder.CreateIndex(
                name: "IX_image_EventoId",
                table: "image",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_image_event_EventoId",
                table: "image",
                column: "EventoId",
                principalTable: "event",
                principalColumn: "event_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_event_EventoId",
                table: "image");

            migrationBuilder.DropIndex(
                name: "IX_image_EventoId",
                table: "image");

            migrationBuilder.DeleteData(
                table: "image",
                keyColumn: "image_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user",
                keyColumn: "user_cc",
                keyValue: "123");

            migrationBuilder.DeleteData(
                table: "event",
                keyColumn: "event_id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "EventoId",
                table: "image");

            migrationBuilder.UpdateData(
                table: "image",
                keyColumn: "image_id",
                keyValue: 1,
                column: "image_url",
                value: "https://www.tooltyp.com/wp-content/uploads/2014/10/1900x920-8-beneficios-de-usar-imagenes-en-nuestros-sitios-web.jpg ");
        }
    }
}
