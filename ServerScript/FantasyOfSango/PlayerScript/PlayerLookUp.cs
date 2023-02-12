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
    public class PlayerLookUp
    {
        //Exam if the UserInfo is Valid
        public static bool VerifyUserInfo(string username, string password)
        {
            using (PlayerContext context = new PlayerContext())
            {
                bool isValidUserInfo = false;
                UserInfo storedUserInfo = context.UserInfos.SingleOrDefault(x => x.Username == username);
                if (storedUserInfo != null)
                {
                    if (storedUserInfo.Username == username && storedUserInfo.Password == password)
                    {
                        isValidUserInfo = true;
                    }
                }
                return isValidUserInfo;
            }            
        }               
    }
}
