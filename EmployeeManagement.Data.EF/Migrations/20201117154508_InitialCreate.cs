using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Data.EF.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOJ = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerID = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Employees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "CreateBy", "CreateDate", "Name", "UpdateBy", "UpdateDate" },
                values: new object[] { 1, null, null, "HR", null, null });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "CreateBy", "CreateDate", "Name", "UpdateBy", "UpdateDate" },
                values: new object[] { 2, null, null, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "ID", "CreateBy", "CreateDate", "Name", "UpdateBy", "UpdateDate" },
                values: new object[] { 3, null, null, "GE", null, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Address", "CreateBy", "CreateDate", "DOJ", "DepartmentID", "FirstName", "LastName", "ManagerID", "UpdateBy", "UpdateDate" },
                values: new object[] { 1, "coimbatore", null, null, new DateTime(2020, 11, 17, 21, 15, 7, 649, DateTimeKind.Local).AddTicks(9365), 1, "Vineeth", "Viswanathan", null, null, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Address", "CreateBy", "CreateDate", "DOJ", "DepartmentID", "FirstName", "LastName", "ManagerID", "UpdateBy", "UpdateDate" },
                values: new object[] { 4, "coimbatore", null, null, new DateTime(2020, 11, 17, 21, 15, 7, 652, DateTimeKind.Local).AddTicks(7719), 2, "Krishna", "v", null, null, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Address", "CreateBy", "CreateDate", "DOJ", "DepartmentID", "FirstName", "LastName", "ManagerID", "UpdateBy", "UpdateDate" },
                values: new object[] { 2, "coimbatore", null, null, new DateTime(2020, 11, 17, 21, 15, 7, 652, DateTimeKind.Local).AddTicks(7273), 1, "Krishna", "v", 1, null, null });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Address", "CreateBy", "CreateDate", "DOJ", "DepartmentID", "FirstName", "LastName", "ManagerID", "UpdateBy", "UpdateDate" },
                values: new object[] { 3, "coimbatore", null, null, new DateTime(2020, 11, 17, 21, 15, 7, 652, DateTimeKind.Local).AddTicks(7703), 1, "Krishna", "v", 2, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentID",
                table: "Employees",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerID",
                table: "Employees",
                column: "ManagerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
