using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор курса"),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название курса"),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false, comment: "Описание курса")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_courses_id", x => x.id);
                },
                comment: "Учебные курсы");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Email пользователя"),
                    password_hash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: "Хэш пароля пользователя")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users_id", x => x.id);
                },
                comment: "Пользователи платформы");

            migrationBuilder.CreateTable(
                name: "lessons",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор урока"),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: "Название урока"),
                    content = table.Column<string>(type: "text", nullable: false, comment: "Содержимое урока"),
                    course_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор курса (FK)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lessons_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_lessons_course_id",
                        column: x => x.course_id,
                        principalSchema: "public",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Уроки, относящиеся к курсу");

            migrationBuilder.CreateTable(
                name: "enrollments",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя (FK)"),
                    course_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор курса (FK)"),
                    enrolled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP", comment: "Дата подписки пользователя на курс")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_enrollments", x => new { x.user_id, x.course_id });
                    table.ForeignKey(
                        name: "fk_enrollments_course_id",
                        column: x => x.course_id,
                        principalSchema: "public",
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_enrollments_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Связь пользователей с курсами (многие ко многим)");

            migrationBuilder.CreateTable(
                name: "user_profiles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор профиля"),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Имя пользователя"),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "Фамилия пользователя"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя (FK)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_profiles_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_profile_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Расширенная информация о пользователе");

            migrationBuilder.CreateIndex(
                name: "ix_courses_title",
                schema: "public",
                table: "courses",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_enrollments_course_id",
                schema: "public",
                table: "enrollments",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_enrollments_enrolled_at",
                schema: "public",
                table: "enrollments",
                column: "enrolled_at");

            migrationBuilder.CreateIndex(
                name: "ix_lessons_course_id",
                schema: "public",
                table: "lessons",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ux_user_profiles_user_id",
                schema: "public",
                table: "user_profiles",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_users_email",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "enrollments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "lessons",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_profiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "courses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
