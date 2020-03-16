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
                    facultyName = table.Column<string>(nullable: true),
                    graduateCreditHours = table.Column<int>(nullable: false)
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
                    { "0c2ef94f-e8a5-45a1-82f3-c010036b247f", "8cc67b73-dde7-4118-8bc1-2933e1d5b5c8", "Student", "STUDENT" },
                    { "0ff50e41-b7ce-458f-be4c-3b342018151d", "237c1c67-2feb-420c-abb0-9ebc6ef8c3d3", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "courseId", "courseDescription", "courseName", "creditHours", "date", "space" },
                values: new object[,]
                {
                    { 25, "Introduction to Macroeconomic Principles", "ECON 1020", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 24, "Introduction to Microeconomic Principles", "ECON 1010", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 23, "Basic Statistical Analysis 2", "STAT 2000", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 22, "Introduction to Calculus", "MATH 1500", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 21, "Basic Statistical Analysis 1", "STAT 1000", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 20, "Calculus 2", "MATH 1700", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 19, "Database Implementation", "COMP 4380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 18, "Machine Learning", "COMP 4360", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 17, "Professional Practice in Computer Science", "COMP 4620", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 16, "Software Engineer 2", "COMP 4350", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 15, "Computer Graphics", "COMP 4490", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 14, "Databases Concepts and Usage", "COMP 3380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 13, "Artificial Intelligence 1", "COMP 3190", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 12, "Computer Organization", "COMP 3370", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 11, "Operating Systems", "COMP 3430", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 10, "Analysis of Algorithms and Data Structures", "COMP 3170", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 9, "Software Engineer 1", "COMP 3350", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 8, "Graphics 1", "COMP 3490", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 7, "Object Orientation", "COMP 2150", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 6, "Introduction to Computer Systems", "COMP 2280", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 5, "Analysis of Algorithms", "COMP 2080", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 4, "Program Practice", "COMP 2160", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 3, "Data Structure and Algorithm", "COMP 2140", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 2, "An Introduction to Computer Science 2", "COMP 1020", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 1, "An Introduction to Computer Science 1", "COMP 1010", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 26, "An Introduction to Modern World History: 1800 - Present(M)", "HIST 1380", 3, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 }
                });

            migrationBuilder.InsertData(
                table: "Faculties",
                columns: new[] { "facultyId", "facultyName", "graduateCreditHours" },
                values: new object[] { 1, "Computer Science", 60 });

            migrationBuilder.InsertData(
                table: "Prerequisites",
                columns: new[] { "courseId", "prerequisiteId", "grade" },
                values: new object[,]
                {
                    { 2, 1, 60 },
                    { 19, 14, 60 },
                    { 19, 11, 60 },
                    { 18, 10, 60 },
                    { 17, 9, 60 },
                    { 16, 9, 60 },
                    { 15, 8, 60 },
                    { 14, 3, 60 },
                    { 13, 3, 60 },
                    { 12, 6, 60 },
                    { 11, 4, 60 },
                    { 18, 13, 70 },
                    { 10, 3, 70 },
                    { 9, 7, 60 },
                    { 8, 3, 60 },
                    { 7, 3, 60 },
                    { 6, 4, 60 },
                    { 6, 3, 60 },
                    { 5, 3, 60 },
                    { 4, 2, 60 },
                    { 3, 2, 60 },
                    { 11, 6, 60 }
                });

            migrationBuilder.InsertData(
                table: "Required",
                columns: new[] { "facultyId", "courseId" },
                values: new object[,]
                {
                    { 1, 17 },
                    { 1, 12 },
                    { 1, 11 },
                    { 1, 7 },
                    { 1, 6 },
                    { 1, 4 },
                    { 1, 3 },
                    { 1, 2 },
                    { 1, 1 },
                    { 1, 5 }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "studentId", "majorId", "name" },
                values: new object[,]
                {
                    { 6, 1, "Carson Leung" },
                    { 1, 1, "John Braico" },
                    { 2, 1, "Mike Zapp" },
                    { 3, 1, "Peter Graham" },
                    { 4, 1, "Robert Guderian" },
                    { 5, 1, "Gord Boyer" },
                    { 7, 1, "Franklin Bristow" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "StudentId" },
                values: new object[,]
                {
                    { "3f1052a3-125b-43fa-94e4-2596a7daa026", 0, "4994401d-9696-4f5a-b052-203f40ee83a4", "StudentUser", null, false, false, null, null, "JB", "AQAAAAEAACcQAAAAEPQklQlzHlKmq2ilNxFDaqUV/1dNCHnUt7lUnexTkQOB9ZXPYXMsP7kPHgpohsX1Cg==", null, false, "dcfc6b9c-ba39-4020-a155-c8a46e8dc823", false, "jb", 1 },
                    { "3fe0c628-ef65-4c47-867d-40250edbfc84", 0, "bb1659c0-fd68-4d72-a6d3-0f72f7442487", "StudentUser", null, false, false, null, null, "RG", "AQAAAAEAACcQAAAAEMVkh2bzfGS4oU/80cNTkGEdTz6tFbvI2uJrvWs8qcRM5s8X9e9WP24aTlgFnAlTFA==", null, false, "f9197e6e-ee9b-475d-893d-04fb4ca02a74", false, "rg", 4 },
                    { "147b0175-c43a-40b9-8b40-733c05930e86", 0, "95d8182d-7081-4f6b-98a4-820d0a565a07", "StudentUser", null, false, false, null, null, "GB", "AQAAAAEAACcQAAAAEOKdxCmspXos64D4LyEzZYEZ7GOp6O9lTaYOq7KfiaW/1dg5mDMjs5/9KpxLBgYDrg==", null, false, "6dc85aab-fcbf-4ae2-b395-95822032a767", false, "gb", 5 },
                    { "5fa47b20-bdf9-4770-819a-6cdb9760927a", 0, "46a13df2-2685-4772-aa15-de1b07b0b15a", "StudentUser", null, false, false, null, null, "PG", "AQAAAAEAACcQAAAAEIwgc9Ba7IcJi/uNFspOHLVrnB6ZSqJv62hUzDGBv/6cQQrRNfLVC5UkrpmtHc7sVw==", null, false, "35682217-bdff-4ff4-b139-6be91ea51375", false, "pg", 3 },
                    { "0f359784-0ece-4d4b-8677-84230e0ac9da", 0, "1869929a-0e28-40d9-8f3e-5ab346638ff2", "StudentUser", null, false, false, null, null, "CL", "AQAAAAEAACcQAAAAEN8y/GQkS6A37Co3vWpwcjQfIoOaWQUvVkBy54e2VK9VFvgz7fyZ/7Nbex+DPCaolg==", null, false, "2c686614-0797-42f1-9767-21a0762ea3d4", false, "cl", 6 },
                    { "bdd89c5b-94d8-493f-9677-cf348070ffbb", 0, "78fbfabb-a94f-4e51-aa94-7c8cb7d98de1", "StudentUser", null, false, false, null, null, "MZ", "AQAAAAEAACcQAAAAEJs2FlMTJTdCUzniGhSVA9Nhtg8E4VbhRPnw8rBtgmxs9ZSDbCrtNwnp8bEq1k/Dtw==", null, false, "056aa746-4b37-493a-8bff-30154dabb557", false, "mz", 2 },
                    { "dde9f388-17ae-494f-b36d-f9d7e0194570", 0, "704807a2-79e1-421e-9cdb-e72b6208e771", "StudentUser", null, false, false, null, null, "FB", "AQAAAAEAACcQAAAAEGTrDG1/O/qDGZFDXVaqpQL+V5BIDP20aS9NMbJkcoROH48xOtCLnv/KLCSeMJnCfg==", null, false, "91e43317-9eae-4156-bf40-5e343bea6b03", false, "fb", 7 }
                });

            migrationBuilder.InsertData(
                table: "Enrolled",
                columns: new[] { "enrollId", "comment", "completed", "courseId", "grade", "rating", "studentId" },
                values: new object[,]
                {
                    { 41, null, true, 3, 98, null, 6 },
                    { 27, null, true, 4, 70, null, 3 },
                    { 28, null, false, 6, null, null, 3 },
                    { 45, "Semi-entertaining lectures. Interesting fun guy to talk to but thinks a midterm average of apx. 60% is normal and fine.", true, 2, 68, 67, 7 },
                    { 29, "Excellent course", true, 1, 90, 87, 4 },
                    { 30, "Lectures were super easy to understand.", true, 2, 80, 90, 4 },
                    { 31, null, true, 3, 90, null, 4 },
                    { 32, null, true, 4, 75, null, 4 },
                    { 33, null, false, 6, null, null, 4 },
                    { 44, "Notes aren't good for studying.", true, 1, 75, 68, 7 },
                    { 34, "Boring lectures. Easy mid term, harder final-assignments took a lot of time. ", true, 1, 72, 58, 5 },
                    { 35, "Don't bother going to the lectures.", true, 2, 85, 68, 5 },
                    { 36, null, true, 3, 90, null, 5 },
                    { 37, null, true, 4, 75, null, 5 },
                    { 38, null, false, 6, null, null, 5 },
                    { 43, null, false, 6, null, null, 6 },
                    { 39, "The course is knowledgeable and well prepared.", true, 1, 98, 85, 6 },
                    { 40, "I enjoyed this class, as I usually do.", true, 2, 97, 98, 6 },
                    { 42, null, true, 4, 88, null, 6 },
                    { 26, null, true, 3, 80, null, 3 },
                    { 24, "Very excellent professor. Very clear, informative, and very helpful. He knows his stuff very well! Can manage to balance his two positions in the Faculty of Science and Computer Science very well.", true, 1, 90, 87, 3 },
                    { 46, null, true, 3, 74, null, 7 },
                    { 1, "I like that course", true, 1, 80, 90, 1 },
                    { 2, "Very good", true, 2, 90, 80, 1 },
                    { 3, "Very interesting prof. Does a good job breaking down ideas into chunks you can easily understand", true, 3, 95, 75, 1 },
                    { 4, "I like the instructor", true, 4, 95, 76, 1 },
                    { 5, "Very good", true, 5, 85, 82, 1 },
                    { 6, "Franklin cannot take this one", true, 6, 90, 87, 1 },
                    { 7, "Excellent", true, 7, 98, 93, 1 },
                    { 8, "Good instructor", true, 8, 97, 80, 1 },
                    { 9, null, true, 9, 96, null, 1 },
                    { 10, null, true, 11, 92, null, 1 },
                    { 11, null, true, 15, 87, null, 1 },
                    { 12, null, true, 14, 88, null, 1 },
                    { 13, "NEVER AGAIN", true, 1, 70, 70, 2 },
                    { 14, "You will either love the course or hate time", true, 2, 73, 80, 2 },
                    { 15, "Awesome", true, 3, 85, 90, 2 },
                    { 16, "Very good professor, the best one I've had withery good professor, the best one I've had with", true, 4, 75, 60, 2 },
                    { 17, "The course thinks that everyone learns the same way (auditory). ", true, 7, 70, 40, 2 },
                    { 18, "Hard grader", true, 8, 50, 20, 2 },
                    { 19, "Nice Instructor", true, 9, 68, 10, 2 },
                    { 20, null, true, 10, 65, null, 2 },
                    { 21, null, true, 13, 65, null, 2 },
                    { 22, null, true, 14, 50, null, 2 },
                    { 23, null, false, 6, null, null, 2 },
                    { 25, "Markers marks unnecessarily harsh. Like his intention is to fail you not to grade you. Nevertheless, he does do his best, and he is better than many.", true, 2, 70, 90, 3 },
                    { 47, null, true, 4, 72, null, 7 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "3f1052a3-125b-43fa-94e4-2596a7daa026", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" },
                    { "bdd89c5b-94d8-493f-9677-cf348070ffbb", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" },
                    { "5fa47b20-bdf9-4770-819a-6cdb9760927a", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" },
                    { "3fe0c628-ef65-4c47-867d-40250edbfc84", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" },
                    { "147b0175-c43a-40b9-8b40-733c05930e86", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" },
                    { "0f359784-0ece-4d4b-8677-84230e0ac9da", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" },
                    { "dde9f388-17ae-494f-b36d-f9d7e0194570", "0c2ef94f-e8a5-45a1-82f3-c010036b247f" }
                });

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
