using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    // Самый абстрактный класс Игровой объект
    public abstract class GameObject
    {
        protected int id;
        protected string name;
        protected int x;
        protected int y;

        public GameObject(int id, string name, int x, int y)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
        }

        public int Id => id;
        public string Name => name;
        public int X => x;
        public int Y => y;
    }

    // Интерфейс Движения
    public interface IMoveable
    {
        void Move(int deltaX, int deltaY);
    }

    // Интерфейс Атаки
    public interface IAttacker
    {
        void Attack(Unit unit);
    }

    // Абстрактный класс Юнит наследует от класса Игровой объект
    public abstract class Unit : GameObject
    {
        public float hp;

        public Unit(int id, string name, int x, int y, float hp) : base(id, name, x, y)
        {
            this.hp = hp;
        }

        public bool IsAlive => hp > 0;
        public float Hp => hp;

        public void ReceiveDamage(float damage)
        {
            hp = Math.Max(0, hp - damage);
        }
    }

    // Класс Лучник наследует от классов Юнит, имеет интерфейсы Атакующий, Движущийся
    public class Archer : Unit, IAttacker, IMoveable
    {
        public Archer(int id, string name, int x, int y, float hp) : base(id, name, x, y, hp) { }

        public void Attack(Unit unit)
        {
            if (IsAlive)
            {
                const float damage = 10f;
                unit.ReceiveDamage(damage);
                Console.WriteLine($"{Name} attacked {unit.Name} for {damage} damage!");
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            Console.WriteLine($"{Name} moved from ({X}, {Y}) to ({X + deltaX}, {Y + deltaY}).");
        }
    }

    // Абстрактный класс Строение наследует от класса Игровой объект
    public abstract class Building : GameObject
    {
        public bool IsBuilt { get; }

        public Building(int id, string name, int x, int y, bool isBuilt) : base(id, name, x, y)
        {
            IsBuilt = isBuilt;
        }
    }

    // Класс Крепости наследует от классов Строение, имеет интерфейс Атакующий
    public class Fort : Building, IAttacker
    {
        public Fort(int id, string name, int x, int y, bool isBuilt) : base(id, name, x, y, isBuilt) { }

        public void Attack(Unit unit)
        {
            if (IsBuilt)
            {
                const float damage = 20f;
                unit.ReceiveDamage(damage);
                Console.WriteLine($"{Name} attacked {unit.Name} for {damage} damage from a distance!");
            }
        }
    }

    // Класс Мобильного Дома наследует от класса Строение, имеет интерфейс Движения
    public class MobileHouse : Building, IMoveable
    {
        public MobileHouse(int id, string name, int x, int y, bool isBuilt) : base(id, name, x, y, isBuilt) { }

        public void Move(int deltaX, int deltaY)
        {
            Console.WriteLine($"{Name} moved from ({X}, {Y}) to ({X + deltaX}, {Y + deltaY}).");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Тест создания объектов
            Archer archer = new Archer(1, "Archer", 0, 0, 100);
            Fort fort = new Fort(2, "Fort", 5, 5, true);
            MobileHouse mobileHouse = new MobileHouse(3, "mobileHouse", 10, 10, true);

            // Тест передвижения и атаки
            archer.Move(3, 3);
            fort.Attack(archer);
            mobileHouse.Move(6, 6);

        }
    }
}