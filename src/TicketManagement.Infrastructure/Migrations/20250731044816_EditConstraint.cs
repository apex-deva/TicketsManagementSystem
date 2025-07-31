using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Tickets_Status_Valid",
                table: "Tickets");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tickets_Status_Valid",
                table: "Tickets",
                sql: "[Status] IN (1, 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Tickets_Status_Valid",
                table: "Tickets");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tickets_Status_Valid",
                table: "Tickets",
                sql: "[Status] IN ('Open', 'Handled')");
        }
    }
}
