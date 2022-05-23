using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CateringSedapAPI.Migrations
{
    public partial class addReservationFood : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Reservations_ReservationId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_ReservationId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Foods");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "Foods",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_ReservationId",
                table: "Foods",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Reservations_ReservationId",
                table: "Foods",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id");
        }
    }
}
