using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Location = table.Column<string>(maxLength: 1000, nullable: true),
                    TeacherName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    ModifiedAt = table.Column<DateTime>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Age = table.Column<int>(nullable: false),
                    GPA = table.Column<float>(nullable: false),
                    ClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_ClassId_Name",
                table: "Students",
                columns: new[] { "ClassId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");
        }
    }
}
