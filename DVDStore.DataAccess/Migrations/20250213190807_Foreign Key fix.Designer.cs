﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieStore.DataAccess.Data;

#nullable disable

namespace MovieStore.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250213190807_Foreign Key fix")]
    partial class ForeignKeyfix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("MovieStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Drama"
                        });
                });

            modelBuilder.Entity("MovieStore.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DirectorId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<double>("Price10")
                        .HasColumnType("REAL");

                    b.Property<double>("Price5")
                        .HasColumnType("REAL");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DirectorId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 4,
                            DirectorId = 2,
                            Price = 0.0,
                            Price10 = 0.0,
                            Price5 = 0.0,
                            ReleaseDate = new DateOnly(2020, 1, 27),
                            Summary = "The Father is a 2020 psychological drama film, directed by Florian Zeller in his directorial debut. The film stars Anthony Hopkins as an octogenarian Welsh man living with dementia. At the 93rd Academy Awards, The Father received six nominations, including Best Picture; Hopkins won Best Actor and Zeller and Hampton won Best Adapted Screenplay. Since then, it has been cited as one of the best films of the 2020s and the 21st century.",
                            Title = "The Father"
                        });
                });

            modelBuilder.Entity("MovieStore.Models.MoviesActors", b =>
                {
                    b.Property<int>("ActorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActorId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesActors");

                    b.HasData(
                        new
                        {
                            ActorId = 1,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MovieStore.Models.MoviesWriters", b =>
                {
                    b.Property<int>("WriterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.HasKey("WriterId", "MovieId");

                    b.HasIndex("MovieId");

                    b.ToTable("MoviesWriters");

                    b.HasData(
                        new
                        {
                            WriterId = 2,
                            MovieId = 1
                        });
                });

            modelBuilder.Entity("MovieStore.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Background = " One of Britain's most recognisable and prolific actors, he is known for his performances on the screen and stage. Hopkins has received numerous accolades, including two Academy Awards, four BAFTA Awards, two Primetime Emmy Awards, and a Laurence Olivier Award.",
                            Name = "Anthony Hopkins"
                        },
                        new
                        {
                            Id = 2,
                            Background = "Florian Zeller is a French novelist, playwright, theatre director, screenwriter, and film director. He has written over a dozen plays, that have been staged worldwide and have made him one of the most celebrated contemporary playwrights.",
                            Name = "Florian Zeller"
                        });
                });

            modelBuilder.Entity("MovieStore.Models.Movie", b =>
                {
                    b.HasOne("MovieStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieStore.Models.Person", "Director")
                        .WithMany("MoviesDirected")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Director");
                });

            modelBuilder.Entity("MovieStore.Models.MoviesActors", b =>
                {
                    b.HasOne("MovieStore.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieStore.Models.Movie", null)
                        .WithMany("MoviesActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieStore.Models.MoviesWriters", b =>
                {
                    b.HasOne("MovieStore.Models.Movie", null)
                        .WithMany("MoviesWriters")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MovieStore.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("WriterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieStore.Models.Movie", b =>
                {
                    b.Navigation("MoviesActors");

                    b.Navigation("MoviesWriters");
                });

            modelBuilder.Entity("MovieStore.Models.Person", b =>
                {
                    b.Navigation("MoviesDirected");
                });
#pragma warning restore 612, 618
        }
    }
}
