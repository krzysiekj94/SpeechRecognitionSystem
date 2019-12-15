using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpeechRecognitionThesis.Migrations
{
    public partial class SpeechRecognitionThesisModelsDatabaseRepositoryContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleCategory",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NickName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    CreateAccountDate = table.Column<string>(maxLength: 50, nullable: true),
                    LastUpdateAccountDate = table.Column<string>(maxLength: 50, nullable: true),
                    LastLoggedAccountDate = table.Column<string>(maxLength: 50, nullable: true),
                    AvatarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArticleCategoryRefId = table.Column<long>(nullable: false),
                    Subject = table.Column<string>(maxLength: 200, nullable: true),
                    Content = table.Column<string>(maxLength: 4000, nullable: true),
                    ArticleModificationDate = table.Column<string>(maxLength: 50, nullable: true),
                    NumberOfViews = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleCategory_ArticleCategoryRefId",
                        column: x => x.ArticleCategoryRefId,
                        principalTable: "ArticleCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserArticles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserRefId = table.Column<long>(nullable: false),
                    ArticleRefId = table.Column<long>(nullable: false)
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
                table: "ArticleCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Sport" },
                    { 2L, "Nauka" },
                    { 3L, "Świat" },
                    { 4L, "Kraj" },
                    { 5L, "Popularnonaukowe" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarId", "CreateAccountDate", "Email", "LastLoggedAccountDate", "LastUpdateAccountDate", "NickName", "Password" },
                values: new object[,]
                {
                    { 1L, 1, "30.05.2019 00:00:00", "guest@speechrecognition.com", "24.08.2019 00:00:00", "20.06.2019 00:00:00", "Guest", "cc5ec2b61fbbdd18d85dd14ab60db397b21b5548999a6afd3ce9557b19c300494a5fd29987e03a6f06677c209b88de47684388de8250671cdd778799eecd018a" },
                    { 2L, 2, "21.05.2019 00:00:00", "robert@mail.com", "23.08.2019 00:00:00", "23.06.2019 00:00:00", "RobertSon", "5e50a8d4e3897e2da8f3ddef3f6d75d1c327724acf408be827e6b2115d1d0d85e9f9dbadc14387b5622405d81763029cf610422bbe4e343bb9414bba4aa38828" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleCategoryRefId", "ArticleModificationDate", "Content", "NumberOfViews", "Subject" },
                values: new object[] { 1L, 1L, "15.12.2019 22:08:45", "To jest treść artykułu 1", 10L, "Artykuł 1" });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleCategoryRefId", "ArticleModificationDate", "Content", "NumberOfViews", "Subject" },
                values: new object[] { 2L, 4L, "15.12.2019 22:08:45", "To jest artykuł 2", 20L, "Artykuł 2" });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "ArticleRefId", "UserRefId" },
                values: new object[] { 1L, 1L, 1L });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "ArticleRefId", "UserRefId" },
                values: new object[] { 3L, 1L, 2L });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "ArticleRefId", "UserRefId" },
                values: new object[] { 2L, 2L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleCategoryRefId",
                table: "Articles",
                column: "ArticleCategoryRefId");

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

            migrationBuilder.DropTable(
                name: "ArticleCategory");
        }
    }
}
