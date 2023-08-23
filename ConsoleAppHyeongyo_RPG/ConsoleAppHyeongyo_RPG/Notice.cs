using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHyeongyo_RPG
{
    // 출력에 관련된 메서드
    public class Notice
    {
        //// 단순 출력 메서드
        // 오류 출력 메서드
        public void warning() 
        { 
            Console.WriteLine("잘못된 입력입니다.");
        }
        // 숫자 대기 출력 메서드
        public void next()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }
        // 시작화면 출력 메서드
        public void start()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
        }
        // 상태 확인 화면 출력 메서드
        public void status()
        {
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("Lv. " + Program.player.Level);
            Console.WriteLine($"{Program.player.Name} ( {Program.player.Job} )");
            // 플레이어의 장착공격력과 장착방어력을 확인해서 그에 따른 결과값 출력
            if ( Program.player.EquipAtk != 0 )
            {
                Console.WriteLine($"공격력 : {Program.player.Atk + Program.player.EquipAtk} (+{Program.player.EquipAtk})");
            }
            else
            {
                Console.WriteLine("공격력 : " + Program.player.Atk);
            }
            if (Program.player.EquipDef != 0)
            {
                Console.WriteLine($"방어력 : {Program.player.Def + Program.player.EquipDef} (+{Program.player.EquipDef})");
            }
            else
            {
                Console.WriteLine("방어력 : " + Program.player.Def);
            }
            Console.WriteLine("체 력 : " + Program.player.Hp);
            Console.WriteLine("Gold : " + Program.player.Gold);
            Console.WriteLine();
        }
        // 인벤토리 화면 출력 메서드
        public void inventory()
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            // 아이템 리스트의 크기만큼 반복문을 돌려서 조건값에 따라 출력을 조정
            for (int i = 0; i < Program.items.Count; i++)
            {
                if (Program.items[i].Placed == Items.Placed.Inventory)
                {
                    switch (Program.items[i].IsEquip)
                    {
                        case true:
                            if (Program.items[i] is Items.Weapon)
                            {
                                Console.WriteLine($"- [E]{Program.items[i].Name}\t│ 공격력 +{Program.items[i].Atk} │ {Program.items[i].Explanation}");
                            }
                            else if (Program.items[i] is Items.Armor)
                            {
                                Console.WriteLine($"- [E]{Program.items[i].Name}\t│ 방어력 +{Program.items[i].Def} │ {Program.items[i].Explanation}");
                            }
                            break;
                        case false:
                            if (Program.items[i] is Items.Weapon)
                            {
                                Console.WriteLine($"- {Program.items[i].Name}\t│ 공격력 +{Program.items[i].Atk} │ {Program.items[i].Explanation}");
                            }
                            else if (Program.items[i] is Items.Armor)
                            {
                                Console.WriteLine($"- {Program.items[i].Name}\t│ 방어력 +{Program.items[i].Def} │ {Program.items[i].Explanation}");
                            }
                            break;
                    }
                }
            }
            Console.WriteLine();
        }
        // 장착 관리 화면 출력 메서드
        public void equipManage()
        {
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
        }
    }
}
