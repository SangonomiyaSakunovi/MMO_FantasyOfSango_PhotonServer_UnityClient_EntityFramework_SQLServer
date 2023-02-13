using ItemData.Player;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemLoad
{
    public class PlayerContext : DbContext
    {
        //SQL configure
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectStr = "Data Source = SANGONOMIYAPC; Initial Catalog = FantasyOfSango_Player;" +
                "User ID = ITEMDEVELOPER; Password = itemdeveloper";
            optionsBuilder.UseSqlServer(connectionString: ConnectStr);
        }        
        //The DbSet should define here
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<WeaponInfo> WeaponInfos { get; set; }
        public DbSet<EnhanceStoneInfo> EnhanceStoneInfos { get; set; }
    }
}
