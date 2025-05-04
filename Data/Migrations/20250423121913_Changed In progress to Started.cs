using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedInprogresstoStarted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: "1",
                column: "StatusName",
                value: "Started");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: "1",
                column: "StatusName",
                value: "In Progress");
        }
    }
}
