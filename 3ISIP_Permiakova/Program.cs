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

class Enemy
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public int Attack;
    public int Defense;
    public bool IgnoreDefense = false;
    public bool HasCrit = false;
    public int CritChance = 0;
    public bool CanFreeze = false;
    public int FreezeChance = 0;

    public Enemy(int hp, int attack, int defense)
    {
        MaxHP = hp;
        CurrentHP = hp;
        Attack = attack;
        Defense = defense;
    }

    public virtual void AttackPlayer(Player player, Random rnd)
    {
        int dmg = Attack;
        if (!IgnoreDefense)
            dmg -= player.Defense;

        if (player.BlockNextAttack)
        {
            int blockPercent = rnd.Next(70, 101);
            dmg = dmg * (100 - blockPercent) / 100;
            player.BlockNextAttack = false;
            Console.WriteLine($"Блок уменьшил получаемый урон на {blockPercent}%!");
        }

        if (HasCrit && rnd.Next(100) < CritChance)
        {
            dmg *= 2;
            Console.WriteLine($"{Name} нанес критический удар!");
        }

        if (dmg < 1) dmg = 1; // Минимальный урон 1
        player.TakeDamage(dmg);

        if (CanFreeze && rnd.Next(100) < FreezeChance)
        {
            player.IsFrozen = true;
            Console.WriteLine($"{Name} наложил заморозку! Вы пропустите следующий ход.");
        }
    }

    public bool IsAlive()
    {
        return CurrentHP > 0;
    }
}

class Goblin : Enemy
{
    public Goblin() : base(30, 5, 2)
    {
        Name = "Гоблин";
        HasCrit = true;
        CritChance = 20;
    }
}

class Skelet : Enemy
{
    public Skelet() : base(40, 6, 3)
    {
        Name = "Скелет";
        IgnoreDefense = true;
    }
}

class Mag : Enemy
{
    public Mag() : base(25, 4, 2)
    {
        Name = "Маг";
        CanFreeze = true;
        FreezeChance = 20;
    }
}
