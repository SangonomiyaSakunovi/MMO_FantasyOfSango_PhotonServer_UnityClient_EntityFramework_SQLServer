using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ItemMigration;
using Common.EnhanceStoneCode;
using Common.StateCode;
using Common.ElementCode;
using Common.ComBatCode;
using Common.WeaponCode;
using Common.MaterialCode;

namespace ItemMigration.Migrations
{
    [DbContext(typeof(ItemContext))]
    partial class ItemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ItemData.Weapon.ElementEnhanceStoneDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ElementAttackEnhance");

                    b.Property<int>("ElementDefenceEnhance");

                    b.Property<int>("ElementEnhanceStoneName");

                    b.Property<int>("ElementStonePlayerLevelNeed");

                    b.Property<int>("ElementStoneRarity");

                    b.Property<int>("ElementStoneValue");

                    b.Property<int>("ElementType");

                    b.HasKey("Id");

                    b.ToTable("ElementEnhanceStoneDetails");
                });

            modelBuilder.Entity("ItemData.Weapon.PhysicEnhanceStoneDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PhysicAttackEnhance");

                    b.Property<int>("PhysicDefenceEnhance");

                    b.Property<int>("PhysicEnhanceStoneName");

                    b.Property<int>("PhysicStonePlayerLevelNeed");

                    b.Property<int>("PhysicStoneRarity");

                    b.Property<int>("PhysicStoneValue");

                    b.HasKey("Id");

                    b.ToTable("PhysicEnhanceStoneInfoDetails");
                });

            modelBuilder.Entity("ItemData.Weapon.WeaponDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ElementAttackDefault");

                    b.Property<int>("ElementDefenceDefault");

                    b.Property<int>("ElementTypeDefault");

                    b.Property<int>("ExperienceDefault");

                    b.Property<int>("LevelDefault");

                    b.Property<int>("PhysicAttackDefault");

                    b.Property<int>("PhysicDefenceDefault");

                    b.Property<int>("RarityDefault");

                    b.Property<int>("SkillDefault");

                    b.Property<int>("UnbreakRecoverTimesDefault");

                    b.Property<float>("UnbreakValueDefault");

                    b.Property<string>("WeaponIntroduce");

                    b.Property<int>("WeaponName");

                    b.Property<int>("WeaponType");

                    b.Property<int>("WeaponValueDefault");

                    b.HasKey("Id");

                    b.ToTable("WeaponDetails");
                });

            modelBuilder.Entity("ItemData.Weapon.WeaponUpdateDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BreakCost");

                    b.Property<int>("BreakLevel");

                    b.Property<int>("BreakMaterial");

                    b.Property<int?>("WeaponDetailId");

                    b.HasKey("Id");

                    b.HasIndex("WeaponDetailId");

                    b.ToTable("WeaponUpdateDetails");
                });

            modelBuilder.Entity("ItemData.Weapon.WeaponUpdateDetail", b =>
                {
                    b.HasOne("ItemData.Weapon.WeaponDetail", "WeaponDetail")
                        .WithMany()
                        .HasForeignKey("WeaponDetailId");
                });
        }
    }
}
