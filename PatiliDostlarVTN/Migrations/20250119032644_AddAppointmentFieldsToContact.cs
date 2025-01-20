using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatiliDostlarVTN.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentFieldsToContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "AppointmentTime",
                table: "Contacts",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Contacts");
        }
    }
}
