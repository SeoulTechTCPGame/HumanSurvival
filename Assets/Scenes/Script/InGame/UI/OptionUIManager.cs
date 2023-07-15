using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionUIManager : MonoBehaviour
{
    [SerializeField] MiniLevels[] mWeaponLevelsUI;
    [SerializeField] Image[] mOwnWeaponImages;
    [SerializeField] MiniLevels[] mAccessoryLevelsUI;
    [SerializeField] Image[] mOwnAccessoryImages;

    [SerializeField] GameObject mItemUI;
    [SerializeField] GameObject mStatUI;
    [SerializeField] GameObject mOptionPageUI;
    [SerializeField] GameObject mOptionButtonUI;
    [SerializeField] GameObject mBackButtonUI;
    [SerializeField] GameObject mQuitButtonUI;
    [SerializeField] GameObject mPlayer;

    [SerializeField] TextMeshProUGUI mStatVarText;

    [SerializeField] Sprite[] mWeaponImages;
    [SerializeField] Sprite[] mAccessoryImages;
    [SerializeField] Sprite[] mEtcImages;
    [SerializeField] Sprite[] mMiniLevelImages;

    private bool mbPauseGame = false;
    private SoundManager mSoundManager;

    private void Awake()
    {
        mItemUI.SetActive(false);
        mStatUI.SetActive(false);
        mOptionPageUI.SetActive(false);
        mOptionButtonUI.SetActive(false);
        mBackButtonUI.SetActive(false);
        mQuitButtonUI.SetActive(false);
        UnSetItemUI();
    }
    private void Start()
    {
        // 저장된 볼륨 값 로드
        mSoundManager = SoundManager.instance;

        // defaultPanel의 자식 요소들을 가져와서 슬라이더를 초기화
        Slider[] sliders = mOptionPageUI.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            switch (slider.name)
            {
                case "MusicControlBar":
                    slider.value = mSoundManager.BgmVolume;
                    slider.onValueChanged.AddListener(OnBgmVolumeChanged);
                    break;
                case "SoundControlBar":
                    slider.value = mSoundManager.SoundEffectVolume;
                    slider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
                    break;
            }
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!mbPauseGame)
            {
                var weapons = GameManager.instance.EquipManageSys.Weapons;
                var accessories = GameManager.instance.EquipManageSys.Accessories;
                var characterStats =  GameManager.instance.CharacterStats;
                Time.timeScale = 0f;
                mItemUI.SetActive(true);
                mStatUI.SetActive(true);
                mOptionButtonUI.SetActive(true);
                mBackButtonUI.SetActive(true);
                SetItemUI(weapons, accessories);
                SetStatUI(characterStats);
                mPlayer.GetComponent<PlayerMovement>().enabled = false;
                mbPauseGame = true;
            }
            else
            {
                mItemUI.SetActive(false);
                mStatUI.SetActive(false);
                mOptionPageUI.SetActive(false);
                mOptionButtonUI.SetActive(false);
                mBackButtonUI.SetActive(false);
                mQuitButtonUI.SetActive(false);
                Time.timeScale = 1f;
                mPlayer.GetComponent<PlayerMovement>().enabled = true;
                mbPauseGame = false;
            }
            
        }
    }
    public void Resume()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {  
            mItemUI.SetActive(false);
            mStatUI.SetActive(false);
            mOptionPageUI.SetActive(false);
            mOptionButtonUI.SetActive(false);
            mBackButtonUI.SetActive(false);
            mQuitButtonUI.SetActive(false);
            Time.timeScale = 1f;
            mPlayer.GetComponent<PlayerMovement>().enabled = true;
        }
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
    public void SetStatUI(float[] characterStats)
    {
        mStatVarText.text =
            (GameManager.instance.CharacterData.MaxHealth * characterStats[(int)Enums.EStat.MaxHealth]).ToString() + "\n" +
            ((int)(characterStats[(int)Enums.EStat.Recovery] * 10) == 0 ? "-" : "+" +(characterStats[(int)Enums.EStat.Recovery]).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.EStat.Armor]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.EStat.Armor])).ToString()) + "\n" +
            ((int)((characterStats[(int)Enums.EStat.MoveSpeed] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.MoveSpeed] - 1) * 100 )).ToString() + "%") + "\n\n" +
            ((int)((characterStats[(int)Enums.EStat.Might] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Might] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.EStat.ProjectileSpeed] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.ProjectileSpeed] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.EStat.Duration] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Duration] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.EStat.Area] - 1) * 100 ) == 0 ? "-" : ((int)((characterStats[(int)Enums.EStat.Area] - 1)) == 0 ? "+" + ((int)((characterStats[(int)Enums.EStat.Area] - 1) * 100)).ToString() + "%" : "+" + ((int)(characterStats[(int)Enums.EStat.Area] * 100)).ToString() + "%")) + "\n\n" + // 초기 공격 범위가 1이 아닌 4일 때 즉 400% 일 때 300%로 표기되는 것을 막기 위해서 if문 하나 더 사용
            ((int)((characterStats[(int)Enums.EStat.Cooldown] - 1) * 100 ) == 0 ? "-" : ((int)((characterStats[(int)Enums.EStat.Cooldown] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)(characterStats[(int)Enums.EStat.Amount]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.EStat.Amount])).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.EStat.Revival]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.EStat.Revival])).ToString()) + "\n" +
            ((int)((characterStats[(int)Enums.EStat.Magnet] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Magnet] - 1) * 100 )).ToString() + "%") + "\n\n" +
            ((int)((characterStats[(int)Enums.EStat.Luck] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Luck] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.EStat.Growth] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Growth] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.EStat.Greed] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Greed] - 1) * 100 )).ToString() + "%") + "\n" +
            ((int)((characterStats[(int)Enums.EStat.Curse] - 1) * 100 ) == 0 ? "-" : "+" + ((int)((characterStats[(int)Enums.EStat.Curse] - 1) * 100 )).ToString() + "%") + "\n\n" +
            ((int)(characterStats[(int)Enums.EStat.Reroll]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.EStat.Reroll])).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.EStat.Skip]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.EStat.Skip])).ToString()) + "\n" +
            ((int)(characterStats[(int)Enums.EStat.Banish]) == 0 ? "-" : "+" + ((int)(characterStats[(int)Enums.EStat.Banish])).ToString());
    }
    public void SetItemUI(List<Weapon> weapons, List<Accessory> accessories)
    {
        SetWeaponUI(weapons);
        SetAccessoryUI(accessories);
    }
    private void OnBgmVolumeChanged(float value)
    {
        mSoundManager.BgmVolume = value;
        SoundManager.instance.AudioSource.volume = value;
    }
    private void OnSoundEffectVolumeChanged(float value)
    {
        mSoundManager.SoundEffectVolume = value;
    }
    private void SetWeaponUI(List<Weapon> weapons)
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            mOwnWeaponImages[i].sprite = mWeaponImages[weapons[i].WeaponIndex];
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
    private void SetAccessoryUI(List<Accessory> accessories)
    {
        for (int i = 0; i < accessories.Count; i++)
        {
            mOwnAccessoryImages[i].sprite = mAccessoryImages[accessories[i].AccessoryIndex];
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