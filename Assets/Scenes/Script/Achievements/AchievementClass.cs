using Enums;
using UnityEngine;

public class AchievementClass
{
    public string Explain;
    public string Obtain;
    public Sprite Sprite;

    public virtual bool IsComplete()
    {
        return false;
    }
    public virtual void EarnRewards() { }
}

public class ReachLevel5 : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.Level >= 5;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.Wings);
    }
}

public class ReachLevel10 : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.Level >= 10;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.Crown);
    }
}

public class ReachLevel20InStage1 : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameStage == EStage.MadForest && GameManager.instance.Level >= 20;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockStage(EStage.InlaidLibrary);
    }
}

public class Survive1Min : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameTime >= 60;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.HollowHeart);
    }
}

public class Survive5MinPlayGennaro : AchievementClass
{
    public override bool IsComplete()
    {
        return DataManager.instance.CurrentCharcter == ECharacterType.Barbarian && GameManager.instance.GameTime >= 300;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.Pummarola);
    }
}

public class Survive10Min : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.GameTime >= 600;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockWeapon(EWeapon.Peachone);
    }
}

public class ReachLevel4KingBible : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.KingBible)
            && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.KingBible).WeaponLevel >= 4;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.Bracer);
    }
}

public class ReachLevel4FireWand : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.FireWand)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.FireWand).WeaponLevel >= 4;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockCharacter(ECharacterType.FireMage);
    }
}

public class ReachLevel4LightningRing : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.LightningRing)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.LightningRing).WeaponLevel >= 4;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockCharacter(ECharacterType.Necromancer);
    }
}

public class ReachLevel7MagicWand : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.MagicWand)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.MagicWand).WeaponLevel >= 7;
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.Duplicator);
    }
}

public class ReachLevel7Peachone : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Peachone)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Peachone).WeaponLevel >= 7;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockWeapon(EWeapon.EbonyWings);
    }
}

public class ReachLevel7Garlic : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Garlic)
                    && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Garlic).WeaponLevel >= 7;
    }
    public override void EarnRewards()
    {
        UserInfo.instance.UnlockCharacter(ECharacterType.Warlock);
    }
}

public class Possession6Weapons : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.Weapons.Count >= 6;
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UnlockAccessory(EAccessory.EmptyTome);
    }
}

public class Cumulative1000Recovery : AchievementClass
{
    public override bool IsComplete()
    {
        return UserInfo.instance.UserDataSet.AccRestore >= 1000;
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UnlockCharacter(ECharacterType.Alchemist);
    }
}

public class Destroy20LightObject : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.DestroyCount >= 20;
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UnlockWeapon(EWeapon.FireWand);
    }
}

public class Found5Chickens : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.FoundChickenCount >= 5;
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UnlockWeapon(EWeapon.Garlic);
    }
}

public class AccKillCount3000 : AchievementClass
{
    public override bool IsComplete()
    {
        return UserInfo.instance.UserDataSet.AccKill >= 3000;
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UnlockWeapon(EWeapon.LightningRing);
    }
}

public class EvoWhip : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Whip)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Whip).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}

public class EvoMagicWand : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.MagicWand)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.MagicWand).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}

public class EvoKnife : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Knife)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Knife).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}

public class EvoLightningRing : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.LightningRing)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.LightningRing).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}

public class EvoKingBible : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.KingBible)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.KingBible).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}

public class EvoFireWand : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.FireWand)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.FireWand).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}

public class EvoGarlic : AchievementClass
{
    public override bool IsComplete()
    {
        return GameManager.instance.EquipManageSys.HasWeapon(EWeapon.Garlic)
                && GameManager.instance.EquipManageSys.GetWeapon(EWeapon.Garlic).IsEvoluction();
    }

    public override void EarnRewards()
    {
        UserInfo.instance.UpdateGold(500);
    }
}