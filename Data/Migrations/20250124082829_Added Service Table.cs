using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedServiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ServiceId",
                table: "Projects",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ServiceEntity_ServiceId",
                table: "Projects",
                column: "ServiceId",
                principalTable: "ServiceEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ServiceEntity_ServiceId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "ServiceEntity");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ServiceId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Projects");
        }
    }
}
