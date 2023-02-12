using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PlayerMigration;
using Common.EnhanceStoneCode;
using Common.StateCode;
using Common.ElementCode;
using Common.ComBatCode;
using Common.WeaponCode;

namespace PlayerMigration.Migrations
{
    [DbContext(typeof(PlayerContext))]
    partial class PlayerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlayerData.EnhanceStoneInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ElementAttackEnhance");

                    b.Property<int>("ElementDefenceEnhance");

                    b.Property<int>("ElementEnhanceStoneName");

                    b.Property<int>("ElementStoneRarity");

                    b.Property<int>("ElementType");

                    b.Property<int>("PhysicAttackEnhance");

                    b.Property<int>("PhysicDefenceEnhance");

                    b.Property<int>("PhysicEnhanceStoneName");

                    b.Property<int>("PhysicStoneRarity");

                    b.Property<int>("PlayerId");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("EnhanceStoneInfos");
                });

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

                    b.Property<int>("Level");

                    b.Property<int>("PhysicAttack");

                    b.Property<int>("PhysicDefence");

                    b.Property<int>("PhysicEnhanceStone");

                    b.Property<int>("PlayerId");

                    b.Property<int>("Rarity");

                    b.Property<int>("Skill");

                    b.Property<int>("UnbreakRecoverTimes");

                    b.Property<float>("UnbreakValue");

                    b.Property<int>("WeaponIndex");

                    b.Property<int>("WeaponName");

                    b.Property<int>("WeaponType");

                    b.Property<int>("WeaponValue");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("WeaponInfos");
                });

            modelBuilder.Entity("PlayerData.EnhanceStoneInfo", b =>
                {
                    b.HasOne("PlayerData.Player", "Player")
                        .WithOne("EnhanceStoneInfo")
                        .HasForeignKey("PlayerData.EnhanceStoneInfo", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
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
