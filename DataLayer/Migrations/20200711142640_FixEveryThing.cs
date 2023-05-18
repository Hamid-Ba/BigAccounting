using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class FixEveryThing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creditors",
                columns: table => new
                {
                    CreditorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    GetMoney = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creditors", x => x.CreditorID);
                });

            migrationBuilder.CreateTable(
                name: "Deptors",
                columns: table => new
                {
                    DeptorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    DeptMoney = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deptors", x => x.DeptorID);
                });

            migrationBuilder.CreateTable(
                name: "Depts",
                columns: table => new
                {
                    DeptID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    Delete = table.Column<bool>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depts", x => x.DeptID);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    DeptorID = table.Column<int>(nullable: false),
                    CreditorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Members_Creditors_CreditorID",
                        column: x => x.CreditorID,
                        principalTable: "Creditors",
                        principalColumn: "CreditorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Members_Deptors_DeptorID",
                        column: x => x.DeptorID,
                        principalTable: "Deptors",
                        principalColumn: "DeptorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dept_Creditors",
                columns: table => new
                {
                    DeptID = table.Column<int>(nullable: false),
                    CreditorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dept_Creditors", x => new { x.DeptID, x.CreditorID });
                    table.ForeignKey(
                        name: "FK_Dept_Creditors_Creditors_CreditorID",
                        column: x => x.CreditorID,
                        principalTable: "Creditors",
                        principalColumn: "CreditorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dept_Creditors_Depts_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Depts",
                        principalColumn: "DeptID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dept_Deptors",
                columns: table => new
                {
                    DeptID = table.Column<int>(nullable: false),
                    DeptorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dept_Deptors", x => new { x.DeptID, x.DeptorID });
                    table.ForeignKey(
                        name: "FK_Dept_Deptors_Depts_DeptID",
                        column: x => x.DeptID,
                        principalTable: "Depts",
                        principalColumn: "DeptID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dept_Deptors_Deptors_DeptorID",
                        column: x => x.DeptorID,
                        principalTable: "Deptors",
                        principalColumn: "DeptorID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dept_Creditors_CreditorID",
                table: "Dept_Creditors",
                column: "CreditorID");

            migrationBuilder.CreateIndex(
                name: "IX_Dept_Deptors_DeptorID",
                table: "Dept_Deptors",
                column: "DeptorID");

            migrationBuilder.CreateIndex(
                name: "IX_Members_CreditorID",
                table: "Members",
                column: "CreditorID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_DeptorID",
                table: "Members",
                column: "DeptorID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dept_Creditors");

            migrationBuilder.DropTable(
                name: "Dept_Deptors");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Depts");

            migrationBuilder.DropTable(
                name: "Creditors");

            migrationBuilder.DropTable(
                name: "Deptors");
        }
    }
}
