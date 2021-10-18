using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogerator.Migrations;

public partial class InitialCreation : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Blogs",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Blogs", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Posts",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                TitleImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Contents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BlogId = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Posts", x => x.Id);
                table.ForeignKey(
                    name: "FK_Posts_Blogs_BlogId",
                    column: x => x.BlogId,
                    principalTable: "Blogs",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Posts_BlogId",
            table: "Posts",
            column: "BlogId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Posts");

        migrationBuilder.DropTable(
            name: "Blogs");
    }
}
