class Player
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int Attack;
    public int Defense;
    public bool IsFrozen = false;
    public bool BlockNextAttack = false;

    public string Weapon = "Пустая рука";
    public string Armor = "Обычная одежда";

    public Player(string name, int hp, int attack, int defense)
    {
        Name = name;
        MaxHP = hp;
        CurrentHP = hp;
        Attack = attack;
        Defense = defense;
    }

    public void Heal()
    {
        CurrentHP = MaxHP;
        Console.WriteLine($"Вы использовали лечебное зелье! HP восстановлено до {CurrentHP}");
    }

    public void EquipWeapon(string newWeapon, int atk)
    {
        Console.WriteLine($"Вы нашли оружие: {newWeapon} (Атака +{atk})");
        Console.WriteLine($"Ваше текущее оружие: {Weapon} (Атака +{Attack})");
        Console.Write("Вы хотите заменить оружие? (1 - Да, 0 - Нет): ");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 1)
        {
            Weapon = newWeapon;
            Attack = atk;
            Console.WriteLine($"Вы экипировали {Weapon}");
        }
        else
        {
            Console.WriteLine("Вы оставили текущее оружие.");
        }
    }

    public void EquipArmor(string newArmor, int def)
    {
        Console.WriteLine($"Вы нашли доспехи: {newArmor} (Защита +{def})");
        Console.WriteLine($"Ваши текущие доспехи: {Armor} (Защита +{Defense})");
        Console.Write("Вы хотите заменить доспехи? (1 - Да, 0 - Нет): ");
        int choice = int.Parse(Console.ReadLine());
        if (choice == 1)
        {
            Armor = newArmor;
            Defense = def;
            Console.WriteLine($"Вы экипировали {Armor}");
        }
        else
        {
            Console.WriteLine("Вы оставили текущие доспехи.");
        }
    }

    public bool TryDodge(Random rnd)
    {
        int chance = rnd.Next(100);
        if (chance < 40)
        {
            Console.WriteLine("Вы успешно увернулись от следующей атаки!");
            return true;
        }
        return false;
    }

    public void TakeDamage(int dmg)
    {
        CurrentHP -= dmg;
        if (CurrentHP < 0) CurrentHP = 0;
        Console.WriteLine($"Вы получили {dmg} урона! HP: {CurrentHP}/{MaxHP}");
    }

    public bool IsAlive()
    {
        return CurrentHP > 0;
    }
}