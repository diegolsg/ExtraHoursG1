using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtraHours.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EliminoTypeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtraHours_ExtraHourTypes_ExtraHoursTypeId",
                table: "ExtraHours");

            migrationBuilder.DropIndex(
                name: "IX_ExtraHours_ExtraHoursTypeId",
                table: "ExtraHours");

            migrationBuilder.DropColumn(
                name: "ExtraHoursTypeId",
                table: "ExtraHours");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtraHoursTypeId",
                table: "ExtraHours",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExtraHours_ExtraHoursTypeId",
                table: "ExtraHours",
                column: "ExtraHoursTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtraHours_ExtraHourTypes_ExtraHoursTypeId",
                table: "ExtraHours",
                column: "ExtraHoursTypeId",
                principalTable: "ExtraHourTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
