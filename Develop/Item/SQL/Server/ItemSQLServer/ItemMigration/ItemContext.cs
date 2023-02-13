using ItemData.Player;
using ItemData.Weapon;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemMigration
{
    public class ItemContext : DbContext
    {
        //SQL configure
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string ConnectStr = "Data Source = SANGONOMIYAPC; Initial Catalog = FantasyOfSango_Player;" +
                "User ID = ITEMDEVELOPER; Password = itemdeveloper";
            optionsBuilder.UseSqlServer(connectionString: ConnectStr);
        }
        
        //The DbSet should define here
        public DbSet<WeaponDetail> WeaponDetails { get; set; }
        public DbSet<WeaponUpdateDetail> WeaponUpdateDetails { get; set; }
        public DbSet<PhysicEnhanceStoneDetail> PhysicEnhanceStoneInfoDetails { get; set; }
        public DbSet<ElementEnhanceStoneDetail> ElementEnhanceStoneDetails { get; set; }
    }
}
