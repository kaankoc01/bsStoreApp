using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "395d174b-50e8-4567-8a65-d5329274520b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "407043f2-f09b-4bc7-851e-d4470c7c86de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "501f9c0f-cfdb-4bef-9027-5b36a4a14236");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ce9ed2c-cc39-4126-bd04-429b6b18a107", "221b1b09-2ac7-4bac-8483-a5179ad2c971", "User", "USER" },
                    { "25f37163-b06e-46b7-80fa-96ef3e321618", "13c438bf-64bc-42ce-9f7d-3c3f8e018fe2", "Editor", "EDITOR" },
                    { "cb68933e-490e-4f1e-abbc-28f70e1184d9", "38a65944-cea5-4598-a38b-9ad0a29c000a", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Network" },
                    { 3, "Database Management Systems" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ce9ed2c-cc39-4126-bd04-429b6b18a107");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25f37163-b06e-46b7-80fa-96ef3e321618");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb68933e-490e-4f1e-abbc-28f70e1184d9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "395d174b-50e8-4567-8a65-d5329274520b", "0d26eb95-6e2d-4dc5-884a-2514ee3938f7", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "407043f2-f09b-4bc7-851e-d4470c7c86de", "262dfb36-555b-4233-9c40-a04f66bdd4ef", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "501f9c0f-cfdb-4bef-9027-5b36a4a14236", "31340343-fc59-4a0c-9812-4fdc6852a0f9", "Admin", "ADMIN" });
        }
    }
}
