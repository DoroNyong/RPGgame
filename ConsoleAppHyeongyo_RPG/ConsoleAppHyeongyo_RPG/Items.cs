using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHyeongyo_RPG
{
    // 아이템 클래스
    public class Items
    {
        // 아이템의 위치를 열거형으로 표현
        public enum Placed { Inventory, Store }

        // 아이템 클래스
        public class Item
        {
            public string Name { get; set; }
            public string Explanation { get; set; }
            public Placed Placed { get; set; } = Placed.Inventory;
            public bool IsEquip { get; set; } = false;
            public int Atk { get; set; } = 0;
            public int Def { get; set; } = 0;
        }

        // 아이템 클래스를 상속받은 무기 클래스
        public class Weapon : Item
        {
            // 초기 아이템 위치에 따른 생성자 나누기
            public Weapon(string name, string explanation, int atk)
            {
                Name = name;
                Explanation = explanation;
                Atk = atk;
            }
            public Weapon(string name, string explanation, int atk, Placed placed)
            {
                Name = name;
                Explanation = explanation;
                Atk = atk;
                Placed = placed;
            }
        }

        // 아이템 클래스를 상속받은 방어구 클래스
        public class Armor : Item
        {
            // 초기 아이템 위치에 따른 생성자 나누기
            public Armor(string name, string explanation, int def)
            {
                Name = name;
                Explanation = explanation;
                Def = def;
            }
            public Armor(string name, string explanation, int def, Placed placed)
            {
                Name = name;
                Explanation = explanation;
                Def = def;
                Placed = placed;
            }
        }
    }
}
