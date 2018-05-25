﻿// <auto-generated />
using GameStatsServer.DataProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GameStatsServer.Migrations
{
    [DbContext(typeof(EFDbContext))]
    partial class EFDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameStatsServer.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FragLimit");

                    b.Property<string>("GameMode")
                        .IsRequired();

                    b.Property<string>("Map")
                        .IsRequired();

                    b.Property<string>("ServerEndpoint")
                        .IsRequired();

                    b.Property<double>("TimeElapsed");

                    b.Property<int>("TimeLimit");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("ServerEndpoint");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("GameStatsServer.Entities.Score", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Deaths");

                    b.Property<int>("Frags");

                    b.Property<int>("Kills");

                    b.Property<int>("LeaderboardPlace");

                    b.Property<int>("MatchId");

                    b.Property<string>("PlayerName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("MatchId");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("GameStatsServer.Entities.Server", b =>
                {
                    b.Property<string>("Endpoint")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GameModes")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Endpoint");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("GameStatsServer.Entities.Match", b =>
                {
                    b.HasOne("GameStatsServer.Entities.Server", "Server")
                        .WithMany("Matches")
                        .HasForeignKey("ServerEndpoint")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GameStatsServer.Entities.Score", b =>
                {
                    b.HasOne("GameStatsServer.Entities.Match", "Match")
                        .WithMany("Scores")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
