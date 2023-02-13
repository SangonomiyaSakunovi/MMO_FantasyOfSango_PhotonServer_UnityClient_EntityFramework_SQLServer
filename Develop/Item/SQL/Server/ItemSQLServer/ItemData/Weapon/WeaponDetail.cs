using Common.ComBatCode;
using Common.ElementCode;
using Common.EnhanceStoneCode;
using Common.StateCode;
using Common.WeaponCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemData.Weapon
{
    public class WeaponDetail
    {        
        //Value
        public int Id { get; set; }
        public WeaponNameCode WeaponName { get; set; }
        public WeaponTypeCode WeaponType { get; set; }
        public RarityCode RarityDefault { get; set; }
        public SkillCode SkillDefault { get; set; }
        public int PhysicAttackDefault { get; set; }
        public int PhysicDefenceDefault { get; set; }
        public ElementTypeCode ElementTypeDefault { get; set; }
        public int ElementAttackDefault { get; set; }
        public int ElementDefenceDefault { get; set; }
        public float UnbreakValueDefault { get; set; }
        public int UnbreakRecoverTimesDefault { get; set; }
        public int LevelDefault { get; set; }
        public int ExperienceDefault { get; set; }
        public int WeaponValueDefault { get; set; }
        public string WeaponIntroduce { get; set; }
    }
}
