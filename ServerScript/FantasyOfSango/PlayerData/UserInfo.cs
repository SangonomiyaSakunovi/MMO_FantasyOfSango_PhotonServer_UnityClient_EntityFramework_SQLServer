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
    public class UserInfo
    {
        //Value
        public int Id { get; set; }
        [Required, MaxLength(64)]
        public string Username { get; set; }
        [Required, MaxLength(64)]
        public string Password { get; set; }
        //ReferenceKey
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
