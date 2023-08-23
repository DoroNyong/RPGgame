using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHyeongyo_RPG
{
    // 선택지와 관련된 클래스를 별도로 만듬
    public class Option
    {
        // 선택지 열거형
        public enum Action { Start, Status, Inventory, EquipManage}

        // 출력 클래스 호출
        Notice notice = new Notice();

        // 선택지 입력받는 리스트
        List<string> optionList = new List<string>();

        // 선택지 전체 메서드
        public void option(Action action)
        {
            // 콘솔창과 선택지 리스트 초기화
            Console.Clear();
            optionList.Clear();

            // option()의 매개변수를 비교하여 조건에 맞는 메서드 실행
            // 메서드 실행시 출력 클래스에서 가져온 메서드 실행후 선택지 리스트에 선택지를 추가
            switch (action)
            {
                case Action.Start:
                    notice.start();
                    optionList.Add("상태 보기");
                    optionList.Add("인벤토리");
                    break;
                case Action.Status:
                    notice.status();
                    optionList.Add("시작화면으로");
                    break;
                case Action.Inventory:
                    notice.inventory();
                    optionList.Add("장착 관리");
                    optionList.Add("아이템 정렬");
                    optionList.Add("시작화면으로");
                    break;
                // 장비 관리 화면은 프로그램 클래스에서 아이템 정보 세팅한 정보를 가져옴
                case Action.EquipManage:
                    notice.equipManage();
                    for (int i = 0; i < Program.items.Count; i++)
                    {
                        if (Program.items[i].IsEquip)
                        {
                            if (Program.items[i] is Items.Weapon)
                            {
                                optionList.Add($"- [E]{Program.items[i].Name}\t│ 공격력 +{Program.items[i].Atk} │ {Program.items[i].Explanation}");
                            }
                            else if (Program.items[i] is Items.Armor)
                            {
                                optionList.Add($"- [E]{Program.items[i].Name}\t│ 방어력 +{Program.items[i].Def} │ {Program.items[i].Explanation}");
                            }
                        }
                        else
                        {
                            if (Program.items[i] is Items.Weapon)
                            {
                                optionList.Add($"- {Program.items[i].Name}\t│ 공격력 +{Program.items[i].Atk} │ {Program.items[i].Explanation}");
                            }
                            else if (Program.items[i] is Items.Armor)
                            {
                                optionList.Add($"- {Program.items[i].Name}\t│ 방어력 +{Program.items[i].Def} │ {Program.items[i].Explanation}");
                            }
                        }
                    }
                    optionList.Add("인벤토리");
                    break;
            }

            // 선택지 출력
            for (int i = 0; i < optionList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {optionList[i]}");
            }
            // 숫자 대기 메서드 실행
            next(1, optionList.Count);
        }

        // 숫자 대기 메서드
        public void next(int min, int max)
        {
            notice.next();
            int a = CheckValidInput(min, max);
            if (optionList[a - 1].Contains("-"))
            {
                if (optionList[a - 1].Contains("[E]"))
                {
                    if (Program.items[a - 1] is Items.Weapon)
                    {
                        Program.player.EquipAtk -= Program.items[a - 1].Atk;
                    }
                    else if (Program.items[a - 1] is Items.Armor)
                    {
                        Program.player.EquipDef -= Program.items[a - 1].Def;
                    }
                    Program.items[a - 1].IsEquip = false;
                    option(Action.EquipManage);
                }
                else
                {
                    if (Program.items[a - 1] is Items.Weapon)
                    {
                        Program.player.EquipAtk += Program.items[a - 1].Atk;
                    }
                    else if (Program.items[a - 1] is Items.Armor)
                    {
                        Program.player.EquipDef += Program.items[a - 1].Def;
                    }
                    Program.items[a - 1].IsEquip = true;
                    option(Action.EquipManage);
                }
            }
            else
            {
                switch (optionList[a - 1])
                {
                    case "상태 보기":
                        option(Action.Status);
                        break;
                    case "인벤토리":
                        option(Action.Inventory);
                        break;
                    case "시작화면으로":
                        option(Action.Start);
                        break;
                    case "장착 관리":
                        option(Action.EquipManage);
                        break;
                    case "아이템 정렬":
                        // 공백 제거후 길이 비교
                        Program.items = Program.items.OrderByDescending(x => string.Concat(x.Name.Where(c => !Char.IsWhiteSpace(c))).Length).ToList();
                        option(Action.Inventory);
                        break;
                }
            }
        }
        // 숫자 체크 메서드
        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }

                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
