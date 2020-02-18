using Microsoft.EntityFrameworkCore.Migrations;

namespace cReg_WebApp.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedCourses");

            migrationBuilder.AddColumn<string>(
                name: "courseDescription",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "creditHours",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "date",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "space",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enrolled",
                columns: table => new
                {
                    enrollId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseId = table.Column<int>(nullable: false),
                    studentId = table.Column<int>(nullable: false),
                    completed = table.Column<bool>(nullable: false),
                    grade = table.Column<int>(nullable: false),
                    rating = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolled", x => x.enrollId);
                    table.ForeignKey(
                        name: "FK_Enrolled_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrolled_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prerequisites",
                columns: table => new
                {
                    courseId = table.Column<int>(nullable: false),
                    prerequisiteId = table.Column<int>(nullable: false),
                    grade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisites", x => new { x.courseId, x.prerequisiteId });
                    table.ForeignKey(
                        name: "FK_Prerequisites_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisites_Courses_prerequisiteId",
                        column: x => x.prerequisiteId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Required",
                columns: table => new
                {
                    facultyId = table.Column<int>(nullable: false),
                    courseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Required", x => new { x.facultyId, x.courseId });
                    table.ForeignKey(
                        name: "FK_Required_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Required_Faculties_facultyId",
                        column: x => x.facultyId,
                        principalTable: "Faculties",
                        principalColumn: "facultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_courseId",
                table: "Enrolled",
                column: "courseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_studentId",
                table: "Enrolled",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisites_prerequisiteId",
                table: "Prerequisites",
                column: "prerequisiteId");

            migrationBuilder.CreateIndex(
                name: "IX_Required_courseId",
                table: "Required",
                column: "courseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrolled");

            migrationBuilder.DropTable(
                name: "Prerequisites");

            migrationBuilder.DropTable(
                name: "Required");

            migrationBuilder.DropColumn(
                name: "courseDescription",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "creditHours",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "date",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "space",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CompletedCourses",
                columns: table => new
                {
                    studentId = table.Column<int>(type: "int", nullable: false),
                    courseId = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    grade = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedCourses", x => new { x.studentId, x.courseId });
                    table.ForeignKey(
                        name: "FK_CompletedCourses_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "courseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompletedCourses_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedCourses_courseId",
                table: "CompletedCourses",
                column: "courseId");
        }
    }
}
