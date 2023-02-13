using Common.WeaponCode;
using ItemData.Player;
using ItemLoad;
using ItemMigration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemScript
{
    public class PlayerLookUp
    {
        public static WeaponInfo LookUpWeaponInfo(WeaponNameCode weaponName)
        {
            using (PlayerContext context = new PlayerContext())
            {
                WeaponInfo storedWeaponInfo = context.WeaponInfos.SingleOrDefault(x => x.WeaponName == weaponName);
                return storedWeaponInfo;
            }            
        }
    }
}
