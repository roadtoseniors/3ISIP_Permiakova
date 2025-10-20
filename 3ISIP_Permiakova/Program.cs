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

        if (dmg < 1) dmg = 1;
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

class GoblinBoss : Goblin
{
    public GoblinBoss()
    {
        Name = "ВВГ Гоблин-Босс";
        MaxHP = 30 * 2;
        CurrentHP = MaxHP;
        Attack = (int)(5 * 1.5);
        Defense = (int)(2 * 1.2);
        CritChance += 10;
    }
}

class SkeletBossKova : Skelet
{
    public SkeletBossKova()
    {
        Name = "Скелет-Ковальский";
        MaxHP = (int)(40 * 2.5);
        CurrentHP = MaxHP;
        Attack = (int)(6 * 1.3);
        Defense = (int)(3 * 1.4);
    }
}

class SkeletBossPest : Skelet
{
    public SkeletBossPest()
    {
        Name = "Скелет-Пестов";
        MaxHP = (int)(40 * 1.3);
        CurrentHP = MaxHP;
        Attack = (int)(6 * 1.8);
        Defense = (int)(3 * 0.6);
    }
}

class MagBoss : Mag
{
    public MagBoss()
    {
        Name = "Архимаг C++";
        MaxHP = (int)(25 * 1.8);
        CurrentHP = MaxHP;
        Attack = (int)(4 * 1.6);
        Defense = (int)(2 * 1.1);
        FreezeChance += 10;
    }
}

class Game
{
    static Random rnd = new Random();

    public static Enemy GenerateRandomEnemy(bool isBoss = false)
    {
        if (isBoss)
        {
            int roll = rnd.Next(4);
            switch (roll)
            {
                case 0: return new GoblinBoss();
                case 1: return new SkeletBossKova();
                case 2: return new SkeletBossPest();
                case 3: return new MagBoss();
            }
        }
        else
        {
            int roll = rnd.Next(3);
            switch (roll)
            {
                case 0: return new Goblin();
                case 1: return new Skelet();
                case 2: return new Mag();
            }
        }
        return null;
    }

    public static void OpenChest(Player player)
    {
        int roll = rnd.Next(3);
        if (roll == 0)
            player.Heal();
        else if (roll == 1)
        {
            int atk = rnd.Next(5, 16);
            player.EquipWeapon($"Меч +{atk}", atk);
        }
        else
        {
            int def = rnd.Next(1, 11);
            player.EquipArmor($"Броня +{def}", def);
        }
    }

    public static void Battle(Player player, Enemy enemy)
    {
        Console.WriteLine($"\nВы столкнулись с {enemy.Name}!");
        while (player.IsAlive() && enemy.IsAlive())
        {
            if (!player.IsFrozen)
            {
                Console.Write("Ваш ход! 1 - Атака, 2 - Защита: ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    int dmg = player.Attack - enemy.Defense;
                    if (dmg < 1) dmg = 1;
                    enemy.CurrentHP -= dmg;
                    Console.WriteLine($"Вы нанесли {dmg} урона {enemy.Name}! HP врага: {enemy.CurrentHP}/{enemy.MaxHP}");
                }
                else
                {
                    if (!player.TryDodge(rnd))
                    {
                        player.BlockNextAttack = true;
                        Console.WriteLine("Уклонение не удалось, блок уменьшит получаемый урон!");
                    }
                }
            }
            else
            {
                Console.WriteLine("Вы пропускаете ход из-за заморозки!");
                player.IsFrozen = false;
            }

            if (enemy.IsAlive())
                enemy.AttackPlayer(player, rnd);
        }

        if (player.IsAlive())
            Console.WriteLine($"Вы победили {enemy.Name}!\n");
        else
            Console.WriteLine("Вы были побеждены...\n");
    }

    public static void MainGame()
    {
        Player player = new Player("Герой", 100, 10, 5);
        int turn = 1;

        while (player.IsAlive())
        {
            Console.WriteLine($"\n=== Ход {turn} ===");
            bool isBossTurn = (turn % 10 == 0);
            bool chestEvent = rnd.Next(2) == 0;

            if (chestEvent && !isBossTurn)
            {
                Console.WriteLine("Вы нашли сундук!");
                OpenChest(player);
            }
            else
            {
                Enemy enemy = GenerateRandomEnemy(isBossTurn);
                Battle(player, enemy);
                if (!player.IsAlive()) break;
            }

            turn++;
        }

        Console.WriteLine($"Игра окончена! Вы прошли {turn - 1} ходов.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Game.MainGame();
    }
}
