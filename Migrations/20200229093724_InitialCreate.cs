using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cReg_WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    courseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseName = table.Column<string>(nullable: true),
                    courseDescription = table.Column<string>(nullable: true),
                    creditHours = table.Column<int>(nullable: false),
                    space = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.courseId);
                });

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    facultyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    facultyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.facultyId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
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

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    studentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    majorId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.studentId);
                    table.ForeignKey(
                        name: "FK_Students_Faculties_majorId",
                        column: x => x.majorId,
                        principalTable: "Faculties",
                        principalColumn: "facultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "studentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrolled",
                columns: table => new
                {
                    enrollId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    courseId = table.Column<int>(nullable: false),
                    studentId = table.Column<int>(nullable: false),
                    completed = table.Column<bool>(nullable: false),
                    grade = table.Column<int>(nullable: true),
                    rating = table.Column<int>(nullable: true),
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b396b113-c194-48b1-b15f-2d7f9bf33a03", "32e9f3b4-6e42-4d6e-8c71-4bcb3ec3c2da", "Student", "STUDENT" },
                    { "8a549b28-c5ba-4097-a095-301ae1e2efbb", "05843e40-1f46-4d28-aa7f-c043ab85b21e", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "courseId", "courseDescription", "courseName", "creditHours", "date", "space" },
                values: new object[,]
                {
                    { 1, "Database Implementation", "COMP 4380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 80 },
                    { 2, "Software Engineering", "COMP 4350", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 80 },
                    { 3, "Computer Graphics", "COMP 4490", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 80 },
                    { 4, "Machine Learning", "COMP 4360", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 80 }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "facultyId", "facultyName" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Engineering" },
                    { 3, "Arts" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "majorId", "name" },
                values: new object[] { 1, 1, "John Braico" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "majorId", "name" },
                values: new object[] { 2, 2, "Mike Zapp" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "majorId", "name" },
                values: new object[] { 3, 3, "Peter Graham" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "StudentId" },
                values: new object[,]
                {
                    { "ec83678c-819d-433b-97f3-608abb81c533", 0, "9933ac73-4054-4c5e-8671-38c5801cf50e", "StudentUser", null, false, false, null, null, "JB", "AQAAAAEAACcQAAAAELE0o15PniuynlGJs1846uUlfoybSOxz3H3tjQG8VI7JyoGihPRio9oRtcHea7Damg==", null, false, "01d8a83a-ae41-44f7-8abb-6fcc85549970", false, "jb", 1 },
                    { "d9ecc188-2264-46d5-bacb-0f764b12ff7f", 0, "4a936baa-f80d-4553-a222-d3bf9811dca2", "StudentUser", null, false, false, null, null, "MZ", "AQAAAAEAACcQAAAAEAkbP/vx5GVyW6K+I7pK9s1E7L83BFEsszSz6dD8I9UceiceXeObNV432RszctB3OQ==", null, false, "a210d01b-3035-4f38-8081-137430c80783", false, "mz", 2 },
                    { "10d18835-e05b-480f-9693-ed39d15dd06e", 0, "6fcd049a-48dd-4b67-9048-985073d864d6", "StudentUser", null, false, false, null, null, "PG", "AQAAAAEAACcQAAAAEMltYp6VY0+92ROhcZGmGBNF8Kw6xZ/rCoUWdRRyjhWjUilo+uy3BUcAItnCVLbwQg==", null, false, "4d1db9ed-2f0f-4cc2-bc99-8ad37674bd91", false, "pg", 3 }
                });

            migrationBuilder.InsertData(
                table: "Enrolled",
                columns: new[] { "enrollId", "comment", "completed", "courseId", "grade", "rating", "studentId" },
                values: new object[,]
                {
                    { 1, "", false, 1, -1, -1, 1 },
                    { 2, "Very good", true, 2, 80, 80, 1 },
                    { 3, "", false, 1, -1, -1, 2 },
                    { 4, "", true, 3, 50, -1, 2 },
                    { 5, "", false, 2, -1, -1, 3 },
                    { 6, "Excellent", true, 4, 70, 90, 3 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "ec83678c-819d-433b-97f3-608abb81c533", "b396b113-c194-48b1-b15f-2d7f9bf33a03" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "d9ecc188-2264-46d5-bacb-0f764b12ff7f", "b396b113-c194-48b1-b15f-2d7f9bf33a03" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "10d18835-e05b-480f-9693-ed39d15dd06e", "b396b113-c194-48b1-b15f-2d7f9bf33a03" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentId",
                table: "AspNetUsers",
                column: "StudentId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Students_majorId",
                table: "Students",
                column: "majorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Enrolled");

            migrationBuilder.DropTable(
                name: "Prerequisites");

            migrationBuilder.DropTable(
                name: "Required");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Faculties");
        }
    }
}
