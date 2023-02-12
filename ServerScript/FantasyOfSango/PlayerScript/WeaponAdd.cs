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
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace PlayerScript
{
    public class WeaponAdd
    {
        public static WeaponInfo PackWeaponInfo(WeaponIndexCode weaponIndex, WeaponNameCode weaponName,
            WeaponTypeCode weaponTypeCode, RarityCode rarity, SkillCode skill, int physicAttack, int physicDefence,
            ElementTypeCode elementType, int elementAttack, int elementDefence, float unbreakValue, int unbreakRecoverTimes,
            PhysicEnhanceStoneCode physicEnhanceStone, ElementEnhanceStoneCode elementEnhanceStone,
            int level, int experience, int weaponValue)
        {
            var weaponInfoNew = new WeaponInfo
            {
                WeaponIndex = weaponIndex,
                WeaponName = weaponName,
                WeaponType = weaponTypeCode,
                Rarity = rarity,
                Skill = skill,
                PhysicAttack = physicAttack,
                PhysicDefence = physicDefence,
                ElementType = elementType,
                ElementAttack = elementAttack,
                ElementDefence = elementDefence,
                UnbreakValue = unbreakValue,
                UnbreakRecoverTimes = unbreakRecoverTimes,
                PhysicEnhanceStone = physicEnhanceStone,
                ElementEnhanceStone = elementEnhanceStone,
                Level = level,
                Experience = experience,
                WeaponValue = weaponValue
            };
            return weaponInfoNew;
        }
        public static EnhanceStoneInfo PackEnhanceStoneInfo(PhysicEnhanceStoneCode physicEnhanceStoneName, ElementEnhanceStoneCode elementEnhanceStoneName,
            RarityCode physicStoneRarity, RarityCode elementStoneRarity, int physicAttackEnhance, int physicDefenceEnhance,
            ElementTypeCode elementType, int elementAttackEnhance, int elementDefenceEnhance)
        {
            var enhanceStoneInfoNew = new EnhanceStoneInfo
            {
                PhysicEnhanceStoneName = physicEnhanceStoneName,
                ElementEnhanceStoneName = elementEnhanceStoneName,
                PhysicStoneRarity = physicStoneRarity,
                ElementStoneRarity = elementStoneRarity,
                PhysicAttackEnhance = physicAttackEnhance,
                PhysicDefenceEnhance = physicDefenceEnhance,
                ElementType = elementType,
                ElementAttackEnhance = elementAttackEnhance,
                ElementDefenceEnhance = elementDefenceEnhance
            };
            return enhanceStoneInfoNew;
        }
    }
}
