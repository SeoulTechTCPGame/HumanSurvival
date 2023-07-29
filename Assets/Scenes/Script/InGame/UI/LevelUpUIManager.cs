using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class MiniLevels
{
    public Image[] MiniLevel;
}

public class LevelUpUIManager : MonoBehaviour
{
    public bool mbOnLevelUp;

    [SerializeField] MiniLevels[] mWeaponLevelsUI;
    [SerializeField] Image[] mOwnWeaponImages;
    [SerializeField] MiniLevels[] mAccessoryLevelsUI;
    [SerializeField] Image[] mOwnAccessoryImages;

    [SerializeField] GameObject mPlayer;
    [SerializeField] GameObject mPickUpUI;
    [SerializeField] GameObject mStatUI;
    [SerializeField] GameObject mItemUI;

    [SerializeField] TextMeshProUGUI mStatVarText;

    [SerializeField] GameObject[] mPickUpButtons;

    [SerializeField] Sprite[] mMiniLevelImages;

    [SerializeField] GameObject mFilter;

    [SerializeField] static List<string[]> mTypeScripts;

    private List<Tuple<int, int, int>> mPickUps;
    private int mRotSpeed = 720;
    private float mTime = 0;

    private void Start()
    {
        mbOnLevelUp = false;
        UnSetPickUpUI();
        mPickUpUI.SetActive(false);
        UnSetItemUI();
        mStatUI.SetActive(false);
        mItemUI.SetActive(false);
        mFilter.SetActive(false);

        // TODO: 아이템 설명들 추가하기
        ItemScriptProcessing();
    }
    private void Update()
    {
        if (mbOnLevelUp && mTime < 0.99f)
        {
            mPickUpUI.transform.Rotate(0, 0, mRotSpeed * Time.unscaledDeltaTime);

            mPickUpUI.transform.localScale = Vector3.one * (mTime);
            mStatUI.transform.localScale = Vector3.one * (mTime);
            mItemUI.transform.localScale = Vector3.one * (mTime);

            mTime += Time.fixedDeltaTime;
            if(mTime >= 0.99f)
            {
                mPickUpUI.transform.rotation = Quaternion.identity;
                mPickUpUI.transform.localScale = Vector3.one;
                mStatUI.transform.localScale = Vector3.one;
                mItemUI.transform.localScale = Vector3.one;
            }
        }
    }
    public void LoadLevelUpUI(float[] characterStats, List<Tuple<int, int, int>> pickUps, List<Weapon> weapons, List<Accessory> accessories)
    {
        mTime = 0;
        mbOnLevelUp = true;
        mPlayer.GetComponent<PlayerMovement>().enabled = false;
        mPickUps = pickUps;

        mPickUpUI.SetActive(true);
        SetPickUpUI(pickUps);
        mStatUI.SetActive(true);
        SetStatUI(characterStats);
        mItemUI.SetActive(true);
        SetItemUI(weapons, accessories);
        mFilter.SetActive(true);
    }
    public void UnloadLevelUpUI()
    {
        mbOnLevelUp = false;
        UnSetPickUpUI();
        mPickUpUI.SetActive(false);
        mStatUI.SetActive(false);
        UnSetItemUI();
        mItemUI.SetActive(false);
        mFilter.SetActive(false);
        mPlayer.GetComponent<PlayerMovement>().enabled = true;
    }
    public void SetPickUpUI(List<Tuple<int, int, int>> pickUps)
    {
        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickUpButtons[i].SetActive(true);

            mPickUpButtons[i].GetComponent<PickButton>().Image.sprite = GetSprite(pickUps[i].Item1, pickUps[i].Item2);

            mPickUpButtons[i].GetComponent<PickButton>().Texts[(int)Enums.EButton.Name].text = TransPickIndexToEnumString(pickUps[i].Item1, pickUps[i].Item2);

            mPickUpButtons[i].GetComponent<PickButton>().Texts[(int)Enums.EButton.Script].text = mTypeScripts[pickUps[i].Item1][pickUps[i].Item2];
            mPickUpButtons[i].GetComponent<PickButton>().Texts[(int)Enums.EButton.Property].text = pickUps[i].Item3 == 0 ? "New" : "";
        }
    }
    public void UnSetPickUpUI()
    {
        foreach (var pickUpButton in mPickUpButtons)
        {
            pickUpButton.SetActive(false);
        }
    }
    public void SetStatUI(float[] characterStats)
    {
        mStatVarText.text =
            (GameManager.instance.CharacterData.MaxHealth * characterStats[(int)Enums.EStat.MaxHealth]).ToString() + "\n" +
            (characterStats[(int)Enums.EStat.Recovery] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Recovery].ToString()) + "\n" +
            (characterStats[(int)Enums.EStat.Armor] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Armor].ToString()) + "\n" +
            (characterStats[(int)Enums.EStat.MoveSpeed] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.MoveSpeed] - 1) * 100)).ToString() + "%") + "\n\n" +
            (characterStats[(int)Enums.EStat.Might] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Might] - 1) * 100)).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.ProjectileSpeed] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.ProjectileSpeed] - 1) * 100)).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.Duration] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Duration] - 1) * 100)).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.Area] == 1 ? "-" : (characterStats[(int)Enums.EStat.Area] - 1 < 1 ? "+" + MathF.Round(((characterStats[(int)Enums.EStat.Area] - 1) * 100)).ToString() + "%" : "+" + ((characterStats[(int)Enums.EStat.Area] * 100).ToString() + "%"))) + "\n\n" + // 초기 공격 범위가 1이 아닌 4일 때 즉 400% 일 때 300%로 표기되는 것을 막기 위해서 if문 하나 더 사용
            (characterStats[(int)Enums.EStat.Cooldown] == 1 ? "-" : "-" + (MathF.Round(((1 - characterStats[(int)Enums.EStat.Cooldown]) * 1000)) * 0.1f).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.Amount] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Amount].ToString()) + "\n" +
            (characterStats[(int)Enums.EStat.Revival] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Revival].ToString()) + "\n" +
            (characterStats[(int)Enums.EStat.Magnet] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Magnet] - 1) * 100)).ToString() + "%") + "\n\n" +
            (characterStats[(int)Enums.EStat.Luck] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Luck] - 1) * 100)).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.Growth] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Growth] - 1) * 100)).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.Greed] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Greed] - 1) * 100)).ToString() + "%") + "\n" +
            (characterStats[(int)Enums.EStat.Curse] == 1 ? "-" : "+" + MathF.Round(((characterStats[(int)Enums.EStat.Curse] - 1) * 100)).ToString() + "%") + "\n\n" +
            (characterStats[(int)Enums.EStat.Reroll] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Reroll].ToString()) + "\n" +
            (characterStats[(int)Enums.EStat.Skip] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Skip].ToString()) + "\n" +
            (characterStats[(int)Enums.EStat.Banish] == 0 ? "-" : "+" + characterStats[(int)Enums.EStat.Banish].ToString());
    }
    public void SetItemUI(List<Weapon> weapons, List<Accessory> accessories)
    {
        SetWeaponUI(weapons);
        SetAccessoryUI(accessories);
    }
    public void UnSetItemUI()
    {
        foreach (var image in mOwnWeaponImages)
        {
            image.enabled = false;
        }
        foreach (var weaponslevel in mWeaponLevelsUI)
        {
            foreach (var weaponMiniLevel in weaponslevel.MiniLevel)
            {
                weaponMiniLevel.enabled = false;
            }
        }
        foreach (var image in mOwnAccessoryImages)
        {
            image.enabled = false;
        }
        foreach (var accessorieslevel in mAccessoryLevelsUI)
        {
            foreach (var accessoryMiniLevel in accessorieslevel.MiniLevel)
            {
                accessoryMiniLevel.enabled = false;
            }
        }
    }
    public void ClickPickButton()
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        int selectedIndex = clickedButton.GetComponent<PickButton>().Index;
        GameManager.instance.EquipManageSys.ApplyItem(mPickUps[selectedIndex]);
        UnloadLevelUpUI();
        GameObject.Find("GameManager").GetComponent<GameManager>().ResumeGame();
    }
    private static void ItemScriptProcessing()
    {
        mTypeScripts = new List<string[]>();
        mTypeScripts.Add(new string[Constants.MAX_WEAPON_NUMBER]
        {
            "Whip 설명",
            "MagicWand 설명",
            "Knife 설명",
            "Cross 설명",
            "KingBible 설명",
            "FireWand 설명",
            "Garlic 설명",
            "Peachone 설명",
            "EbonyWings 설명",
            "LightningRing 설명"
        });
        mTypeScripts.Add(new string[Constants.MAX_ACCESSORY_NUMBER]
        {
            "Spinach 설명",
            "Armor 설명",
            "HollowHeart 설명",
            "Pummarola 설명",
            "EmptyTome 설명",
            "Candelabrador 설명",
            "Bracer 설명",
            "Spellbinder 설명",
            "Duplicator 설명",
            "Wings 설명",
            "Attractorb 설명",
            "Clover 설명",
            "Crown 설명",
            "StoneMask 설명",
            "Skull 설명",
        });
        mTypeScripts.Add(new string[Constants.MAX_ETC_NUMBER]
        {
            "25골드를 추가합니다.",
            "체력을 30 회복합니다."
        });
    }
    private string TransPickIndexToEnumString(int type, int index)
    {
        switch (type)
        {
            case 0:
                return ((Enums.EWeapon)index).ToString();
            case 1:
                return ((Enums.EAccessory)index).ToString();
            default:
                return ((Enums.EEtc)index).ToString();
        }
    }
    private void SetWeaponUI(List<Weapon> weapons)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            mOwnWeaponImages[i].sprite = GetSprite(0, weapons[i].WeaponIndex);
            mOwnWeaponImages[i].enabled = true;

            int j = 0;
            for (; j < weapons[i].WeaponLevel; j++)
            {
                mWeaponLevelsUI[i].MiniLevel[j].sprite = mMiniLevelImages[1];
                mWeaponLevelsUI[i].MiniLevel[j].enabled = true;
            }
            for (; j < weapons[i].WeaponMaxLevel; j++)
            {
                mWeaponLevelsUI[i].MiniLevel[j].sprite = mMiniLevelImages[0];
                mWeaponLevelsUI[i].MiniLevel[j].enabled = true;
            }
        }
    }
    private Sprite GetSprite(int itemType, int index)
    {
        string resourceName;
        switch (itemType)
        {
            case 0:
                Enums.EWeapon[] enumValuesWeapon = (Enums.EWeapon[])System.Enum.GetValues(typeof(Enums.EWeapon));
                Enums.EWeapon weapon = enumValuesWeapon[index];
                resourceName = "Weapons/" + weapon.ToString();
                return Resources.Load<Sprite>(resourceName);
            case 1:
                Enums.EAccessory[] enumValuesAccessory = (Enums.EAccessory[])System.Enum.GetValues(typeof(Enums.EAccessory));
                Enums.EAccessory accessory = enumValuesAccessory[index];
                resourceName = "Accessory/" + accessory.ToString();
                return Resources.Load<Sprite>(resourceName);
            case 2:
                switch(index)
                {
                    case 0:
                        resourceName = "Item/Coin";
                        return Resources.Load<Sprite>(resourceName);
                    case 1:
                        resourceName = "Item/Recovery";
                        return Resources.Load<Sprite>(resourceName);
                }
                break;
        }

        return null;
    }
    private void SetAccessoryUI(List<Accessory> accessories)
    {
        for (int i = 0; i < accessories.Count; i++)
        {
            mOwnAccessoryImages[i].sprite = GetSprite(1, accessories[i].AccessoryIndex);
            mOwnAccessoryImages[i].enabled = true;

            int j = 0;
            for (; j < accessories[i].AccessoryLevel; j++)
            {
                mAccessoryLevelsUI[i].MiniLevel[j].sprite = mMiniLevelImages[1];
                mAccessoryLevelsUI[i].MiniLevel[j].enabled = true;
            }
            for (; j < accessories[i].AccessoryMaxLevel; j++)
            {
                mAccessoryLevelsUI[i].MiniLevel[j].sprite = mMiniLevelImages[0];
                mAccessoryLevelsUI[i].MiniLevel[j].enabled = true;
            }
        }
    }
}