using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Otus.Teaching.PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preferences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preferences", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    description = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer_preferences",
                columns: table => new
                {
                    customer_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    preference_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer_preferences", x => new { x.customer_id, x.preference_id });
                    table.ForeignKey(
                        name: "fk_customer_preferences_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customer_preferences_preferences_preference_id",
                        column: x => x.preference_id,
                        principalTable: "preferences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    first_name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    last_name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    applied_promocodes_count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "promo_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    code = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    service_info = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    begin_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    end_date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    partner_name = table.Column<string>(type: "TEXT", maxLength: 12, nullable: true),
                    partner_manager_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    customer_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    preference_id = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_promo_code", x => x.id);
                    table.ForeignKey(
                        name: "fk_promo_code_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_promo_code_employees_partner_manager_id",
                        column: x => x.partner_manager_id,
                        principalTable: "employees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_promo_code_preferences_preference_id",
                        column: x => x.preference_id,
                        principalTable: "preferences",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "id", "email", "first_name", "last_name" },
                values: new object[] { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), "ivan_sergeev@mail.ru", "Иван", "Петров" });

            migrationBuilder.InsertData(
                table: "preferences",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84"), "Дети" },
                    { new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), "Семья" },
                    { new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "Театр" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02"), "Администратор", "Admin" },
                    { new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665"), "Партнерский менеджер", "PartnerManager" }
                });

            migrationBuilder.InsertData(
                table: "customer_preferences",
                columns: new[] { "customer_id", "preference_id" },
                values: new object[,]
                {
                    { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new Guid("76324c47-68d2-472d-abb8-33cfa8cc0c84") },
                    { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd") },
                    { new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c") }
                });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "id", "applied_promocodes_count", "email", "first_name", "last_name", "role_id" },
                values: new object[,]
                {
                    { new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), 5, "owner@somemail.ru", "Иван", "Сергеев", new Guid("53729686-a368-4eeb-8bfa-cc69b6050d02") },
                    { new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"), 10, "andreev@somemail.ru", "Петр", "Андреев", new Guid("b0ae7aac-5493-45cd-ad16-87426a5e7665") }
                });

            migrationBuilder.InsertData(
                table: "promo_code",
                columns: new[] { "id", "begin_date", "code", "customer_id", "end_date", "partner_manager_id", "partner_name", "preference_id", "service_info" },
                values: new object[,]
                {
                    { new Guid("2d5c0b24-0f61-4ae3-ad2a-e0ded5153d09"), new DateTime(2024, 4, 20, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8925), "OSEN2024", new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new DateTime(2024, 4, 21, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8938), new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), "Рога и Копыта", new Guid("ef7f299f-92d7-459f-896e-078ed53ef99c"), "Сервисная информация1" },
                    { new Guid("33867eef-321b-4b88-a4e6-e57f77e3e57a"), new DateTime(2024, 4, 19, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8950), "LETO2024", new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new DateTime(2024, 4, 22, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8951), new Guid("451533d5-d8d5-4a11-9c7b-eb9f14e1a32f"), "Домик в Деревне", new Guid("c4bda62e-fc74-4256-a956-4760b3858cbd"), "Сервисная информация2" },
                    { new Guid("fd49d9a9-609f-4376-bfb4-14b157aae3a9"), new DateTime(2024, 4, 17, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8956), "ZIMA2024", new Guid("a6c8c6b1-4349-45b0-ab31-244740aaf0f0"), new DateTime(2024, 4, 24, 16, 0, 43, 108, DateTimeKind.Local).AddTicks(8957), new Guid("f766e2bf-340a-46ea-bff3-f1700b435895"), "Интел", null, "Сервисная информация3" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_customer_preferences_preference_id",
                table: "customer_preferences",
                column: "preference_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_role_id",
                table: "employees",
                column: "role_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_promo_code_customer_id",
                table: "promo_code",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "ix_promo_code_partner_manager_id",
                table: "promo_code",
                column: "partner_manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_promo_code_preference_id",
                table: "promo_code",
                column: "preference_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customer_preferences");

            migrationBuilder.DropTable(
                name: "promo_code");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "preferences");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
