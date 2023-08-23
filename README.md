## RPGgame

---

클래스를 나누어 개발

Plyaer 클래스, Items 클래스를 객체화 해서 사용

    // 플레이어 객체 생성
    public static Player player;

    // 아이템 객체 생성
    public static Items.Weapon weapon01;
    public static Items.Armor armor01;
    public static Items.Weapon weapon02;
    public static Items.Armor armor02;

아이템 객체를 List로 보관하는 방식을 사용

    // 아이템 리스트 생성
    public static List<Items.Item> items= new List<Items.Item>();

게임 시작시 플레이어와 아이템 정보를 세팅하면서 items리스트에 아이템 정보를 추가

    static void GameDataSetting()
    {
        // 캐릭터 정보 세팅
        player = new Player("Player", "전사", 1, 10, 5, 20, 150);

        // 아이템 정보 세팅
        weapon01 = new Items.Weapon("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2);
        items.Add(weapon01);
        armor01 = new Items.Armor("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 5);
        items.Add(armor01);
        weapon02 = new Items.Weapon("나만의 대검", "나만의 대검 입니다.", 5);
        items.Add(weapon02);
        armor02 = new Items.Armor("천갑옷", "천으로 만들어져 가볍습니다.", 2);
        items.Add(armor02);
    }

단순 출력만을 담당하는 Notice클래스, 선택지를 담당하는 Option클래스
Option클래스를 호출하면 Option클래스가 Notice클래스를 호출하는 식으로 사용

Option클래스에서 전체적인 상호작용이 이루어짐
게임화면을 열거형으로 저장해서 호출하고 선택지를 저장하는 리스트를 생성

    // 선택지 화면 열거형
    public enum Action { Start, Status, Inventory, EquipManage}

    // 선택지 입력받는 리스트
    List<string> optionList = new List<string>();

옵션클래스 전체 메서드

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

