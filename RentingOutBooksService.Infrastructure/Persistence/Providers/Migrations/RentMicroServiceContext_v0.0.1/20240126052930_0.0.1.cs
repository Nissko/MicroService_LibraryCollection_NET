using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentingOutBooksService.Infrastructure.Persistence.Providers.Migrations.RentMicroServiceContextv0._0._1
{
    /// <inheritdoc />
    public partial class _001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "LIBRARY_RENT_BOOKS");

            migrationBuilder.CreateTable(
                name: "RentStatuses",
                schema: "LIBRARY_RENT_BOOKS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenantries",
                schema: "LIBRARY_RENT_BOOKS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenantries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                schema: "LIBRARY_RENT_BOOKS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentStatusId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CountRentDay = table.Column<int>(type: "integer", nullable: false),
                    RentEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RentStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rents_RentStatuses_RentStatusId",
                        column: x => x.RentStatusId,
                        principalSchema: "LIBRARY_RENT_BOOKS",
                        principalTable: "RentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rents_Tenantries_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "LIBRARY_RENT_BOOKS",
                        principalTable: "Tenantries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rents_ClientId",
                schema: "LIBRARY_RENT_BOOKS",
                table: "Rents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rents_RentStatusId",
                schema: "LIBRARY_RENT_BOOKS",
                table: "Rents",
                column: "RentStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rents",
                schema: "LIBRARY_RENT_BOOKS");

            migrationBuilder.DropTable(
                name: "RentStatuses",
                schema: "LIBRARY_RENT_BOOKS");

            migrationBuilder.DropTable(
                name: "Tenantries",
                schema: "LIBRARY_RENT_BOOKS");
        }
    }
}
