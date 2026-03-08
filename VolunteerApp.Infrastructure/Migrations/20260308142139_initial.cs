using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VolunteerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "VolunteerApp");

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    SpokenLanguage = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "title_definitions",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CriteriaType = table.Column<int>(type: "integer", nullable: false),
                    RequiredValue = table.Column<int>(type: "integer", nullable: false),
                    ServiceFilterId = table.Column<int>(type: "integer", nullable: true),
                    TitlePadge = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_title_definitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_title_definitions_ServiceTypes_ServiceFilterId",
                        column: x => x.ServiceFilterId,
                        principalSchema: "VolunteerApp",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "students",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Bio = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_students_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    TotalXp = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volunteers_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    VolunteerProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reviews_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reviews_Volunteers_VolunteerProfileId",
                        column: x => x.VolunteerProfileId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Volunteers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                schema: "VolunteerApp",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChatRoomId = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ServiceTypeId = table.Column<int>(type: "integer", nullable: false),
                    Descreption = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tickets_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalSchema: "VolunteerApp",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tickets_students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "VolunteerApp",
                        principalTable: "students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "volunteer_achievments",
                schema: "VolunteerApp",
                columns: table => new
                {
                    VolunteerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TitleDefinitionId = table.Column<int>(type: "integer", nullable: false),
                    UnlockedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_volunteer_achievments", x => new { x.VolunteerId, x.TitleDefinitionId });
                    table.ForeignKey(
                        name: "FK_volunteer_achievments_ServiceTypes_TitleDefinitionId",
                        column: x => x.TitleDefinitionId,
                        principalSchema: "VolunteerApp",
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_volunteer_achievments_Volunteers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalSchema: "VolunteerApp",
                        principalTable: "Volunteers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reviews_AuthorId_VolunteerId",
                schema: "VolunteerApp",
                table: "reviews",
                columns: new[] { "AuthorId", "VolunteerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reviews_VolunteerId",
                schema: "VolunteerApp",
                table: "reviews",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_VolunteerProfileId",
                schema: "VolunteerApp",
                table: "reviews",
                column: "VolunteerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_students_UserId",
                schema: "VolunteerApp",
                table: "students",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ChatRoomId",
                schema: "VolunteerApp",
                table: "tickets",
                column: "ChatRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_ServiceTypeId",
                schema: "VolunteerApp",
                table: "tickets",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_StudentId",
                schema: "VolunteerApp",
                table: "tickets",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_tickets_VolunteerId",
                schema: "VolunteerApp",
                table: "tickets",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_title_definitions_ServiceFilterId",
                schema: "VolunteerApp",
                table: "title_definitions",
                column: "ServiceFilterId");

            migrationBuilder.CreateIndex(
                name: "IX_volunteer_achievments_TitleDefinitionId",
                schema: "VolunteerApp",
                table: "volunteer_achievments",
                column: "TitleDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_UserId",
                schema: "VolunteerApp",
                table: "Volunteers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "tickets",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "title_definitions",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "volunteer_achievments",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "students",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "ServiceTypes",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "Volunteers",
                schema: "VolunteerApp");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "VolunteerApp");
        }
    }
}
