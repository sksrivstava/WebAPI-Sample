using Microsoft.EntityFrameworkCore.Migrations;

namespace netcore_reactapi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employeedb",
                columns: table => new
                {
                    empid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    empname = table.Column<string>(type: "varchar(100)", nullable: true),
                    address = table.Column<string>(type: "varchar(100)", nullable: true),
                    mobile = table.Column<string>(type: "varchar(100)", nullable: true),
                    age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employeedb", x => x.empid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employeedb");
        }
    }
}
