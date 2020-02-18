using Microsoft.EntityFrameworkCore.Migrations;

namespace cReg_WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedCourses");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Students",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "majorId",
                table: "Students",
                newName: "MajorId");

            migrationBuilder.RenameColumn(
                name: "studentId",
                table: "Students",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "facultyName",
                table: "Faculties",
                newName: "FacultyName");

            migrationBuilder.RenameColumn(
                name: "facultyId",
                table: "Faculties",
                newName: "FacultyId");

            migrationBuilder.RenameColumn(
                name: "courseName",
                table: "Courses",
                newName: "CourseName");

            migrationBuilder.RenameColumn(
                name: "courseId",
                table: "Courses",
                newName: "CourseId");

            migrationBuilder.AddColumn<string>(
                name: "CourseDescription",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditHours",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Space",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enrolled",
                columns: table => new
                {
                    EnrollId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrolled", x => x.EnrollId);
                    table.ForeignKey(
                        name: "FK_Enrolled_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrolled_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prerequisites",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    PrerequisiteId = table.Column<int>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    PrerequisiteCourseCourseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisites", x => new { x.CourseId, x.PrerequisiteId });
                    table.ForeignKey(
                        name: "FK_Prerequisites_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prerequisites_Courses_PrerequisiteCourseCourseId",
                        column: x => x.PrerequisiteCourseCourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Required",
                columns: table => new
                {
                    FacultyId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Required", x => new { x.FacultyId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_Required_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Required_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "FacultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_CourseId",
                table: "Enrolled",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolled_StudentId",
                table: "Enrolled",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisites_PrerequisiteCourseCourseId",
                table: "Prerequisites",
                column: "PrerequisiteCourseCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Required_CourseId",
                table: "Required",
                column: "CourseId");
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
                name: "CourseDescription",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CreditHours",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Space",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Students",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MajorId",
                table: "Students",
                newName: "majorId");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Students",
                newName: "studentId");

            migrationBuilder.RenameColumn(
                name: "FacultyName",
                table: "Faculties",
                newName: "facultyName");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Faculties",
                newName: "facultyId");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Courses",
                newName: "courseName");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Courses",
                newName: "courseId");

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
