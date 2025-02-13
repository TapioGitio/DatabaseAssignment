using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedServicesSinceItWasNotThere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ServiceEntity_ServiceId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceEntity",
                table: "ServiceEntity");

            migrationBuilder.RenameTable(
                name: "ServiceEntity",
                newName: "Services");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Services_ServiceId",
                table: "Projects",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Services_ServiceId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "ServiceEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceEntity",
                table: "ServiceEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ServiceEntity_ServiceId",
                table: "Projects",
                column: "ServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
