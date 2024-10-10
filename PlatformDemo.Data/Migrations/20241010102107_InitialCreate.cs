using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PlatformDemo.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicePlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timesheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicePlanId = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Timesheets_ServicePlans_ServicePlanId",
                        column: x => x.ServicePlanId,
                        principalTable: "ServicePlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ServicePlans",
                columns: new[] { "Id", "DateOfPurchase" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2023, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2023, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Timesheets",
                columns: new[] { "Id", "Description", "EndDateTime", "ServicePlanId", "StartDateTime" },
                values: new object[,]
                {
                    { 1, "Initial consultation", new DateTime(2023, 1, 16, 17, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 1, 16, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Project kickoff", new DateTime(2023, 1, 17, 18, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 1, 17, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Requirements gathering", new DateTime(2023, 2, 11, 16, 30, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 2, 11, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Design discussion", new DateTime(2023, 2, 12, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 2, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Development sprint", new DateTime(2023, 3, 21, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 3, 21, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "Code review", new DateTime(2023, 3, 22, 18, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 3, 22, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Timesheets_ServicePlanId",
                table: "Timesheets",
                column: "ServicePlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timesheets");

            migrationBuilder.DropTable(
                name: "ServicePlans");
        }
    }
}
