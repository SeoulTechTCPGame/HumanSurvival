using Enums;

public class CollectionClass
{
    public string Name;
    public string Explain;
    public string Rank;

    public virtual bool IsComplete()
    {
        return false;
    }
}

public class CollectionWhip: CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Whip);
    }
}

public class CollectionBloodyTear : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Whip) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Whip).IsEvoluction();
    }
}

public class CollectionMagicWand : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.MagicWand);
    }
}

public class CollectionHolyWand : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.MagicWand) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.MagicWand).IsEvoluction();
    }
}

public class CollectionKnife : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Knife);
    }
}

public class CollectionThousandEdge : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Knife) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Knife).IsEvoluction();
    }
}

public class CollectionCross : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Cross);
    }
}

public class CollectionHeavenSword : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Cross) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Cross).IsEvoluction();
    }
}

public class CollectionKingBible : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.KingBible);
    }
}

public class CollectionUnHolyVespers : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.KingBible) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.KingBible).IsEvoluction();
    }
}

public class CollectionFireWand : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.FireWand);
    }
}

public class CollectionHellfire : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.FireWand) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.FireWand).IsEvoluction();
    }
}

public class CollectionGarlic : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Garlic);
    }
}

public class CollectionSoulEater : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Garlic) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Garlic).IsEvoluction();
    }
}

public class CollectionPeachone : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Peachone);
    }
}

public class CollectionEbonyWings : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.EbonyWings);
    }
}

public class CollectionVandalier : CollectionClass
{
    public override bool IsComplete()
    {
        return (GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Peachone) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Peachone).IsEvoluction())
            || (GameManager.instance.EquipManageSys.HasWeapon(EWeapon.EbonyWings) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.EbonyWings).IsEvoluction());
    }
}

public class CollectionLightningRing : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.LightningRing);
    }
}

public class CollectionThunderLoop : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.LightningRing) && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.LightningRing).IsEvoluction();
    }
}

public class CollectionSpinach : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Spinach);
    }
}

public class CollectionArmor : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Armor);
    }
}

public class CollectionHollowHeart : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.HollowHeart);
    }
}

public class CollectionPummarola : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Pummarola);
    }
}

public class CollectionEmptyTome : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.EmptyTome);
    }
}

public class CollectionCandelabrador : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Candelabrador);
    }
}

public class CollectionBracer : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Bracer);
    }
}

public class CollectionDuplicator : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Duplicator);
    }
}

public class CollectionSpellbinder : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Spellbinder);
    }
}

public class CollectionWings : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Wings);
    }
}

public class CollectionAttractorb : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Attractorb);
    }
}

public class CollectionClover : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Clover);
    }
}

public class CollectionCrown : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Crown);
    }
}

public class CollectionStoneMask : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.StoneMask);
    }
}

public class CollectionSkull : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasAcc(EAccessory.Skull);
    }
}

public class CollectionExp : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.Level > 1 || GameManager.instance.Exp > 0;
    }
}

public class CollectionGold : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.Coin > 0;
    }
}

public class CollectionHeart : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.RestoreCount > 0;
    }
}

public class CollectionChest : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.BGetChest;
    }
}

public class CollectionNecromancer99 : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.Necromancer && GameManager.instance.Level >= 99;
    }
}

public class CollectionBarbarian99 : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.Barbarian && GameManager.instance.Level >= 99;
    }
}

public class CollectionAlchemist99 : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.Alchemist && GameManager.instance.Level >= 99;
    }
}

public class CollectionRogue99 : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.Rogue && GameManager.instance.Level >= 99;
    }
}

public class CollectionStormMage99 : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.StormMage && GameManager.instance.Level >= 99;
    }
}

public class CollectionFireMage : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.FireMage && GameManager.instance.Level >= 99;
    }
}

public class CollectionWarlock99 : CollectionClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.Warlock && GameManager.instance.Level >= 99;
    }
}

public class CollectionStage1Survive31min : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameStage == EStage.MadForest && GameManager.instance.GameTime >=  31 * 60;
    }
}

public class CollectionStage2Survive31min : CollectionClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameStage == EStage.InlaidLibrary && GameManager.instance.GameTime >= 31 * 60;
    }
}