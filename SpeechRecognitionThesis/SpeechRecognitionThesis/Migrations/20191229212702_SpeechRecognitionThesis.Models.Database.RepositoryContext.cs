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
                    { 2L, 2, "22.04.2018 00:00:00", "robert@gmail.com", "12.09.2019 00:00:00", "21.02.2019 00:00:00", "robert1234", "907be532d318507e73c44a7899eb7c81f97294ec5619c0e47d59f0d32f986ba87e42f51be6a461d42c5bfede284342afd735682b3b7926fd4c6ba69da9839a04" },
                    { 3L, 5, "20.01.2019 00:00:00", "michal854@example.com", "28.06.2019 00:00:00", "23.06.2019 00:00:00", "michal854", "339136aa80c4abdd548dc4b9e9092992b5c40d0b6374fce880a4ceb74db580c741ffe0f64ac9081d6790b1715f7977eaa47d77eefd99c277354dbacee498bf2b" },
                    { 4L, 7, "30.04.2019 00:00:00", "witek@gmail.com", "28.12.2019 00:00:00", "21.12.2019 00:00:00", "witek754", "120672f4755d88105e7653aa6e9d3792146921e387064e11761f283910ab6cfc25918d70d24df82f23b400ec1904f6d6f004132fb1cc49063f0b1acb72be2bdf" },
                    { 5L, 3, "29.03.2018 00:00:00", "adam243567@gmail.com", "10.12.2019 00:00:00", "04.10.2019 00:00:00", "adam5", "dc092fd8666fab2589db452d95294c67d7928e261a56f968d20f1ce2365c76c1590094e1287c921418f072c26cc6e6c392d8175b96d824de432874b9f4ea46a6" }
                });

            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleCategoryRefId", "ArticleModificationDate", "Content", "NumberOfViews", "Subject" },
                values: new object[,]
                {
                    { 2L, 1L, "15.09.2019 14:32:21", "Polak na podium. Po dwóch kapitalnych skokach polskiego skoczka, zajął 3 miejsce w turnieju czterech skoczni.Zawody wygrał Japończyk Ryoyu Kobayashi. Możemy być dumni z naszego rodaka.", 41L, "Dawid Kubacki zajął 3 miejsce w konkursie!" },
                    { 7L, 1L, "29.12.2019 22:27:01", "Robert Lewandowski po raz czwarty w karierze został królem strzelców Bundesligi! Więcej razy po tą nagrodę sięgał tylko Gerd Muller. To bardzo niezwykłe. Wcześniej tego nie dokonał żaden z polskich napastników.", 41L, "Robert Lewandowski królem strzelców Bundesligi!" },
                    { 1L, 2L, "28.06.2019 11:20:58", "zwierzęta należące do kręgowców, charakteryzujące się głównie występowaniem gruczołów mlekowych u samic, zazwyczaj obecnością owłosienia oraz stałocieplnością. Około 60% ssaków utrzymuje temperaturę w granicach 34 a 39 stopni Celcjusza. Stałocieplność umożliwia aktywny tryb życia w różnych środowiskach. Od mroźnych obszarów podbiegunowych do gorących tropików. Futro i tłuszcz pomagają uchronić się przed zimnem, a wydzielanie potu i szybki oddech pomagają pozbyć się nadmiernego ciepła.", 12L, "Ssaki" },
                    { 6L, 2L, "28.12.2019 09:55:27", "Mózg jest uznawany za najważniejszy z ludzkich organów. Pełni wiele skomplikowanych funkcji. Wyróżnia się trzy części mózgowia. Jest to mózg właściwy, międzymózgowie oraz pień mózgu. Każda z tych części odróżnia się budową i pełnioną funkcją.", 94L, "Mózg człowieka" },
                    { 5L, 3L, "28.02.2019 21:59:38", "Całkowita sprzedaż norweskiego gazu wyniosła ok 11 mld euro. Czarne złoto stanowi większość przychodów państwa położonego na Półwyspie Skandynawskim. Są też pomówienia, że tej ropy może kiedyś zabraknąć nowerskiemu przemysłowi.", 38L, "Norwegia wydobywa coraz wiecęj ropy." },
                    { 10L, 3L, "10.11.2019 15:22:26", "Chińczycy zbudowali w rekordowym tempie kolejny lotniskowiec. Zajęło im to dosłownie dwa miesiące, podczas gdy amerykanie budują go w przeciągu dwóch lat. Taka sytuacja powoduje, że USA nie jest już największą potęgą militarną na świecie.", 85L, "Chińczycy budują nowy lotniskowiec" },
                    { 3L, 4L, "30.10.2019 12:24:44", "Olga Tokarczuk została tegoroczną laureatką Literackiej Nagrody Nobla za 2018 rok. W żadnej z innych kategorii nie odnosimy tak spektakularnych sukcesów. W jaki sposób przekonała opinię publiczną do swojego sukcesu. Co spowodowało, że Polska literatura zaczęła być uznawana na całym świecie? Czy rzeczywiście jesteśmy w ścisłej czołówce na świecie? Ostatni raz nagroda Nobla została przyznana innej polskiej pisarce, Wisławie Szymborskiej w 1996 roku. To naprawdę wielki sukces z którego powinniśmy być dumni", 108L, "Nobel dla Polki" },
                    { 8L, 4L, "29.12.2019 22:27:01", "Od kilku tygodni możemy cieszyć się pięknym latem w Kołobrzegu. Nie może dziwić fakt, że wiele osób wybrało w tym roku Bałtyk i piękny krajobraz miasta Kołobrzeg. Panują temperatury od 28 stopni Celsjusza. Taka temperatura powinna się utrzymać przez długie tygodnie tego pięknego lata.", 76L, "W Kołobrzegu piękna pogoda od soboty" },
                    { 4L, 5L, "15.05.2019 10:21:09", "Co sądzą ludzie o czarnych dziurach? Czy istnieją naprawdę? Czy powinniśmy się ich obawiać? To pytanie nurtuje nas od wielu lat. Czarne dziury kojarzą się głównie ze śmiercią gwiazd. Jest to bardzo oczywiste stwierdzenie. Powstają w sposób naturalny, będąc końcowym efektem ewolucji największych gwiazd. Posiadają masę co najmniej kilku naszych Słońc. Gdy gwiazda zaczyna umierać, procesy termojądrowe z jego wnętrza przestają równoważyć nacisk z zewnątrz. Grawitacja zwycięża. Wkrótce więcej informacji.", 101L, "Czarne dziury istnieją naprawdę!" },
                    { 9L, 5L, "10.12.2019 07:43:22", "Dzisiaj ludzie nie mogą sobie wyobrazić życia bez elektryczności. Wszystko co nas otacza, komputery, radio, sprzęty domowego użytku. To wszystko opiera się o energię pochodzącą z elektryczności. W 1600 roku naukowiec William Gilbert jako pierwszy wytworzył iskrę elektryczną poprzez pocieranie bursztynu.", 34L, "Kto wynalazł elektryczność?" }
                });

            migrationBuilder.InsertData(
                table: "UserArticles",
                columns: new[] { "Id", "ArticleRefId", "UserRefId" },
                values: new object[,]
                {
                    { 2L, 2L, 3L },
                    { 7L, 7L, 4L },
                    { 1L, 1L, 4L },
                    { 6L, 6L, 3L },
                    { 5L, 5L, 2L },
                    { 10L, 10L, 3L },
                    { 3L, 3L, 2L },
                    { 8L, 8L, 4L },
                    { 4L, 4L, 1L },
                    { 9L, 9L, 2L }
                });

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
