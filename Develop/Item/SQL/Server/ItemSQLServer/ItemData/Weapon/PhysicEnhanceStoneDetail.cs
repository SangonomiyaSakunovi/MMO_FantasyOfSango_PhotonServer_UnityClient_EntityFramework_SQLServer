using Common.ElementCode;
using Common.EnhanceStoneCode;
using Common.StateCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemData.Weapon
{
    public class PhysicEnhanceStoneDetail
    {
        //Value
        public int Id { get; set; }
        public PhysicEnhanceStoneCode PhysicEnhanceStoneName { get; set; }
        public RarityCode PhysicStoneRarity { get; set; }
        public int PhysicAttackEnhance { get; set; }
        public int PhysicDefenceEnhance { get; set; }
        public int PhysicStonePlayerLevelNeed { get; set; }
        public int PhysicStoneValue { get; set; }
    }
}
