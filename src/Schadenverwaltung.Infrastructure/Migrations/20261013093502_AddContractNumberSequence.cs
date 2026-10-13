using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schadenverwaltung.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddContractNumberSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ContractNumbers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ContractNumbers");
        }
    }
}
