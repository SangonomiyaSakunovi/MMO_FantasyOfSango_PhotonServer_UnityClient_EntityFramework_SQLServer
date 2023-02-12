using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerData
{
    public class Player
    {
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string PlayerShowName { get; set; }
        public UserInfo UserInfo { get; set; }
        public WeaponInfo WeaponInfo { get; set; }
        public EnhanceStoneInfo EnhanceStoneInfo { get; set; }
    }
}
