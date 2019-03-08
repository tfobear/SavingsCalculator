using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SavingsCalculator.Api.Migrations
{
    public partial class SavingsGoalsUserFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SavingsGoals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SavingsGoals_UserId",
                table: "SavingsGoals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingsGoals_AspNetUsers_UserId",
                table: "SavingsGoals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavingsGoals_AspNetUsers_UserId",
                table: "SavingsGoals");

            migrationBuilder.DropIndex(
                name: "IX_SavingsGoals_UserId",
                table: "SavingsGoals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SavingsGoals");
        }
    }
}
