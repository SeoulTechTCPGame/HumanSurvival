using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;
using static UnityEngine.Rendering.DebugUI.Table;

[System.Serializable]
public class MiniLevels
{
    public Image[] MiniLevel;
}
public class LevelUpUIManager : MonoBehaviour
{
    [SerializeField] MiniLevels[] WeaponLevelsUI;
    [SerializeField] Image[] OwnWeaponImages;
    [SerializeField] MiniLevels[] AccessoryLevelsUI;
    [SerializeField] Image[] OwnAccessoryImages;

    [SerializeField] GameObject Player;
    [SerializeField] GameObject PickUpUI;
    [SerializeField] GameObject StatUI;
    [SerializeField] GameObject ItemUI;

    [SerializeField] TextMeshProUGUI StatVarText;

    [SerializeField] GameObject[] mPickUpButtons;

    [SerializeField] Sprite[] WeaponImages;
    [SerializeField] Sprite[] AccessoryImages;
    [SerializeField] Sprite[] EtcImages;
    [SerializeField] Sprite[] MiniLevelImages;

    [SerializeField] GameObject Filter;

    [SerializeField] static List<string[]> TypeScripts;

    private List<Tuple<int, int, int>> mPickUps;
    private bool IsOnLevelUp;
    private int mRotSpeed = 60;
    private float mTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        IsOnLevelUp = false;
        UnSetPickUpUI();
        PickUpUI.SetActive(false);
        UnSetItemUI();
        StatUI.SetActive(false);
        ItemUI.SetActive(false);
        Filter.SetActive(false);

        // TODO: 아이템 설명들 추가하기
        ItemScriptProcessing();
    }
    private void Update()
    {
        if (IsOnLevelUp && mTime < 0.99f)
        {
            PickUpUI.transform.Rotate(0, 0, mRotSpeed * Time.unscaledDeltaTime);

            PickUpUI.transform.localScale = Vector3.one * (mTime);
            StatUI.transform.localScale = Vector3.one * (mTime);
            ItemUI.transform.localScale = Vector3.one * (mTime);

            mTime += 0.02f;
            if(mTime >= 0.99f)
                PickUpUI.transform.rotation = Quaternion.identity;
        }
    }

    private static void ItemScriptProcessing()
    {
        TypeScripts = new List<string[]>();
        TypeScripts.Add(new string[Constants.MaxWeaponNumber]
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
        TypeScripts.Add(new string[Constants.MaxAccessoryNumber]
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
        TypeScripts.Add(new string[Constants.MaxEtcNumber]
            {
                "25골드를 추가합니다.",
                "체력을 30 회복합니다."
            });
    }

    public void LoadLevelUpUI(float[] characterStats, List<Tuple<int, int, int>> pickUps, List<Weapon> weapons, List<Accessory> accessories)
    {
        mTime = 0;
        IsOnLevelUp = true;
        Player.GetComponent<PlayerMovement>().enabled = false;
        mPickUps = pickUps;

        PickUpUI.SetActive(true);
        SetPickUpUI(pickUps);
        StatUI.SetActive(true);
        SetStatUI(characterStats);
        ItemUI.SetActive(true);
        SetItemUI(weapons, accessories);
        Filter.SetActive(true);
    }
    public void UnloadLevelUpUI()
    {
        IsOnLevelUp = false;
        UnSetPickUpUI();
        PickUpUI.SetActive(false);
        StatUI.SetActive(false);
        UnSetItemUI();
        ItemUI.SetActive(false);
        Filter.SetActive(false);
        Player.GetComponent<PlayerMovement>().enabled = true;
    }
    public void SetPickUpUI(List<Tuple<int, int, int>> pickUps)
    {
        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickUpButtons[i].SetActive(true);

            mPickUpButtons[i].GetComponent<PickButton>().Image.sprite = getSprites(pickUps[i].Item1)[pickUps[i].Item2];

            mPickUpButtons[i].GetComponent<PickButton>().Texts[(int)Enums.Button.Name].text = transPickIndexToEnumString(pickUps[i].Item1, pickUps[i].Item2);

            mPickUpButtons[i].GetComponent<PickButton>().Texts[(int)Enums.Button.Script].text = TypeScripts[pickUps[i].Item1][pickUps[i].Item2];
            mPickUpButtons[i].GetComponent<PickButton>().Texts[(int)Enums.Button.Property].text = pickUps[i].Item3 == 0 ? "New" : "";
        }
    }
    private Sprite[] getSprites(int type)
    {
        switch (type)
        {
            case 0:
                return WeaponImages;
            case 1:
                return AccessoryImages;
            default:
                return EtcImages;
        }
    }
    private string transPickIndexToEnumString(int type, int index)
    {
        switch (type)
        {
            case 0:
                return ((Enums.Weapon)index).ToString();
            case 1:
                return ((Enums.Accessory)index).ToString();
            default:
                return ((Enums.Etc)index).ToString();
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
        StatVarText.text =
            (GameManager.instance.characterData.MaxHealth * characterStats[(int)Enums.Stat.MaxHealth]).ToString() + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Recovery] * 10) == 0 ? "-" : "+" + (characterStats[(int)Enums.Stat.Recovery]).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Armor]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.Stat.Armor])).ToString()) + "\n" +
            ((int)((characterStats[(int)Enums.Stat.MoveSpeed] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.MoveSpeed] - 1) * 100)).ToString() + "%") + "\n\n" +
            ((int)((characterStats[(int)Enums.Stat.Might] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Might] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.Stat.ProjectileSpeed] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.ProjectileSpeed] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.Stat.Duration] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Duration] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.Stat.Area] - 1) * 100) == 0 ? "-" : ((int)((characterStats[(int)Enums.Stat.Area] - 1)) == 0 ? "+" + ((int)((characterStats[(int)Enums.Stat.Area] - 1) * 100)).ToString() + "%" : "+" + ((int)(characterStats[(int)Enums.Stat.Area] * 100)).ToString() + "%")) + "\n\n" + // 초기 공격 범위가 1이 아닌 4일 때 즉 400% 일 때 300%로 표기되는 것을 막기 위해서 if문 하나 더 사용
            ((int)((characterStats[(int)Enums.Stat.Cooldown] - 1) * 100) == 0 ? "-" : ((int)((characterStats[(int)Enums.Stat.Cooldown] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Amount]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.Stat.Amount])).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Revival]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.Stat.Revival])).ToString()) + "\n" +
            ((int)((characterStats[(int)Enums.Stat.Magnet] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Magnet] - 1) * 100)).ToString() + "%") + "\n\n" +
            ((int)((characterStats[(int)Enums.Stat.Luck] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Luck] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.Stat.Growth] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Growth] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.Stat.Greed] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Greed] - 1) * 100)).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.Stat.Curse] - 1) * 100) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.Stat.Curse] - 1) * 100)).ToString() + "%") + "\n\n" +
            ((int)(characterStats[(int)Enums.Stat.Reroll]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.Stat.Reroll])).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Skip]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.Stat.Skip])).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Banish]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.Stat.Banish])).ToString());
    }
    public void SetItemUI(List<Weapon> weapons, List<Accessory> accessories)
    {
        SetWeaponUI(weapons);
        SetAccessoryUI(accessories);
    }
    private void SetWeaponUI(List<Weapon> weapons)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            OwnWeaponImages[i].sprite = WeaponImages[weapons[i].WeaponIndex];
            OwnWeaponImages[i].enabled = true;

            int j = 0;
            for (; j < weapons[i].WeaponLevel; j++)
            {
                WeaponLevelsUI[i].MiniLevel[j].sprite = MiniLevelImages[1];
                WeaponLevelsUI[i].MiniLevel[j].enabled = true;
            }
            for (; j < weapons[i].WeaponMaxLevel; j++)
            {
                WeaponLevelsUI[i].MiniLevel[j].sprite = MiniLevelImages[0];
                WeaponLevelsUI[i].MiniLevel[j].enabled = true;
            }
        }
    }
    private void SetAccessoryUI(List<Accessory> accessories)
    {
        for (int i = 0; i < accessories.Count; i++)
        {
            OwnAccessoryImages[i].sprite = AccessoryImages[accessories[i].AccessoryIndex];
            OwnAccessoryImages[i].enabled = true;

            int j = 0;
            for (; j < accessories[i].AccessoryLevel; j++)
            {
                AccessoryLevelsUI[i].MiniLevel[j].sprite = MiniLevelImages[1];
                AccessoryLevelsUI[i].MiniLevel[j].enabled = true;
            }
            for (; j < accessories[i].AccessoryMaxLevel; j++)
            {
                AccessoryLevelsUI[i].MiniLevel[j].sprite = MiniLevelImages[0];
                AccessoryLevelsUI[i].MiniLevel[j].enabled = true;
            }
        }
    }
    public void UnSetItemUI()
    {
        foreach (var image in OwnWeaponImages)
        {
            image.enabled = false;
        }
        foreach (var weaponslevel in WeaponLevelsUI)
        {
            foreach (var weaponMiniLevel in weaponslevel.MiniLevel)
            {
                weaponMiniLevel.enabled = false;
            }
        }
        foreach (var image in OwnAccessoryImages)
        {
            image.enabled = false;
        }
        foreach (var accessorieslevel in AccessoryLevelsUI)
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
        int selectedIndex = clickedButton.GetComponent<PickButton>().index;
        GameManager.instance.equipManageSys.ApplyItem(mPickUps[selectedIndex]);
        UnloadLevelUpUI();
        GameObject.Find("GameManager").GetComponent<GameManager>().ResumeGame();
    }
}
