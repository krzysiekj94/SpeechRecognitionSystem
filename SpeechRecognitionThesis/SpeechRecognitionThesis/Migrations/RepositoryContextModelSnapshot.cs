﻿// <auto-generated />
using System;
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

                    b.Property<bool>("AvailabilityStatus");

                    b.Property<string>("Content");

                    b.Property<DateTime>("InsertionDate");

                    b.Property<DateTime>("LastUpdateDate");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AvailabilityStatus = false,
                            Content = "To jest artykuł 1",
                            InsertionDate = new DateTime(2017, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdateDate = new DateTime(2018, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            AvailabilityStatus = false,
                            Content = "To jest artykuł 2",
                            InsertionDate = new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdateDate = new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.DatabaseModels.UserArticles", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArticleModificationDate");

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
                            ArticleModificationDate = "17.08.2019 18:32:32",
                            ArticleRefId = 1L,
                            UserRefId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            ArticleModificationDate = "17.08.2019 18:32:32",
                            ArticleRefId = 2L,
                            UserRefId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            ArticleModificationDate = "17.08.2019 18:32:32",
                            ArticleRefId = 1L,
                            UserRefId = 2L
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActiveAccountState");

                    b.Property<string>("CreateAccountDate");

                    b.Property<string>("Email");

                    b.Property<bool>("IsLogged");

                    b.Property<string>("LastUpdateAccountDate");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            ActiveAccountState = 1,
                            CreateAccountDate = "30.05.2019 00:00:00",
                            Email = "guest@speechrecognition.com",
                            IsLogged = true,
                            LastUpdateAccountDate = "20.06.2019 00:00:00",
                            NickName = "Guest",
                            Password = "cc5ec2b61fbbdd18d85dd14ab60db397b21b5548999a6afd3ce9557b19c300494a5fd29987e03a6f06677c209b88de47684388de8250671cdd778799eecd018a"
                        },
                        new
                        {
                            Id = 2L,
                            ActiveAccountState = 1,
                            CreateAccountDate = "21.05.2019 00:00:00",
                            Email = "robert@mail.com",
                            IsLogged = false,
                            LastUpdateAccountDate = "23.06.2019 00:00:00",
                            NickName = " RobertSon",
                            Password = "5e50a8d4e3897e2da8f3ddef3f6d75d1c327724acf408be827e6b2115d1d0d85e9f9dbadc14387b5622405d81763029cf610422bbe4e343bb9414bba4aa38828"
                        });
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
