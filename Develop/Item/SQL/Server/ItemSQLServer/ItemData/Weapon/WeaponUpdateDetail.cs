using Common.ComBatCode;
using Common.StateCode;
using Common.WeaponCode;
using Common.MaterialCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemData.Weapon
{
    public class WeaponUpdateDetail
    {
        //Value
        public int Id { get; set; }
        public int BreakLevel { get; set; }
        public int BreakCost { get; set; }
        public WeaponMaterialCode BreakMaterial { get; set; }

        //Reference
        public WeaponDetail WeaponDetail { get; set; }
    }
}
