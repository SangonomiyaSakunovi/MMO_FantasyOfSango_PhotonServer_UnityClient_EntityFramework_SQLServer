using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayerMigration;
using Common.EnhanceStoneCode;
using Common.ElementCode;
using Common.StateCode;
using Common.ComBatCode;
using Common.WeaponCode;

namespace PlayerMigration.Migrations
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20230210152951_20230210")]
    partial class _20230210
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

                    b.Property<int>("ElementAttack");

                    b.Property<int>("ElementDefence");

                    b.Property<int>("ElementEnhanceStone");

                    b.Property<int>("ElementType");

                    b.Property<int>("Experience");

                    b.Property<string>("Introduce");

                    b.Property<int>("Level");

                    b.Property<int>("PhysicAttack");

                    b.Property<int>("PhysicDefence");

                    b.Property<int>("PhysicEnhanceStone");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Rarity");

                    b.Property<int>("Skill");

                    b.Property<int>("Type");

                    b.Property<float>("Unbreak");

                    b.Property<int>("WeaponIndex");

                    b.Property<int>("WeaponName");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("WeaponInfos");
                });

            modelBuilder.Entity("PlayerData.UserInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithOne("UserInfo")
                        .HasForeignKey("PlayerData.UserInfo", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PlayerData.WeaponInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithOne("WeaponInfo")
                        .HasForeignKey("PlayerData.WeaponInfo", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
