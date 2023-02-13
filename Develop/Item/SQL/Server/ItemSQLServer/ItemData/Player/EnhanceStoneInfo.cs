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

namespace ItemData.Player
{
    public class EnhanceStoneInfo
    {
        //Value
        public int Id { get; set; }
        public PhysicEnhanceStoneCode PhysicEnhanceStoneName { get; set; }
        public ElementEnhanceStoneCode ElementEnhanceStoneName { get; set; }
        public RarityCode PhysicStoneRarity { get; set; }
        public RarityCode ElementStoneRarity { get; set; }
        public int PhysicAttackEnhance { get; set; }
        public int PhysicDefenceEnhance { get; set; }
        public ElementTypeCode ElementType { get; set; }
        public int ElementAttackEnhance { get; set; }
        public int ElementDefenceEnhance { get; set; }

        //ReferenceKey
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
