using Microsoft.EntityFrameworkCore;
using SpeechRecognitionThesis.Models.DatabaseModels;
using SpeechRecognitionThesis.Models.Scripts;
using System;

namespace SpeechRecognitionThesis.Models.Database
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions articleDbContextOptions)
            : base(articleDbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitArticlesCategory(modelBuilder);
            InitArticles(modelBuilder);
            InitUsers(modelBuilder);
            InitUserArticles(modelBuilder);
        }

        private void InitArticlesCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleCategory>().HasData(new ArticleCategory
            {
                Id = (long)CategoryId.SPORT_CATEGORY_ID,
                Name = "Sport"
            },
            new ArticleCategory
            {
                Id = (long)CategoryId.SCIENCE_CATEGORY_ID,
                Name = "Nauka"
            },
            new ArticleCategory
            {
                Id = (long)CategoryId.WORLD_CATEGORY_ID,
                Name = "Świat"
            },
            new ArticleCategory
            {
                Id = (long)CategoryId.COUNTRY_CATEGORY_ID,
                Name = "Kraj"
            },
            new ArticleCategory
            {
                Id = (long)CategoryId.POPULAR_SCIENCE_CATEGORY_ID,
                Name = "Popularnonaukowe"
            });
        }

        private void InitUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                NickName = UserTools.ANONYMOUS_USER_NICKNAME,
                Password = UserTools.ConvertInputTextToSha512( UserTools.ANONYMOUS_USER_NICKNAME ),
                Email = "guest@speechrecognition.com",
                CreateAccountDate = new DateTime( 2019, 5, 30 ).ToString(),
                LastUpdateAccountDate = new DateTime( 2019, 6, 20 ).ToString(),
                LastLoggedAccountDate = new DateTime( 2019, 8, 24 ).ToString(),
                AvatarId = 1
            },
           new User
           {
                Id = 2,
                NickName = "robert1234",
                Password = UserTools.ConvertInputTextToSha512("password123ba8"),
                Email = "robert@gmail.com",
                CreateAccountDate = new DateTime( 2018, 4, 22 ).ToString(),
                LastUpdateAccountDate = new DateTime( 2019, 2, 21 ).ToString(),
                LastLoggedAccountDate = new DateTime( 2019, 9, 12 ).ToString(),
                AvatarId = 2
           },
           new User
           {
                Id = 3,
                NickName = "michal854",
                Password = UserTools.ConvertInputTextToSha512("polska987wrm"),
                Email = "michal854@example.com",
                CreateAccountDate = new DateTime(2019, 1, 20).ToString(),
                LastUpdateAccountDate = new DateTime(2019, 6, 23).ToString(),
                LastLoggedAccountDate = new DateTime(2019, 6, 28).ToString(),
                AvatarId = 5
           },
           new User
           {
               Id = 4,
               NickName = "witek754",
               Password = UserTools.ConvertInputTextToSha512("witek123abc"),
               Email = "witek@gmail.com",
               CreateAccountDate = new DateTime( 2019, 4, 30 ).ToString(),
               LastUpdateAccountDate = new DateTime( 2019, 12, 21 ).ToString(),
               LastLoggedAccountDate = new DateTime( 2019, 12, 28 ).ToString(),
               AvatarId = 7
           },
           new User
           {
               Id = 5,
               NickName = "adam5",
               Password = UserTools.ConvertInputTextToSha512("adamP12z"),
               Email = "adam243567@gmail.com",
               CreateAccountDate = new DateTime( 2018, 3, 29 ).ToString(),
               LastUpdateAccountDate = new DateTime( 2019, 10, 04 ).ToString(),
               LastLoggedAccountDate = new DateTime( 2019, 12, 10 ).ToString(),
               AvatarId = 3
           });
        }

        private void InitArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().HasData(new Article
            {
                Id = 1,
                ArticleCategoryRefId = (long) CategoryId.SCIENCE_CATEGORY_ID,
                Subject = "Ssaki",
                Content = "zwierzęta należące do kręgowców, " +
                "charakteryzujące się głównie występowaniem gruczołów mlekowych u samic, zazwyczaj obecnością owłosienia " +
                "oraz stałocieplnością. Około 60% ssaków utrzymuje temperaturę w granicach 34 a 39 stopni Celcjusza. " +
                "Stałocieplność umożliwia aktywny tryb życia w różnych środowiskach. Od mroźnych obszarów podbiegunowych " +
                "do gorących tropików. Futro i tłuszcz pomagają uchronić się przed zimnem, a wydzielanie potu i szybki oddech " +
                "pomagają pozbyć się nadmiernego ciepła.",
                ArticleModificationDate = new DateTime(2019, 6, 28).ToString(),
                NumberOfViews = 12,
            },
            new Article
            {
                Id = 2,
                ArticleCategoryRefId = (long) CategoryId.SPORT_CATEGORY_ID,
                Subject = "Dawid Kubacki zajął 3 miejsce w konkursie!",
                Content = "Polak na podium. Po dwóch kapitalnych skokach polskiego skoczka, zajął 3 miejsce w turnieju czterech skoczni." +
                "Zawody wygrał Japończyk Ryoyu Kobayashi. Możemy być dumni z naszego rodaka.",
                ArticleModificationDate = new DateTime(2019, 9, 15).ToString(),
                NumberOfViews = 41,
            },
            new Article
            {
                Id = 3,
                ArticleCategoryRefId = (long) CategoryId.COUNTRY_CATEGORY_ID,
                Subject = "Nobel dla Polki",
                Content = "Olga Tokarczuk została tegoroczną laureatką Literackiej Nagrody Nobla za 2018 rok. W żadnej z innych kategorii " +
                "nie odnosimy tak spektakularnych sukcesów. W jaki sposób przekonała opinię publiczną do swojego sukcesu. Co spowodowało, " +
                "że Polska literatura zaczęła być uznawana na całym świecie? Czy rzeczywiście jesteśmy w ścisłej czołówce na świecie? " +
                "Ostatni raz nagroda Nobla została przyznana innej polskiej pisarce, Wisławie Szymborskiej w 1996 roku. To naprawdę wielki sukces " +
                "z którego powinniśmy być dumni",
                ArticleModificationDate = new DateTime(2019, 10, 30).ToString(),
                NumberOfViews = 108,
            },
            new Article
            {
                Id = 4,
                ArticleCategoryRefId = (long) CategoryId.POPULAR_SCIENCE_CATEGORY_ID,
                Subject = "Czarne dziury istnieją naprawdę!",
                Content = "Co sądzą ludzie o czarnych dziurach? Czy istnieją naprawdę? Czy powinniśmy się ich obawiać? To pytanie nurtuje nas " +
                "od wielu lat. Czarne dziury kojarzą się głównie ze śmiercią gwiazd. Jest to bardzo oczywiste stwierdzenie. Powstają w sposób " +
                "naturalny, będąc końcowym efektem ewolucji największych gwiazd. Posiadają masę co najmniej kilku naszych Słońc. Gdy gwiazda " +
                "zaczyna umierać, procesy termojądrowe z jego wnętrza przestają równoważyć nacisk z zewnątrz. Grawitacja zwycięża. Wkrótce więcej " +
                "informacji.",
                ArticleModificationDate = new DateTime(2019, 5, 15).ToString(),
                NumberOfViews = 101,
            },
            new Article
            {
                Id = 5,
                ArticleCategoryRefId = (long) CategoryId.WORLD_CATEGORY_ID,
                Subject = "Norwegia wydobywa coraz wiecęj ropy.",
                Content = "Całkowita sprzedaż norweskiego gazu wyniosła ok 11 mld euro. Czarne złoto stanowi większość przychodów państwa " +
                "położonego na Półwyspie Skandynawskim. Są też pomówienia, że tej ropy może kiedyś zabraknąć nowerskiemu przemysłowi.",
                ArticleModificationDate = new DateTime(2019, 2, 28).ToString(),
                NumberOfViews = 38,
            },
            new Article
            {
                Id = 6,
                ArticleCategoryRefId = (long) CategoryId.SCIENCE_CATEGORY_ID,
                Subject = "Mózg człowieka",
                Content = "Mózg jest uznawany za najważniejszy z ludzkich organów. Pełni wiele skomplikowanych funkcji. " +
                "Wyróżnia się trzy części mózgowia. Jest to mózg właściwy, międzymózgowie oraz pień mózgu. Każda z tych części odróżnia " +
                "się budową i pełnioną funkcją.",
                ArticleModificationDate = new DateTime(2019, 12, 28).ToString(),
                NumberOfViews = 94,
            },
            new Article
            {
                Id = 7,
                ArticleCategoryRefId = (long) CategoryId.SPORT_CATEGORY_ID,
                Subject = "Robert Lewandowski królem strzelców Bundesligi!",
                Content = "Robert Lewandowski po raz czwarty w karierze został królem strzelców Bundesligi! Więcej razy po tą nagrodę " +
                "sięgał tylko Gerd Muller. To bardzo niezwykłe. Wcześniej tego nie dokonał żaden z polskich napastników.",
                ArticleModificationDate = DateTime.Now.ToString(),
                NumberOfViews = 41,
            },
            new Article
            {
                Id = 8,
                ArticleCategoryRefId = (long) CategoryId.COUNTRY_CATEGORY_ID,
                Subject = "W Kołobrzegu piękna pogoda od soboty",
                Content = "Od kilku tygodni możemy cieszyć się pięknym latem w Kołobrzegu. Nie może dziwić fakt, że wiele osób wybrało w tym " +
                "roku Bałtyk i piękny krajobraz miasta Kołobrzeg. Panują temperatury od 28 stopni Celsjusza. Taka temperatura powinna się utrzymać " +
                "przez długie tygodnie tego pięknego lata.",
                ArticleModificationDate = DateTime.Now.ToString(),
                NumberOfViews = 76,
            },
            new Article
            {
                Id = 9,
                ArticleCategoryRefId = (long) CategoryId.POPULAR_SCIENCE_CATEGORY_ID,
                Subject = "Kto wynalazł elektryczność?",
                Content = "Dzisiaj ludzie nie mogą sobie wyobrazić życia bez elektryczności. Wszystko co nas otacza, komputery, radio, " +
                "sprzęty domowego użytku. To wszystko opiera się o energię pochodzącą z elektryczności. W 1600 roku naukowiec William Gilbert " +
                "jako pierwszy wytworzył iskrę elektryczną poprzez pocieranie bursztynu.",
                ArticleModificationDate = new DateTime( 2019, 12, 10 ).ToString(),
                NumberOfViews = 34,
            },
            new Article
            {
                Id = 10,
                ArticleCategoryRefId = (long) CategoryId.WORLD_CATEGORY_ID,
                Subject = "Chińczycy budują nowy lotniskowiec",
                Content = "Chińczycy zbudowali w rekordowym tempie kolejny lotniskowiec. Zajęło im to dosłownie dwa miesiące, podczas gdy " +
                "amerykanie budują go w przeciągu dwóch lat. Taka sytuacja powoduje, że USA nie jest już największą potęgą militarną na świecie.",
                ArticleModificationDate = new DateTime(2019, 11, 10).ToString(),
                NumberOfViews = 85,
            });
        }

        private void InitUserArticles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserArticles>().HasData(new UserArticles
            {
                Id = 1,
                UserRefId = 4,
                ArticleRefId = 1,
            },
            new UserArticles
            {
                Id = 2,
                UserRefId = 3,
                ArticleRefId = 2,
            },
            new UserArticles
            {
                Id = 3,
                UserRefId = 2,
                ArticleRefId = 3,
            },
            new UserArticles
            {
                Id = 4,
                UserRefId = 1,
                ArticleRefId = 4,
            },
            new UserArticles
            {
                Id = 5,
                UserRefId = 2,
                ArticleRefId = 5,
            },
            new UserArticles
            {
                Id = 6,
                UserRefId = 3,
                ArticleRefId = 6,
            },
            new UserArticles
            {
                Id = 7,
                UserRefId = 4,
                ArticleRefId = 7,
            },
            new UserArticles
            {
                Id = 8,
                UserRefId = 4,
                ArticleRefId = 8,
            },
            new UserArticles
            {
                Id = 9,
                UserRefId = 2,
                ArticleRefId = 9,
            },
            new UserArticles
            {
                Id = 10,
                UserRefId = 3,
                ArticleRefId = 10,
            });
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserArticles> UserArticles { get; set; }
    }
}