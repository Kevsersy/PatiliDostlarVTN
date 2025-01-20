using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatiliDostlarVTN.Migrations
{
    /// <inheritdoc />
    public partial class Progresss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Metric2",
                table: "Progresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Metric2",
                table: "Progresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
