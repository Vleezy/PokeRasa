﻿using System;
using UnityEngine;

public abstract class ItemData
{
    public string itemName;
    public int price;
    public ItemType type;
    public string graphicsPath;
    public int flingPower = 30;
    public MoveEffect flingEffect = MoveEffect.None;
    public abstract int[] ItemSubdata { get; }

    public Texture2D ItemSprite => Resources.Load<Texture2D>("Sprites/Items/" + graphicsPath);
}

public class AbstractItem : ItemData //subdata length 0
{
    public override int[] ItemSubdata => new int[0];
    public AbstractItem()
    {
        type = ItemType.AbstractItem;
    }
}

public class FieldItem : ItemData //subdata length 2
{
    public FieldEffect fieldEffect;
    public int fieldEffectIntensity;
    public override int[] ItemSubdata => new int[2]
        {
            (int)fieldEffect,
            fieldEffectIntensity,
        };

    public FieldItem()
    {
        type = ItemType.FieldItem;
    }

}

public class BattleItem : ItemData //subdata length 2
{
    public BattleItemEffect battleEffect;
    public int battleEffectIntensity;
    public override int[] ItemSubdata => new int[2]
        {
            (int)battleEffect,
            battleEffectIntensity,
        };
    public BattleItem()
    {
        type = ItemType.BattleItem;
    }
}

public class HeldItem : ItemData //subdata length 2
{
    public HeldEffect heldEffect;
    public int heldEffectIntensity;
    public override int[] ItemSubdata => new int[2]
    {
        (int)heldEffect,
        heldEffectIntensity,
    };
    public HeldItem()
    {
        type = ItemType.HeldItem;
    }
}

public class PlateItem : HeldItem //subdata length 3
{
    public Type plateType;
    public override int[] ItemSubdata => new int[3]
    {
        (int)heldEffect,
        heldEffectIntensity,
        (int)plateType,
    };
    public PlateItem()
    {
        type = ItemType.Plate;
    }
}

public class HeldFieldItem : ItemData //subdata length 4
{
    public HeldEffect heldEffect;
    public int heldEffectIntensity;
    public FieldEffect fieldEffect;
    public int fieldEffectIntensity;
    public override int[] ItemSubdata => new int[4]
    {
        (int)heldEffect,
        heldEffectIntensity,
        (int)fieldEffect,
        fieldEffectIntensity,
    };
}

public class Berry : ItemData //subdata length 8
{
    public HeldEffect heldEffect;
    public int heldEffectIntensity;
    public BattleItemEffect battleEffect;
    public int battleEffectIntensity;
    public FieldEffect fieldEffect;
    public int fieldEffectIntensity;
    public BerryEffect berryEffect;
    public int hoursToGrow;
    public override int[] ItemSubdata => new int[8]
    {
        (int)heldEffect,
        heldEffectIntensity,
        (int)battleEffect,
        battleEffectIntensity,
        (int)fieldEffect,
        fieldEffectIntensity,
        (int)berryEffect,
        hoursToGrow,
    };
    public Berry()
    {
        type = ItemType.Berry;
        flingPower = 10;
    }
}

public class Medicine : ItemData //subdata length 4
{
    public FieldEffect fieldEffect;
    public int fieldEffectIntensity;
    public BattleItemEffect battleEffect;
    public int battleEffectIntensity;
    public override int[] ItemSubdata => new int[4]
        {
            (int)fieldEffect,
            fieldEffectIntensity,
            (int)battleEffect,
            battleEffectIntensity,
        };
    public Medicine()
    {
        type = ItemType.Medicine;
    }
}

public class TM : ItemData //subdata length 1
{
    public TMID TMID;
    public override int[] ItemSubdata => new int[1] { (int)TMID };
    public TM()
    {
        type = ItemType.TM;
        flingPower = 0;
    }
}

public class MegaStone : ItemData //subdata length 2
{
    public SpeciesID originalSpecies;
    public SpeciesID destinationSpecies;
    public override int[] ItemSubdata => new int[2]
    {
        (int)originalSpecies,
        (int)destinationSpecies,
    };
    public MegaStone()
    {
        type = ItemType.MegaStone;
        flingPower = 80;
    }
}

public class KeyItem : ItemData //subdata length 1
{
    public int KeyItemID;
    public override int[] ItemSubdata => new int[1] { KeyItemID };
    public KeyItem()
    {
        type = ItemType.KeyItem;
        flingPower = 0;
    }
}

public static class ItemUtils
{
    public static HeldEffect HeldEffect(this ItemID item)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.HeldItem:
            case ItemType.HeldFieldItem:
            case ItemType.Berry:
                return (HeldEffect)Item.ItemTable[(int)item].ItemSubdata[0];
            default:
                return global::HeldEffect.None;
        }
    }
    public static int HeldEffectIntensity(this ItemID item)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.HeldItem:
            case ItemType.HeldFieldItem:
            case ItemType.Berry:
                return Item.ItemTable[(int)item].ItemSubdata[1];
            default:
                return 0;
        }
    }
    public static FieldEffect FieldEffect(this ItemID item)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.FieldItem:
            case ItemType.Medicine:
                return (FieldEffect)Item.ItemTable[(int)item].ItemSubdata[0];
            case ItemType.HeldFieldItem:
                return (FieldEffect)Item.ItemTable[(int)item].ItemSubdata[2];
            case ItemType.Berry:
                return (FieldEffect)Item.ItemTable[(int)item].ItemSubdata[4];
            default:
                return global::FieldEffect.None;
        }
    }
    public static int FieldEffectIntensity(this ItemID item)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.FieldItem:
            case ItemType.Medicine:
                return Item.ItemTable[(int)item].ItemSubdata[1];
            case ItemType.HeldFieldItem:
                return Item.ItemTable[(int)item].ItemSubdata[3];
            case ItemType.Berry:
                return Item.ItemTable[(int)item].ItemSubdata[5];
            default:
                return 0;
        }
    }

    public static BerryEffect BerryEffect(this ItemID item)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.Berry:
                return (BerryEffect)Item.ItemTable[(int)item].ItemSubdata[6];
            default:
                return global::BerryEffect.None;
        }
    }

    public static SpeciesID MegaStoneUser(this ItemID item)
    {
        switch (Item.ItemTable[(int)item].type)
        {
            case ItemType.MegaStone:
                return (SpeciesID)Item.ItemTable[(int)item].ItemSubdata[0];
            default:
                return SpeciesID.Missingno;
        }
    }

    public static Type PlateType(this ItemID item)
    {
        switch(item.Data().type)
        {
            case ItemType.Plate:
                return (Type)item.Data().ItemSubdata[2];
            default:
                return Type.Typeless;
        }
    }

    public static ItemData Data(this ItemID item) => Item.ItemTable[(int)item];
    public static Sprite Sprite(this ItemID item) => UnityEngine.Sprite.Create(
        Resources.Load<Texture2D>("Sprites/Items/" + item.Data().graphicsPath),
        new(0F, 0F, 24F, 24F), new(0.5F, 0.5F));
}