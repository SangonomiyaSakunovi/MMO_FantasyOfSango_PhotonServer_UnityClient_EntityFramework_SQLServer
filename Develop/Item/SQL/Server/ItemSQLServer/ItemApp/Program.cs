using Common.WeaponCode;
using ItemData.Player;
using ItemScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Tools;
using ItemData.Weapon;
using ItemScript.Weapon;
using ItemScript.ModifyState;

namespace ItemApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<WeaponDetail> weaponDetails = 
                WeaponDetailAdd.PackWeaponDetailFromExcel
                ("D:\\FantasyOfSango\\Develop\\Item\\SQL\\Server\\ItemExcel\\WeaponTestExcel.xlsx");
            WeaponDetailModifyState state = WeaponDetailAdd.AddWeaponDetail(weaponDetails);
            Console.WriteLine("修改武器数量");
            Console.WriteLine(state.ModifyWeaponDetailNum);
            Console.WriteLine("已加入武器名称");
            foreach (var item in state.AddWeaponDetailName)
            {
                Console.Write(item + ", ");
            }
            Console.WriteLine("冲突武器名称");
            foreach (var item in state.HasWeaponDetailName)
            {
                Console.Write(item + ", ");
            }            
            Console.ReadKey();
        }
    }
}
