using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionUIManager : MonoBehaviour
{
    [SerializeField] MiniLevels[] WeaponLevelsUI;
    [SerializeField] Image[] OwnWeaponImages;
    [SerializeField] MiniLevels[] AccessoryLevelsUI;
    [SerializeField] Image[] OwnAccessoryImages;

    [SerializeField] GameObject ItemUI;
    [SerializeField] GameObject StatUI;
    [SerializeField] GameObject OptionPage1UI;
    [SerializeField] GameObject OptionPage2UI;
    [SerializeField] GameObject OptionButtonUI;
    [SerializeField] GameObject BackButtonUI;
    [SerializeField] GameObject QuitButtonUI;
    [SerializeField] GameObject player;

    [SerializeField] TextMeshProUGUI StatVarText;

    [SerializeField] Sprite[] WeaponImages;
    [SerializeField] Sprite[] AccessoryImages;
    [SerializeField] Sprite[] EtcImages;
    [SerializeField] Sprite[] MiniLevelImages;

    // Start is called before the first frame update
    void Start()
    {
        ItemUI.SetActive(false);
        StatUI.SetActive(false);
        OptionPage1UI.SetActive(false);
        OptionPage2UI.SetActive(false);
        OptionButtonUI.SetActive(false);
        BackButtonUI.SetActive(false);
        QuitButtonUI.SetActive(false);
        UnSetItemUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            var weapons = GameManager.instance.equipManageSys.Weapons;
            var accessories = GameManager.instance.equipManageSys.Accessories;
            var characterStats =  GameManager.instance.CharacterStats;
            Time.timeScale = 0f;
            ItemUI.SetActive(true);
            StatUI.SetActive(true);
            OptionButtonUI.SetActive(true);
            BackButtonUI.SetActive(true);
            SetItemUI(weapons, accessories);
            SetStatUI(characterStats);
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        
    }

    public void Resume()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {  
            ItemUI.SetActive(false);
            StatUI.SetActive(false);
            OptionPage1UI.SetActive(false);
            OptionPage2UI.SetActive(false);
            OptionButtonUI.SetActive(false);
            BackButtonUI.SetActive(false);
            QuitButtonUI.SetActive(false);
            Time.timeScale = 1f;
            player.GetComponent<PlayerMovement>().enabled = true;
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

    public void SetStatUI(float[] characterStats)
    {
        StatVarText.text =
            ((int)(characterStats[(int)Enums.Stat.MaxHealth] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.MaxHealth] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Recovery] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Recovery] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Armor] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Armor] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.MoveSpeed] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.MoveSpeed] * 100 )).ToString()) + "\n\n" +
            ((int)(characterStats[(int)Enums.Stat.Might] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Might] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.ProjectileSpeed] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.ProjectileSpeed] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Duration] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Duration] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Area] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Area] * 100 )).ToString()) + "\n\n" +
            ((int)(characterStats[(int)Enums.Stat.Cooldown] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Cooldown] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Amount] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Amount] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Revival] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Revival] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Magnet] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Magnet] * 100 )).ToString()) + "\n\n" +
            ((int)(characterStats[(int)Enums.Stat.Luck] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Luck] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Growth] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Growth] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Greed] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Greed] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Curse] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Curse] * 100 )).ToString()) + "\n\n" +
            ((int)(characterStats[(int)Enums.Stat.Reroll] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Reroll] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Skip] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Skip] * 100 )).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.Stat.Banish] * 100 ) == 0 ? "-" : ((int)(characterStats[(int)Enums.Stat.Banish] * 100 )).ToString());
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
}
