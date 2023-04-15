﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProCardsNew.Infrastructure.Persistence;

#nullable disable

namespace ProCardsNew.Infrastructure.Migrations
{
    [DbContext(typeof(ProCardsDbContext))]
    partial class ProCardsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("BackSide")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime>("CreatedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FrontSide")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Cards", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Entities.Grade", b =>
                {
                    b.Property<Guid>("CardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeckId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("GradeValue")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GradedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CardId", "DeckId", "UserId");

                    b.HasIndex("DeckId");

                    b.HasIndex("UserId");

                    b.ToTable("Grades", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Entities.Image", b =>
                {
                    b.Property<Guid>("CardId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SideId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Data")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CardId", "SideId");

                    b.HasIndex("SideId");

                    b.ToTable("Images", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Entities.Side", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("SideName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("Sides", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Deck", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("CardsCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<DateTime>("UpdatedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Decks", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Entities.DeckCard", b =>
                {
                    b.Property<Guid>("DeckId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("DeckId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("DeckCard", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Entities.DeckStatistic", b =>
                {
                    b.Property<Guid>("DeckId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("CardsViewed")
                        .HasColumnType("integer");

                    b.Property<float>("Hours")
                        .HasColumnType("real");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("DeckId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("DeckStatistic", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.UserAggregate.Entities.Statistic", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("CardsCreated")
                        .HasColumnType("integer");

                    b.Property<int>("CardsViewed")
                        .HasColumnType("integer");

                    b.Property<float>("Hours")
                        .HasColumnType("real");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Statistic", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.UserAggregate.Entities.UserDeck", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeckId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OpenedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.HasKey("UserId", "DeckId", "OpenedAtDateTime");

                    b.HasIndex("DeckId");

                    b.ToTable("UserDeck", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("LockoutEndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("PasswordRecoveryCode")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("PasswordRecoveryEndDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PasswordRecoveryFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("UpdatedAtDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Card", b =>
                {
                    b.HasOne("ProCardsNew.Domain.UserAggregate.User", "Owner")
                        .WithMany("OwnedCards")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Entities.Grade", b =>
                {
                    b.HasOne("ProCardsNew.Domain.CardAggregate.Card", "Card")
                        .WithMany("Grades")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProCardsNew.Domain.DeckAggregate.Deck", "Deck")
                        .WithMany()
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProCardsNew.Domain.UserAggregate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Deck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Entities.Image", b =>
                {
                    b.HasOne("ProCardsNew.Domain.CardAggregate.Card", "Card")
                        .WithMany("Images")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProCardsNew.Domain.CardAggregate.Entities.Side", "Side")
                        .WithMany()
                        .HasForeignKey("SideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Side");
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Deck", b =>
                {
                    b.HasOne("ProCardsNew.Domain.UserAggregate.User", "Owner")
                        .WithMany("OwnedDecks")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Entities.DeckCard", b =>
                {
                    b.HasOne("ProCardsNew.Domain.CardAggregate.Card", "Card")
                        .WithMany("DeckCards")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProCardsNew.Domain.DeckAggregate.Deck", "Deck")
                        .WithMany("DeckCards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Entities.DeckStatistic", b =>
                {
                    b.HasOne("ProCardsNew.Domain.DeckAggregate.Deck", "Deck")
                        .WithMany("DeckStatistics")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProCardsNew.Domain.UserAggregate.User", "User")
                        .WithMany("DeckStatistics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProCardsNew.Domain.UserAggregate.Entities.Statistic", b =>
                {
                    b.HasOne("ProCardsNew.Domain.UserAggregate.User", "User")
                        .WithOne("Statistic")
                        .HasForeignKey("ProCardsNew.Domain.UserAggregate.Entities.Statistic", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProCardsNew.Domain.UserAggregate.Entities.UserDeck", b =>
                {
                    b.HasOne("ProCardsNew.Domain.DeckAggregate.Deck", "Deck")
                        .WithMany("UserDecks")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProCardsNew.Domain.UserAggregate.User", "User")
                        .WithMany("UserDecks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProCardsNew.Domain.CardAggregate.Card", b =>
                {
                    b.Navigation("DeckCards");

                    b.Navigation("Grades");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("ProCardsNew.Domain.DeckAggregate.Deck", b =>
                {
                    b.Navigation("DeckCards");

                    b.Navigation("DeckStatistics");

                    b.Navigation("UserDecks");
                });

            modelBuilder.Entity("ProCardsNew.Domain.UserAggregate.User", b =>
                {
                    b.Navigation("DeckStatistics");

                    b.Navigation("OwnedCards");

                    b.Navigation("OwnedDecks");

                    b.Navigation("Statistic");

                    b.Navigation("UserDecks");
                });
#pragma warning restore 612, 618
        }
    }
}
