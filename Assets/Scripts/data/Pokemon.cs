using static System.Math;
using static System.Convert;
using System;

[Serializable]
public class Pokemon : ICloneable
{
    public SpeciesID species;


    public string monName;
    public Gender gender;

    public int xp;
    public int level;
    public int currentLevelXP => XP.LevelToXP(level, SpeciesData.xpClass);
    public int nextLevelXP => XP.LevelToXP(level + 1, SpeciesData.xpClass);
    public float levelProgress => level >= PokemonConst.maxLevel ? 0 :
            (float)(xp - currentLevelXP) / (nextLevelXP - currentLevelXP);

    private int ivHP;
    private int ivAttack;
    private int ivDefense;
    private int ivSpAtk;
    private int ivSpDef;
    private int ivSpeed;

    public int evHP;
    public int evAttack;
    public int evDefense;
    public int evSpAtk;
    public int evSpDef;
    public int evSpeed;
    public int totalEv;

    private Nature nature;
    public Nature Nature => nature;
    public int whichAbility;

    public int hpMax;
    public int attack;
    public int defense;
    public int spAtk;
    public int spDef;
    public int speed;

    public MoveID move1;
    public int maxPp1;
    public int pp1;
    public MoveID move2;
    public int maxPp2;
    public int pp2;
    public MoveID move3;
    public int maxPp3;
    public int pp3;
    public MoveID move4;
    public int maxPp4;
    public int pp4;

    public int HP;
    public Status status;
    public int sleepTurns;

    public bool fainted;

    public bool exists;

    public ItemID item;
    public bool itemChanged;
    public ItemID newItem;

    public bool canBelch;

    public ItemID CurrentItem => itemChanged ? newItem : item;

    public bool onField = false;
    public int lastIndex = 0;

    public bool transformed;
    public SpeciesID temporarySpecies;

    public int friendship;

    public Type hiddenPowerType;

    public bool gainedLevel;

    public int id;

    public bool checkEvolution;

    public bool UsesFemaleSprites => SpeciesData.genderDifferences && gender == Gender.Female;



    public SpeciesData SpeciesData => Species.SpeciesTable[transformed ?
        (int)temporarySpecies : (int)species];

    public SpeciesID getSpecies => transformed ? temporarySpecies : species;

    public MoveID[] MoveIDs => new MoveID[4]
    {
        move1,
        move2,
        move3,
        move4
    };

    public int[] PP => new int[4]
    {
        pp1,
        pp2,
        pp3,
        pp4
    };

    public bool HasMove(MoveID move) => move1 == move || move2 == move || move3 == move || move4 == move;

    public Ability GetAbility => SpeciesData.abilities[whichAbility];

    public void SetTransformPP()
    {
        pp1 = 5;
        pp2 = 5;
        pp3 = 5;
        pp4 = 5;
    }

    public void SetEvIv(EvIvSpread spread)
    {
        ivHP = spread.ivHP;
        ivAttack = spread.ivAttack;
        ivDefense = spread.ivDefense;
        ivSpAtk = spread.ivSpAtk;
        ivSpDef = spread.ivSpDef;
        ivSpeed = spread.ivSpeed;

        evHP = spread.evHP;
        evAttack = spread.evAttack;
        evDefense = spread.evDefense;
        evSpAtk = spread.evSpAtk;
        evSpDef = spread.evSpDef;
        evSpeed = spread.evSpeed;
        CalculateStats();
    }

    public void SetNature(Nature nature) => this.nature = nature;

    public void AbilityCapsule()
    {
        whichAbility = whichAbility switch
        {
            0 => 1,
            1 => 0,
            2 => 2,
            _ => whichAbility
        };
    }

    private int CalculateHPMax()
    {
        return (((2 * SpeciesData.baseHP) + ivHP + (evHP >> 2)) * level / 100 + level + 10);
    }

    private int CalculateStat(Stat stat, int baseStat, int statIv, int statEv)
    {
        return (int)Floor((((2 * baseStat) + statIv + (statEv >> 2)) * level / 100 + 5) * NatureUtils.NatureEffect(nature, stat));
    }

    public void CalculateStats()
    {
        hpMax = CalculateHPMax();
        attack = CalculateStat(Stat.Attack, SpeciesData.baseAttack, ivAttack, evAttack);
        defense = CalculateStat(Stat.Defense, SpeciesData.baseDefense, ivDefense, evDefense);
        spAtk = CalculateStat(Stat.SpAtk, SpeciesData.baseSpAtk, ivSpAtk, evSpAtk);
        spDef = CalculateStat(Stat.SpDef, SpeciesData.baseSpDef, ivSpDef, evSpDef);
        speed = CalculateStat(Stat.Speed, SpeciesData.baseSpeed, ivSpeed, evSpeed);
    }

    public bool ShouldLevelUp()
    {
        return level < PokemonConst.maxLevel && xp > nextLevelXP;
    }
    public (bool, SpeciesID) ShouldEvolve()
    {
        for (int i = 0; i < SpeciesData.evolution.Length; i++)
        {
            if (CheckEvolutionMethod(SpeciesData.evolution[i].Method, SpeciesData.evolution[i].Data))
            {
                return (true, SpeciesData.evolution[i].Destination);
            }
        }
        return (false, SpeciesID.Missingno);
    }
    public bool CheckEvolutionMethod(EvolutionMethod method, int data)
    {
        switch (method)
        {
            case EvolutionMethod.LevelUp:
                return level >= data;
            default:
            case EvolutionMethod.Never:
                return false;
        }
    }
    public void LevelUp()
    {
        level++;
        int maxHpBefore = hpMax;
        CalculateStats();
        HP += hpMax - maxHpBefore;
    }
    public int Moves
        => move2 == MoveID.None ? 1 : move3 == MoveID.None ? 2 : move4 == MoveID.None ? 3 : 4;
    public void AddMove(int slot, MoveID move)
    {
        switch (slot)
        {
            case 1:
                move1 = move;
                maxPp1 = Move.MoveTable[(int)move].pp;
                pp1 = maxPp1;
                break;
            case 2:
                move2 = move;
                maxPp2 = Move.MoveTable[(int)move].pp;
                pp2 = maxPp2;
                break;
            case 3:
                move3 = move;
                maxPp3 = Move.MoveTable[(int)move].pp;
                pp3 = maxPp3;
                break;
            case 4:
                move4 = move;
                maxPp4 = Move.MoveTable[(int)move].pp;
                pp4 = maxPp4;
                break;
        }
    }
    public Pokemon(SpeciesID thisSpecies, Gender Gender, int Level,
        int IvHP, int IvAttack, int IvDefense, int IvSpAtk, int IvSpDef, int IvSpeed,
        int EvHP, int EvAttack, int EvDefense, int EvSpAtk, int EvSpDef, int EvSpeed,
        Nature thisNature, MoveID Move1, MoveID Move2, MoveID Move3, MoveID Move4,
        int WhichAbility, int Friendship, ItemID Item, Type HiddenPowerType, bool Exists = true)
    {
        species = thisSpecies;
        gender = Gender;

        monName = SpeciesData.speciesName;

        level = Level;

        ivHP = IvHP;
        ivAttack = IvAttack;
        ivDefense = IvDefense;
        ivSpAtk = IvSpAtk;
        ivSpDef = IvSpDef;
        ivSpeed = IvSpeed;

        evHP = EvHP;
        evAttack = EvAttack;
        evDefense = EvDefense;
        evSpAtk = EvSpAtk;
        evSpDef = EvSpDef;
        evSpeed = EvSpeed;
        totalEv = evHP + evAttack + evDefense + evSpAtk + evSpDef + evSpeed;

        nature = thisNature;
        whichAbility = WhichAbility;

        xp = XP.LevelToXP(level, SpeciesData.xpClass);

        hpMax = ToUInt16(((2 * SpeciesData.baseHP) + ivHP) * level / 100 + level + 10);
        attack = ToUInt16(Floor((((2 * SpeciesData.baseAttack) + ivAttack + (evHP >> 2)) * level / 100 + 5) * nature.NatureEffect(Stat.Attack)));
        defense = ToUInt16(Floor((((2 * SpeciesData.baseDefense) + ivDefense) * level / 100 + 5) * nature.NatureEffect(Stat.Defense)));
        spAtk = ToUInt16(Floor((((2 * SpeciesData.baseSpAtk) + ivSpAtk) * level / 100 + 5) * nature.NatureEffect(Stat.SpAtk)));
        spDef = ToUInt16(Floor((((2 * SpeciesData.baseSpDef) + ivSpDef) * level / 100 + 5) * nature.NatureEffect(Stat.SpDef)));
        speed = ToUInt16(Floor((((2 * SpeciesData.baseSpeed) + ivSpeed) * level / 100 + 5) * nature.NatureEffect(Stat.Speed)));

        move1 = Move1;
        move2 = Move2;
        move3 = Move3;
        move4 = Move4;

        maxPp1 = Move.MoveTable[(int)move1].pp;
        pp1 = maxPp1;
        maxPp2 = Move.MoveTable[(int)move2].pp;
        pp2 = maxPp2;
        maxPp3 = Move.MoveTable[(int)move3].pp;
        pp3 = maxPp3;
        maxPp4 = Move.MoveTable[(int)move4].pp;
        pp4 = maxPp4;

        HP = hpMax;
        status = Status.None;
        sleepTurns = 0;

        var random = new System.Random();
        id = random.Next();

        hiddenPowerType = HiddenPowerType;

        fainted = false;

        transformed = false;
        temporarySpecies = species;

        friendship = Friendship;
        item = Item;

        exists = Exists;
    }
    public static Pokemon WildPokemon(SpeciesID species, int level)
    {
        var random = new System.Random();
        int IvHP = random.Next() & 31;
        int IvAttack = random.Next() & 31;
        int IvDefense = random.Next() & 31;
        int IvSpAtk = random.Next() & 31;
        int IvSpDef = random.Next() & 31;
        int IvSpeed = random.Next() & 31;

        int personality = random.Next();
        Gender gender = Species.SpeciesTable[(int)species].malePercent == SpeciesData.Genderless
            ? Gender.Genderless
            : personality % 100 > Species.SpeciesTable[(int)species].malePercent
            ? Gender.Female : Gender.Male;
        Nature Nature = (Nature)((personality >> 1) % 25);

        MoveID[] Moves = Learnset.GetMoves(Species.SpeciesTable[(int)species].learnset, level);

        return new Pokemon(species, gender, level, IvHP, IvAttack, IvDefense, IvSpAtk, IvSpDef, IvSpeed,
            0, 0, 0, 0, 0, 0,
            Nature, Moves[0], Moves[1], Moves[2], Moves[3], (int)Floor(random.NextDouble() * 2),
            Species.SpeciesTable[(int)species].baseFriendship, ItemID.None, (Type)(random.Next() % 18));
    }
    public static Pokemon MakeEmptyMon => new
        (SpeciesID.Missingno, Gender.Genderless, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, Nature.Serious, MoveID.None, MoveID.None, MoveID.None, MoveID.None, 0, 0, ItemID.None, Type.Normal, false);
    public object Clone() => MemberwiseClone() as Pokemon;
}