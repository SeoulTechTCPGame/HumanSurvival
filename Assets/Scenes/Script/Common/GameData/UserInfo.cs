using Enums;
using System;
using System.Linq;
using UnityEngine;

public class UserInfo : MonoBehaviour
{
    public static UserInfo instance;
    public UserData UserDataSet;

    private const int JUMP_ACCESSORY = 58;
    private const int JUMP_STAGE = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetDefaultUnlockSettings()
    {
        SetDefaultStageSetting();
        SetDefaultCharacterSetting();
        SetDefaultWeaponSetting();
        SetDefaultAccessorySetting();
    }
        public void UpdateAccumulatedTime(float time)
    {
        UserDataSet.AccumulatedTime += time;
        UserDataManager.instance.SaveData();
    }
    public void UpdateAccumulatedKill(int kill)
    {
        UserDataSet.AccKill += kill;
    }   
    public void UpdateGold(int gold)
    {
        UserDataSet.Gold += gold;
        UserDataManager.instance.SaveData();
    }
    public void ConsumeGold(int gold)
    {
        UserDataSet.Gold += gold;
        UserDataSet.ConsumedGold -= gold;
        UserDataManager.instance.SaveData();
    }
    public void RefundGold()
    {
        UserDataSet.ConsumedGold = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdateCollection(int collectionIndex)
    {
        UserDataSet.BCollection[collectionIndex] = true;
        UserDataManager.instance.SaveData();
    }
    public void UpdateAchievement(int achievementIndexes)
    {
        UserDataSet.BAchievements[achievementIndexes] = true;
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpLevel(int powerUpIndex)
    {
        UserDataSet.PowerUpLevel[powerUpIndex]++;
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpLevel(int powerUpIndex)
    {
        UserDataSet.PowerUpLevel[powerUpIndex] = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpStat(int powerUpIndex, float powerUpStat)
    {
        UserDataSet.PowerUpStat[powerUpIndex] += powerUpStat;
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpStat(int powerUpIndex)
    {
        UserDataSet.PowerUpStat[powerUpIndex] = 0;
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpCash(int powerUpIndex)
    {
        UserDataSet.NowPowerUpCash[powerUpIndex] = (int)(UserDataSet.PowerUpCash[powerUpIndex] * (1 + UserDataSet.PowerUpLevel[powerUpIndex]) + 20 * Math.Pow(1.1, UserDataSet.PowerUpLevel.Sum() - 1));
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpCash(int powerUpIndex)
    {
        UserDataSet.NowPowerUpCash[powerUpIndex] = UserDataSet.PowerUpCash[powerUpIndex];
        UserDataManager.instance.SaveData();
    }
    public void UpdatePowerUpActive(int powerUpIndex, bool btemp)
    {
        UserDataSet.BPowerUpActive[powerUpIndex] = btemp;
        UserDataManager.instance.SaveData();
    }
    public void RefundPowerUpActive(int powerUpIndex)
    {
        UserDataSet.BPowerUpActive[powerUpIndex] = true;
        UserDataManager.instance.SaveData();
    }
    public void UnlockStage(EStage stage)
    {
        UserDataSet.BUnlockStages[(int)stage] = true;
    }
    public void UnlockCharacter(ECharacterType characterType)
    {
        UserDataSet.BUnlockCharacters[(int)characterType] = true;
    }
    public void UnlockWeapon(EWeapon weapon)
    {
        UserDataSet.BUnlockWeapons[(int)weapon] = true;
    }
    public void UnlockAccessory(EAccessory accessory)
    {
        UserDataSet.BUnlockAccessories[(int)accessory] = true;
    }
    public bool IsStageUnlocked(EStage stage) 
    {
        return UserDataSet.BUnlockStages[(int)stage];
    }
    public bool IsCharacterUnlocked(ECharacterType characterType) 
    {
        return UserDataSet.BUnlockCharacters[(int)characterType];
    }
    public bool IsWeaponUnlocked(EWeapon weapon)
    {
        return UserDataSet.BUnlockWeapons[(int)weapon];
    }
    public bool IsAccessoryUnlocked(EAccessory accessory)
    {
        return UserDataSet.BUnlockAccessories[(int)accessory];
    }
    public void UpdateOption()
    {
        // TODO: Option기능 완성되면 추가
        UserDataManager.instance.SaveData();
    }
    private void SetDefaultStageSetting()
    {
        UserDataSet.BUnlockStages[(int)EStage.MadForest] = true;
        UserDataSet.BUnlockStages[(int)EStage.InlaidLibrary] = false;
    }
    private void SetDefaultCharacterSetting()
    {
        UserDataSet.BUnlockCharacters[(int)ECharacterType.Rogue] = true;
        UserDataSet.BUnlockCharacters[(int)ECharacterType.StormMage] = true;
        UserDataSet.BUnlockCharacters[(int)ECharacterType.Barbarian] = true;
        UserDataSet.BUnlockCharacters[(int)ECharacterType.FireMage] = false;
        UserDataSet.BUnlockCharacters[(int)ECharacterType.Necromancer] = false;
        UserDataSet.BUnlockCharacters[(int)ECharacterType.Warlock] = false;
        UserDataSet.BUnlockCharacters[(int)ECharacterType.Alchemist] = false;
    }
    private void SetDefaultWeaponSetting()
    {
        UserDataSet.BUnlockWeapons[(int)EWeapon.Whip] = true;
        UserDataSet.BUnlockWeapons[(int)EWeapon.MagicWand] = false;
        UserDataSet.BUnlockWeapons[(int)EWeapon.Knife] = true;
        UserDataSet.BUnlockWeapons[(int)EWeapon.Cross] = true;
        UserDataSet.BUnlockWeapons[(int)EWeapon.KingBible] = true;
        UserDataSet.BUnlockWeapons[(int)EWeapon.FireWand] = false;
        UserDataSet.BUnlockWeapons[(int)EWeapon.Garlic] = false;
        UserDataSet.BUnlockWeapons[(int)EWeapon.Peachone] = false;
        UserDataSet.BUnlockWeapons[(int)EWeapon.EbonyWings] = false;
        UserDataSet.BUnlockWeapons[(int)EWeapon.LightningRing] = false;
    }
    private void SetDefaultAccessorySetting()
    {
        UserDataSet.BUnlockAccessories[(int)EAccessory.Spinach] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Armor] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.HollowHeart] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Pummarola] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.EmptyTome] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Candelabrador] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Bracer] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Spellbinder] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Duplicator] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Wings] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Attractorb] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Clover] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Crown] = false;
        UserDataSet.BUnlockAccessories[(int)EAccessory.Skull] = true;
        UserDataSet.BUnlockAccessories[(int)EAccessory.StoneMask] = true;
    }
}