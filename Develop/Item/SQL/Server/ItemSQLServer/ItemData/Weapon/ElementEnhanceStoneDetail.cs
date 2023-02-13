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
    public class ElementEnhanceStoneDetail
    {
        //Value
        public int Id { get; set; }        
        public ElementEnhanceStoneCode ElementEnhanceStoneName { get; set; }
        public RarityCode ElementStoneRarity { get; set; }
        public ElementTypeCode ElementType { get; set; }
        public int ElementAttackEnhance { get; set; }
        public int ElementDefenceEnhance { get; set; }
        public int ElementStonePlayerLevelNeed { get; set; }
        public int ElementStoneValue { get; set; }
    }
}
