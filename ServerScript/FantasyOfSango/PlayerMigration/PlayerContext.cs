using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerData;
using Microsoft.EntityFrameworkCore.Storage;

//Developer : SangonomiyaSakunovi
//Discription : The CoreScript of EntityFramework, all DbSet and Relations should define here

namespace PlayerMigration
{
    public class PlayerContext : DbContext
    {
        //SQL configure
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectStr = "Data Source = SANGONOMIYAPC; Initial Catalog = FantasyOfSango_Player;" +
                "User ID = ROOT; Password = root;";
            optionsBuilder.UseSqlServer(connectionString: ConnectStr);
        }
        //The RefenrenceKey structure should define here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
                .HasOne(x => x.Player)
                .WithOne(y => y.UserInfo)
                .HasForeignKey<UserInfo>(z => z.PlayerId);
            modelBuilder.Entity<WeaponInfo>()
                .HasOne(x => x.Player)
                .WithOne(y => y.WeaponInfo)
                .HasForeignKey<WeaponInfo>(z => z.PlayerId);
            modelBuilder.Entity<EnhanceStoneInfo>()
                .HasOne(x => x.Player)
                .WithOne(y=>y.EnhanceStoneInfo)
                .HasForeignKey<EnhanceStoneInfo>(z => z.PlayerId);
        }
        //The DbSet should define here
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Player> Players { get;set; }
        public DbSet<WeaponInfo> WeaponInfos { get; set; }
        public DbSet<EnhanceStoneInfo> EnhanceStoneInfos { get; set; }
    }
}
