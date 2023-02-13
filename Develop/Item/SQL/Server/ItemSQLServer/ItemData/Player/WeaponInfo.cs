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

namespace ItemData.Player
{
    public class WeaponInfo
    {
        //Value
        public int Id { get; set; }
        public WeaponIndexCode WeaponIndex { get; set; }
        public WeaponNameCode WeaponName { get; set; }
        public WeaponTypeCode WeaponType { get; set; }
        public RarityCode Rarity { get; set; }
        public SkillCode Skill { get; set; }
        public int PhysicAttack { get; set; }
        public int PhysicDefence { get; set; }
        public ElementTypeCode ElementType { get; set; }
        public int ElementAttack { get; set; }
        public int ElementDefence { get; set; }
        public float UnbreakValue { get; set; }
        public int UnbreakRecoverTimes { get; set; }
        public PhysicEnhanceStoneCode PhysicEnhanceStone { get; set; }
        public ElementEnhanceStoneCode ElementEnhanceStone { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int WeaponValue { get; set; }

        
    }
}
