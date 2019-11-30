﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeechRecognitionThesis.Models.Database;

namespace SpeechRecognitionThesis.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SpeechRecognitionThesis.Models.Article", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ArticleCategoryRefId");

                    b.Property<string>("ArticleModificationDate")
                        .HasMaxLength(50);

                    b.Property<string>("Content")
                        .HasMaxLength(4000);

                    b.Property<long>("NumberOfViews");

                    b.Property<string>("Subject")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("ArticleCategoryRefId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ArticleCategoryRefId = 1L,
                            ArticleModificationDate = "30.11.2019 22:50:35",
                            Content = "To jest treść artykułu 1",
                            NumberOfViews = 10L,
                            Subject = "Artykuł 1"
                        },
                        new
                        {
                            Id = 2L,
                            ArticleCategoryRefId = 4L,
                            ArticleModificationDate = "30.11.2019 22:50:35",
                            Content = "To jest artykuł 2",
                            NumberOfViews = 20L,
                            Subject = "Artykuł 2"
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.DatabaseModels.ArticleCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ArticleCategory");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Sport"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Nauka"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Świat"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Kraj"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Popularnonaukowe"
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.DatabaseModels.UserArticles", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ArticleRefId");

                    b.Property<long>("UserRefId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleRefId");

                    b.HasIndex("UserRefId");

                    b.ToTable("UserArticles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ArticleRefId = 1L,
                            UserRefId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ArticleRefId = 2L,
                            UserRefId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            ArticleRefId = 1L,
                            UserRefId = 2L
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvatarId");

                    b.Property<string>("CreateAccountDate")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("LastLoggedAccountDate")
                        .HasMaxLength(50);

                    b.Property<string>("LastUpdateAccountDate")
                        .HasMaxLength(50);

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AvatarId = 1,
                            CreateAccountDate = "30.05.2019 00:00:00",
                            Email = "guest@speechrecognition.com",
                            LastLoggedAccountDate = "24.08.2019 00:00:00",
                            LastUpdateAccountDate = "20.06.2019 00:00:00",
                            NickName = "Guest",
                            Password = "cc5ec2b61fbbdd18d85dd14ab60db397b21b5548999a6afd3ce9557b19c300494a5fd29987e03a6f06677c209b88de47684388de8250671cdd778799eecd018a"
                        },
                        new
                        {
                            Id = 2L,
                            AvatarId = 2,
                            CreateAccountDate = "21.05.2019 00:00:00",
                            Email = "robert@mail.com",
                            LastLoggedAccountDate = "23.08.2019 00:00:00",
                            LastUpdateAccountDate = "23.06.2019 00:00:00",
                            NickName = "RobertSon",
                            Password = "5e50a8d4e3897e2da8f3ddef3f6d75d1c327724acf408be827e6b2115d1d0d85e9f9dbadc14387b5622405d81763029cf610422bbe4e343bb9414bba4aa38828"
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.Article", b =>
                {
                    b.HasOne("SpeechRecognitionThesis.Models.DatabaseModels.ArticleCategory", "ArticleCategory")
                        .WithMany()
                        .HasForeignKey("ArticleCategoryRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.DatabaseModels.UserArticles", b =>
                {
                    b.HasOne("SpeechRecognitionThesis.Models.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleRefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SpeechRecognitionThesis.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
