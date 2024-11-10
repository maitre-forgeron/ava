using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ava.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTherapistCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TherapistCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TherapistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapistCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TherapistCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TherapistCategories_UserProfiles_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TherapistCategories_CategoryId",
                table: "TherapistCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapistCategories_TherapistId",
                table: "TherapistCategories",
                column: "TherapistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TherapistCategories");
        }
    }
}
