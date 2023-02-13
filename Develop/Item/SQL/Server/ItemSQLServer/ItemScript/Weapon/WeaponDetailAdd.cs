using Common.ComBatCode;
using Common.ElementCode;
using Common.EnhanceStoneCode;
using Common.MaterialCode;
using Common.StateCode;
using Common.Tools;
using Common.WeaponCode;
using ItemData.Player;
using ItemData.Weapon;
using ItemLoad;
using ItemMigration;
using ItemScript.ModifyState;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

//Developer : SangonomiyaSakunovi
//Discription:

namespace ItemScript.Weapon
{
    public class WeaponDetailAdd
    {
        public static WeaponDetail PackWeaponDetail(WeaponNameCode weaponName, WeaponTypeCode weaponType,
            RarityCode rarity, SkillCode skill, int physicAttack, int physicDefence, ElementTypeCode elementType,
            int elementAttack, int elementDefence, float unbreakValue, int unbreakRecover,
            int level, int experience, int weaponValue, string weaponIntroduce)
        {
            var weaponDetailNew = new WeaponDetail
            {
                WeaponName = weaponName,
                WeaponType = weaponType,
                RarityDefault = rarity,
                SkillDefault = skill,
                PhysicAttackDefault = physicAttack,
                PhysicDefenceDefault = physicDefence,
                ElementTypeDefault = elementType,
                ElementAttackDefault = elementAttack,
                ElementDefenceDefault = elementDefence,
                UnbreakValueDefault = unbreakValue,
                UnbreakRecoverTimesDefault = unbreakRecover,
                LevelDefault = level,
                ExperienceDefault = experience,
                WeaponValueDefault = weaponValue,
                WeaponIntroduce = weaponIntroduce
            };
            return weaponDetailNew;
        }

        public static WeaponUpdateDetail PackWeaponUpdateDetail(int breakLevel, int breakCost,
            WeaponMaterialCode breakMaterial)
        {
            var weaponUpdateNew = new WeaponUpdateDetail
            {
                BreakLevel = breakLevel,
                BreakCost = breakCost,
                BreakMaterial = breakMaterial
            };
            return weaponUpdateNew;
        }

        public static List<WeaponDetail> PackWeaponDetailFromExcel(string weaponDetailExcelLocation)
        {
            List<List<string>> weaponDetailList = ReadFile.ReadExcelFromSecondRow(weaponDetailExcelLocation);
            List<WeaponDetail> weaponDetailListNew = new List<WeaponDetail>();
            foreach (var weaponDetail in weaponDetailList)
            {
                var weaponDetailNew = new WeaponDetail
                {
                    WeaponName = (WeaponNameCode)GetEnum.GetEnumByString(typeof(WeaponNameCode), weaponDetail[0]),
                    WeaponType = (WeaponTypeCode)GetEnum.GetEnumByString(typeof(WeaponTypeCode), weaponDetail[1]),
                    RarityDefault = (RarityCode)GetEnum.GetEnumByString(typeof(RarityCode), weaponDetail[2]),
                    SkillDefault = (SkillCode)GetEnum.GetEnumByString(typeof(SkillCode), weaponDetail[3]),
                    PhysicAttackDefault = int.Parse(weaponDetail[4]),
                    PhysicDefenceDefault = int.Parse(weaponDetail[5]),
                    ElementTypeDefault = (ElementTypeCode)GetEnum.GetEnumByString(typeof(ElementTypeCode), weaponDetail[6]),
                    ElementAttackDefault = int.Parse(weaponDetail[7]),
                    ElementDefenceDefault = int.Parse(weaponDetail[8]),
                    UnbreakValueDefault = float.Parse(weaponDetail[9]),
                    UnbreakRecoverTimesDefault = int.Parse(weaponDetail[10]),
                    LevelDefault = int.Parse(weaponDetail[11]),
                    ExperienceDefault = int.Parse(weaponDetail[12]),
                    WeaponValueDefault = int.Parse(weaponDetail[13]),
                    WeaponIntroduce = weaponDetail[14]
                };
                weaponDetailListNew.Add(weaponDetailNew);
            }     
            return weaponDetailListNew;
        }

        public static WeaponDetailModifyState AddWeaponDetail(List<WeaponDetail> weaponDetailList)
        {
            using (ItemContext context = new ItemContext())
            {
                int changeLineNum = 0;
                List<WeaponNameCode> addWeaponDetailName = new List<WeaponNameCode>();
                List<WeaponNameCode> hasWeaponDetailName = new List<WeaponNameCode>();
                foreach (var weaponDetail in weaponDetailList)
                {
                    //Exam if there already has this WeaponDetail
                    WeaponDetail storedWeaponDetail = context.WeaponDetails.SingleOrDefault(x => x.WeaponName == weaponDetail.WeaponName);
                    if (storedWeaponDetail != null)
                    {
                        hasWeaponDetailName.Add(weaponDetail.WeaponName);
                    }
                    else
                    {
                        //Add WeaponDetail into DbSet                
                        var weaponDetailNew = new WeaponDetail
                        {
                            WeaponName = weaponDetail.WeaponName,
                            WeaponType = weaponDetail.WeaponType,
                            RarityDefault = weaponDetail.RarityDefault,
                            SkillDefault = weaponDetail.SkillDefault,
                            PhysicAttackDefault = weaponDetail.PhysicAttackDefault,
                            PhysicDefenceDefault = weaponDetail.PhysicDefenceDefault,
                            ElementTypeDefault = weaponDetail.ElementTypeDefault,
                            ElementAttackDefault = weaponDetail.ElementAttackDefault,
                            ElementDefenceDefault = weaponDetail.ElementDefenceDefault,
                            UnbreakValueDefault = weaponDetail.UnbreakValueDefault,
                            UnbreakRecoverTimesDefault = weaponDetail.UnbreakRecoverTimesDefault,
                            LevelDefault = weaponDetail.LevelDefault,
                            ExperienceDefault = weaponDetail.ExperienceDefault,
                            WeaponValueDefault = weaponDetail.WeaponValueDefault,
                            WeaponIntroduce = weaponDetail.WeaponIntroduce
                        };                        
                        context.WeaponDetails.Add(weaponDetailNew);
                        addWeaponDetailName.Add(weaponDetail.WeaponName);
                    }
                }
                changeLineNum = context.SaveChanges();
                WeaponDetailModifyState state = new WeaponDetailModifyState
                {
                    ModifyWeaponDetailNum = changeLineNum,
                    AddWeaponDetailName = addWeaponDetailName,
                    HasWeaponDetailName = hasWeaponDetailName
                };
                return state;
            }
        }
    }
}
