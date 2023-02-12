using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayerMigration;
using Common.StateCode;

namespace PlayerMigration.Migrations
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20230210123040_testEmunCanWork")]
    partial class testEmunCanWork
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlayerData.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PlayerShowName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PlayerData.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<int>("PlayerId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("PlayerData.WeaponInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Type");

                    b.Property<string>("WeaponName")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("WeaponInfos");
                });

            modelBuilder.Entity("PlayerData.UserInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithOne("UserInfo")
                        .HasForeignKey("PlayerData.UserInfo", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
