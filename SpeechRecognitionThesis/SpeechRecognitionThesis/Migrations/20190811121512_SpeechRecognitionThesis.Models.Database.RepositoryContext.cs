using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeechRecognitionThesis.Migrations
{
    public partial class SpeechRecognitionThesisModelsDatabaseRepositoryContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    InsertionDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NickName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 512, nullable: true),
                    Email = table.Column<string>(nullable: true),
                    CreateAccountDate = table.Column<string>(nullable: true),
                    LastUpdateAccountDate = table.Column<string>(nullable: true),
                    ActiveAccountState = table.Column<int>(nullable: false),
                    IsLogged = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "AuthorId", "AuthorName", "Content", "InsertionDate", "LastUpdateDate" },
                values: new object[,]
                {
                    { 1L, 1L, "Krystian B.", "To jest artykuł 1", new DateTime(2017, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 1L, "Roman Z.", "To jest artykuł 2", new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ActiveAccountState", "CreateAccountDate", "Email", "IsLogged", "LastUpdateAccountDate", "NickName", "Password" },
                values: new object[,]
                {
                    { 1L, 1, "30.05.2019 00:00:00", "bas@gmail.com", false, "20.06.2019 00:00:00", "SuperBass", "3c54ae8854fd40631cdaabba9b9df836bb5cace38cafcfad7e9a89477300a1cbf5fb7937ee188ace530d1a27aedd4e90e69e27c60d888e6136d326e24cff1699" },
                    { 2L, 1, "21.05.2019 00:00:00", "robert@mail.com", false, "23.06.2019 00:00:00", " RobertSon", "5e50a8d4e3897e2da8f3ddef3f6d75d1c327724acf408be827e6b2115d1d0d85e9f9dbadc14387b5622405d81763029cf610422bbe4e343bb9414bba4aa38828" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
