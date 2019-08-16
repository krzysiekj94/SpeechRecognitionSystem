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
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true),
                    InsertionDate = table.Column<DateTime>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
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
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserArticles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserRefId = table.Column<long>(nullable: false),
                    ArticleRefId = table.Column<long>(nullable: false),
                    AddArticleToUserDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserArticles_Articles_ArticleRefId",
                        column: x => x.ArticleRefId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserArticles_Users_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "AuthorId", "AuthorName", "Content", "InsertionDate", "LastUpdateDate" },
                values: new object[,]
                {
                    { 1L, 1L, "Krystian B.", "To jest artykuł 1", new DateTime(2017, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, 1L, "Roman Z.", "To jest artykuł 2", new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveAccountState", "CreateAccountDate", "Email", "IsLogged", "LastUpdateAccountDate", "NickName", "Password" },
                values: new object[,]
                {
                    { 1L, 1, "30.05.2019 00:00:00", "bas@gmail.com", false, "20.06.2019 00:00:00", "SuperBass", "3c54ae8854fd40631cdaabba9b9df836bb5cace38cafcfad7e9a89477300a1cbf5fb7937ee188ace530d1a27aedd4e90e69e27c60d888e6136d326e24cff1699" },
                    { 2L, 1, "21.05.2019 00:00:00", "robert@mail.com", false, "23.06.2019 00:00:00", " RobertSon", "5e50a8d4e3897e2da8f3ddef3f6d75d1c327724acf408be827e6b2115d1d0d85e9f9dbadc14387b5622405d81763029cf610422bbe4e343bb9414bba4aa38828" }
                });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "AddArticleToUserDate", "ArticleRefId", "UserRefId" },
                values: new object[] { 1L, "15.08.2019 20:50:08", 1L, 1L });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "AddArticleToUserDate", "ArticleRefId", "UserRefId" },
                values: new object[] { 2L, "15.08.2019 20:50:08", 2L, 1L });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "AddArticleToUserDate", "ArticleRefId", "UserRefId" },
                values: new object[] { 3L, "15.08.2019 20:50:08", 1L, 2L });

            migrationBuilder.CreateIndex(
                name: "IX_UserArticles_ArticleRefId",
                table: "UserArticles",
                column: "ArticleRefId");

            migrationBuilder.CreateIndex(
                name: "IX_UserArticles_UserRefId",
                table: "UserArticles",
                column: "UserRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserArticles");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
