using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cRegis.Core.Migrations
{
    public partial class initialCreate : Migration
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
                    { "5bf845c9-409d-42a6-bb95-0df301450651", "798db454-eb55-4c6e-8f98-f69f217519c6", "Student", "STUDENT" },
                    { "c5f107cd-ad92-4729-99ab-65233067ca02", "18bddeff-a29c-4089-a21f-2a516f2f651a", "Admin", "ADMIN" }
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
                    { "e5fb98af-bb6b-44ed-a2d8-231323d46584", 0, "39a67569-49f6-4cd2-8037-040a0aa1c7bd", "StudentUser", null, false, false, null, null, "FB", "AQAAAAEAACcQAAAAEP2ezRDiwP1kOTo6LufC89dL7uU+eCiIMdZmwtllHdjea3IXIEvNDcV9FGezSJGVkQ==", null, false, "816dd282-ff8a-4624-986a-1daed948876c", false, "fb", 7 },
                    { "94a5dec5-a5e6-4e75-8ae1-f154d1c5755b", 0, "d8b0b5b4-dad3-453c-b917-d827d2ee0817", "StudentUser", null, false, false, null, null, "MZ", "AQAAAAEAACcQAAAAEIgFm3E0C5pRL9hqJPp4E1OvQE5fc7vSlNsx8bk8LRNGwqca0EPvJjVRYBattXn8Nw==", null, false, "b4fc79a1-7db7-4ebd-a047-0f5c9809e3f7", false, "mz", 2 },
                    { "90367e1e-ee60-48e3-aff1-5697ce873757", 0, "9e14bd96-bae2-4d43-87d1-f483ff665305", "StudentUser", null, false, false, null, null, "PG", "AQAAAAEAACcQAAAAEM9SwYG+/QXsL6LpOPv0ietvglMD+L457jIHLBjXGWTZKsEZj/kPjCokHpwHbgeDWg==", null, false, "1c04858b-fc84-447c-8996-e8f4741c4909", false, "pg", 3 },
                    { "5dec39d5-c438-4414-bc4a-5690d3f934b2", 0, "82d736a2-0eea-4d9d-917c-50f8354aabb9", "StudentUser", null, false, false, null, null, "CL", "AQAAAAEAACcQAAAAEJHifQNzJkBgraXUuzudy3WWTsf+yXmXTCV+B3iy6ebWU/Ls3u+cuESm+V6GvT7axA==", null, false, "33b6a139-28f1-47a7-839f-aa743b082a14", false, "cl", 6 },
                    { "52f71399-d137-4509-88eb-1e703f3fc115", 0, "e4281d41-3082-4670-bbc4-c2354e1302ee", "StudentUser", null, false, false, null, null, "RG", "AQAAAAEAACcQAAAAENCsU2DVAoeH+A4zxUunjBfsrohr0lo/tmS1mNpLra4NFNh9JuG+jx6htzDIZIqMmw==", null, false, "309d8252-0c78-45eb-8180-96472feec441", false, "rg", 4 },
                    { "36ab1443-e18b-4d4a-9d66-33af9cedd13a", 0, "c8e52633-8c95-4b2c-88af-c2315f1c8439", "StudentUser", null, false, false, null, null, "GB", "AQAAAAEAACcQAAAAEPOVup1v+4te+0L1/w30gT4OtBBvDg4zlhUIvEbDe0twOI+WKZhmhS61Lzq3BTC0HA==", null, false, "7f653116-105c-4e64-b185-b0ebf34c04da", false, "gb", 5 },
                    { "7b5b426b-b93f-4d20-ae15-d7128f9687a4", 0, "9244ba2e-2d4f-4adf-93f2-ec51d38d13a9", "StudentUser", null, false, false, null, null, "JB", "AQAAAAEAACcQAAAAELo8+1lm2Ud+QVbzxQqaHClMsvy0jwA6+T+6rjX/BtwgFNVawYDhEgGeNfWvz68qTQ==", null, false, "5827b175-1dcc-4461-bc09-3dc0941fb964", false, "jb", 1 }
                });

            migrationBuilder.InsertData(
                table: "Enrolled",
                columns: new[] { "enrollId", "comment", "completed", "courseId", "grade", "rating", "studentId" },
                values: new object[,]
                {
                    { 29, "Excellent course", true, 1, 90, 87, 4 },
                    { 30, "Lectures were super easy to understand.", true, 2, 80, 90, 4 },
                    { 31, null, true, 3, 90, null, 4 },
                    { 32, null, true, 4, 75, null, 4 },
                    { 33, null, false, 6, null, null, 4 },
                    { 34, "Boring lectures. Easy mid term, harder final-assignments took a lot of time. ", true, 1, 72, 58, 5 },
                    { 35, "Don't bother going to the lectures.", true, 2, 85, 68, 5 },
                    { 37, null, true, 4, 75, null, 5 },
                    { 28, null, false, 6, null, null, 3 },
                    { 38, null, false, 6, null, null, 5 },
                    { 39, "The course is knowledgeable and well prepared.", true, 1, 98, 85, 6 },
                    { 40, "I enjoyed this class, as I usually do.", true, 2, 97, 98, 6 },
                    { 41, null, true, 3, 98, null, 6 },
                    { 42, null, true, 4, 88, null, 6 },
                    { 43, null, false, 6, null, null, 6 },
                    { 44, "Notes aren't good for studying.", true, 1, 75, 68, 7 },
                    { 45, "Semi-entertaining lectures. Interesting fun guy to talk to but thinks a midterm average of apx. 60% is normal and fine.", true, 2, 68, 67, 7 },
                    { 46, null, true, 3, 74, null, 7 },
                    { 36, null, true, 3, 90, null, 5 },
                    { 27, null, true, 4, 70, null, 3 },
                    { 25, "Markers marks unnecessarily harsh. Like his intention is to fail you not to grade you. Nevertheless, he does do his best, and he is better than many.", true, 2, 70, 90, 3 },
                    { 47, null, true, 4, 72, null, 7 },
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
                    { 24, "Very excellent professor. Very clear, informative, and very helpful. He knows his stuff very well! Can manage to balance his two positions in the Faculty of Science and Computer Science very well.", true, 1, 90, 87, 3 },
                    { 26, null, true, 3, 80, null, 3 },
                    { 1, "I like that course", true, 1, 80, 90, 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { "7b5b426b-b93f-4d20-ae15-d7128f9687a4", "5bf845c9-409d-42a6-bb95-0df301450651" },
                    { "94a5dec5-a5e6-4e75-8ae1-f154d1c5755b", "5bf845c9-409d-42a6-bb95-0df301450651" },
                    { "90367e1e-ee60-48e3-aff1-5697ce873757", "5bf845c9-409d-42a6-bb95-0df301450651" },
                    { "52f71399-d137-4509-88eb-1e703f3fc115", "5bf845c9-409d-42a6-bb95-0df301450651" },
                    { "36ab1443-e18b-4d4a-9d66-33af9cedd13a", "5bf845c9-409d-42a6-bb95-0df301450651" },
                    { "5dec39d5-c438-4414-bc4a-5690d3f934b2", "5bf845c9-409d-42a6-bb95-0df301450651" },
                    { "e5fb98af-bb6b-44ed-a2d8-231323d46584", "5bf845c9-409d-42a6-bb95-0df301450651" }
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
