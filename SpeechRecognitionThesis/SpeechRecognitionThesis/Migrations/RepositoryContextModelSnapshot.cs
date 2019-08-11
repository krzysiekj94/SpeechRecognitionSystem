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
                    b.Property<long>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId");

                    b.Property<string>("AuthorName");

                    b.Property<string>("Content");

                    b.Property<DateTime>("InsertionDate");

                    b.Property<DateTime>("LastUpdateDate");

                    b.HasKey("ArticleId");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            ArticleId = 1L,
                            AuthorId = 1L,
                            AuthorName = "Krystian B.",
                            Content = "To jest artykuł 1",
                            InsertionDate = new DateTime(2017, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdateDate = new DateTime(2018, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            ArticleId = 2L,
                            AuthorId = 1L,
                            AuthorName = "Roman Z.",
                            Content = "To jest artykuł 2",
                            InsertionDate = new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastUpdateDate = new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("SpeechRecognitionThesis.Models.User", b =>
                {
                    b.Property<long>("UserId")
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

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            ActiveAccountState = 1,
                            CreateAccountDate = "30.05.2019 00:00:00",
                            Email = "bas@gmail.com",
                            IsLogged = false,
                            LastUpdateAccountDate = "20.06.2019 00:00:00",
                            NickName = "SuperBass",
                            Password = "3c54ae8854fd40631cdaabba9b9df836bb5cace38cafcfad7e9a89477300a1cbf5fb7937ee188ace530d1a27aedd4e90e69e27c60d888e6136d326e24cff1699"
                        },
                        new
                        {
                            UserId = 2L,
                            ActiveAccountState = 1,
                            CreateAccountDate = "21.05.2019 00:00:00",
                            Email = "robert@mail.com",
                            IsLogged = false,
                            LastUpdateAccountDate = "23.06.2019 00:00:00",
                            NickName = " RobertSon",
                            Password = "5e50a8d4e3897e2da8f3ddef3f6d75d1c327724acf408be827e6b2115d1d0d85e9f9dbadc14387b5622405d81763029cf610422bbe4e343bb9414bba4aa38828"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
