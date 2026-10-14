using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schadenverwaltung.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimNumberSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ClaimNumbers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ClaimNumbers");
        }
    }
}
