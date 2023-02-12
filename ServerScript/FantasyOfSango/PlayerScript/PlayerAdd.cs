using Common.ComBatCode;
using Common.ElementCode;
using Common.EnhanceStoneCode;
using Common.StateCode;
using Common.WeaponCode;
using PlayerData;
using PlayerMigration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerScript
{
    public class PlayerAdd
    {
        public static int AddUserInfo(string playerShowName, string username, string password)
        {
            using (PlayerContext context = new PlayerContext())
            {
                int changeLineNum = 0;
                bool isHasUserInfo = false;
                //Exam if there already has this username
                UserInfo storedUserInfo = context.UserInfos.SingleOrDefault(x => x.Username == username);
                if (storedUserInfo != null)
                {
                    isHasUserInfo = true;
                }
                if (isHasUserInfo)
                {
                    return changeLineNum;
                }
                else
                {
                    //Add UserInfo into DbSet                
                    var userInfoNew = new UserInfo
                    {
                        Username = username,
                        Password = password
                    };
                    var weaponInfoNew = WeaponAdd.PackWeaponInfo(WeaponIndexCode.Default, WeaponNameCode.Default, WeaponTypeCode.Default,
                        RarityCode.Default, SkillCode.Default, 0, 0, ElementTypeCode.Default, 0, 0, 0, 0,
                        PhysicEnhanceStoneCode.Default, ElementEnhanceStoneCode.Default, 0, 0, 0);
                    var enhanceStoneInfoNew = WeaponAdd.PackEnhanceStoneInfo(PhysicEnhanceStoneCode.Default, ElementEnhanceStoneCode.Default,
                        RarityCode.Default, RarityCode.Default, 0, 0, ElementTypeCode.Default, 0, 0);
                    var PlayerNew = new Player
                    {
                        PlayerShowName = playerShowName,
                        UserInfo = userInfoNew,
                        WeaponInfo = weaponInfoNew,
                        EnhanceStoneInfo = enhanceStoneInfoNew
                    };
                    context.Players.Add(PlayerNew);
                    return changeLineNum = context.SaveChanges();
                }
            }
        }        
    }
}
