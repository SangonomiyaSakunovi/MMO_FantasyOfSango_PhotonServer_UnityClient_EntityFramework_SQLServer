using Common.WeaponCode;
using PlayerData;
using PlayerMigration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerScript
{
    public class WeaponLookUp
    {
        //Read the WeaponInfo
        public static WeaponInfo ReadWeaponInfo(WeaponNameCode weaponName)
        {
            using (PlayerContext context = new PlayerContext())
            {
                WeaponInfo storedWeaponInfo = context.WeaponInfos.SingleOrDefault(x => x.WeaponName == weaponName);
                if (storedWeaponInfo != null)
                {
                    return storedWeaponInfo;
                }
                else
                {
                    return context.WeaponInfos.SingleOrDefault(x => x.WeaponName == WeaponNameCode.Default);
                }
            }                     
        }
    }
}
