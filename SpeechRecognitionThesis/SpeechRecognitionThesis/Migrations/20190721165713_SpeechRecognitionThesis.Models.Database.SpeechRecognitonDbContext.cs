using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeechRecognitionThesis.Migrations
{
    public partial class SpeechRecognitionThesisModelsDatabaseSpeechRecognitonDbContext : Migration
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
                    ActiveAccountState = table.Column<int>(nullable: false)
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
                columns: new[] { "UserId", "ActiveAccountState", "CreateAccountDate", "Email", "LastUpdateAccountDate", "NickName", "Password" },
                values: new object[,]
                {
                    { 1L, 1, "30.05.2019 00:00:00", "bas@gmail.com", "20.06.2019 00:00:00", "SuperBass", "0x07e547d9586f6a73f73fbac0435ed76951218fb7d0c8d788a309d785436bbb642e93a252a954f23912547d1e8a3b5ed6e1bfd7097821233fa0538f3db854fee6" },
                    { 2L, 1, "21.05.2019 00:00:00", "robert@mail.com", "23.06.2019 00:00:00", " RobertSon", "0x07e547d9586f6a73f73fbac0435ed76951218fb7d0c8d788a309d785436bbb642e93a252a954f23912547d1e8a3b5ed6e1bfd7097821233fa0538f3db854fee6" }
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
