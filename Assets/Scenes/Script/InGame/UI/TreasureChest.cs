using System;
using System.Collections.Generic;
using UnityEngine;
using Rito;
using UnityEngine.UI;
using TMPro;

public class TreasureChest : MonoBehaviour
{
    // 임시, 다른 곳에서 가져올 예정
    [SerializeField] GameObject mBG;
    [SerializeField] GameObject mChest;
    [SerializeField] GameObject[] mPickLights;
    [SerializeField] GameObject[] mPickedItems;
    [SerializeField] GameObject[] mItemEffects;
    [SerializeField] GameObject mCoinText;
    [SerializeField] GameObject mCoin;
    [SerializeField] GameObject mTreasureText;
    [SerializeField] GameObject mButtonOpen;
    [SerializeField] GameObject mButtonClose;
    [SerializeField] GameObject mPickLightMask;
    [SerializeField] GameObject mFlyCoin;
    [SerializeField] GameObject mFlyItem;

    private bool mbIsOn;
    private bool mbIsOnUiEffect;
    private bool mbIsOnPickUpEffect;
    private int mRotSpeed;
    private int mPickedIndex;
    private int mGold;
    private int mCnt = 0;
    private float mEffectEndTime = 10f;
    private float mUiEffectTime;
    private float mPickUpEffectTime;
    private static int[] mChestRarity;
    private List<Tuple<int, int, int>> mPickUps;
    private List<Vector3> mEndPoints;
    private static Color[][] mLightColors = { new Color[]{ new Color(0.12f, 0.1f, 1f, 0.56f) },
        new Color[]{ new Color(0.5f, 0, 0.9f, 0.56f), new Color(1, 0, 0.86f, 0.56f), new Color(1, 0, 0.86f, 0.56f) },
        new Color[]{ new Color(1f, 0.2f, 0.2f, 0.56f), new Color(1f, 0.48f, 0, 0.56f), new Color(1f, 0.48f, 0, 0.56f), new Color(0.94f, 1f, 0.2f, 0.56f), new Color(0.94f, 1f, 0.2f, 0.56f) } };

    static TreasureChest()
    {
        mChestRarity = new int[3] { 100, 10, 3 }; // 동, 은, 금 상자들의 드랍 확률
    }
    private void Start()
    {
        mbIsOn = false;
        mbIsOnPickUpEffect = false;
        mbIsOnUiEffect = false;
        mRotSpeed = 720;
        mUiEffectTime = 0;
        mPickUpEffectTime = 0;
        DisableAllObject();
    }
    private void Update()
    {
        if (mbIsOn)
        {
            if (mbIsOnUiEffect)
            {
                UiEffect();
            }
            else if (mbIsOnPickUpEffect)
            {
                PickUpEffect();
            }
        }
    }
    public void LoadChestUI()
    {
        GameManager.instance.PauseGame();
        mUiEffectTime = 0;
        mPickUpEffectTime = 0;
        mbIsOn = true;
        mbIsOnUiEffect = true;
        mPickUps = GameManager.instance.RandomPickUpSystem.RandomPickUp(GetChoice());
        SetGold();
        SetEndPoints();
        ChestOpenUI();
        GameManager.instance.Player.enabled = false;
    }
    public void UnloadChestUI()
    {
        mbIsOn = false;
        DisableAllObject();

        GameManager.instance.ResumeGame();
        GameManager.instance.Player.enabled = true;
    }
    public void ChestOpenUI()
    {
        mBG.SetActive(true);
        mChest.SetActive(true);
        mButtonOpen.SetActive(true);
        mTreasureText.SetActive(true);
    }
    public void ChestPickUI()
    {
        mButtonOpen.SetActive(false);
        mTreasureText.SetActive(false);
        mCoin.SetActive(true);
        mCoinText.SetActive(true);
        PickEffect();
    }
    public void ChestCloseUI()
    {
        mButtonClose.SetActive(true);
        for (int i = 0; i < mPickUps.Count; i++)
        {
            GameManager.instance.EquipManageSys.ApplyItem(mPickUps[i]);
            mPickLights[i].SetActive(false);
        }
    }
    public void ClickOpenButton()
    {
        ChestPickUI();
    }
    public void ClickCloseButton()
    {
        UnloadChestUI();
    }
    private void UiEffect()
    {
        transform.Rotate(0, 0, mRotSpeed * Time.unscaledDeltaTime);

        transform.localScale = Vector3.one * (mUiEffectTime);

        mUiEffectTime += Time.fixedDeltaTime;
        if (mUiEffectTime >= 1f)
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            mbIsOnUiEffect = false;
        }
    }
    private void PickEffect()
    {
        SetLightColor();
        mPickLightMask.SetActive(true);
        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickLights[i].SetActive(true);
        }
        mbIsOnPickUpEffect = true;
    }
    private void PickUpEffect()
    {
        mCnt++;
        if (mCnt > 4)
        {
            mCnt = 0;
            SpawnCoin();
            SpawnCoin();
            SpawnCoin();
            SpawnCoin();
            SpawnCoin();
            SpawnItem();
        }
        mPickUpEffectTime += Time.fixedDeltaTime;
        mCoinText.GetComponent<TextMeshProUGUI>().text = ((int)(mPickUpEffectTime * mGold / mEffectEndTime)).ToString();
        if (mPickUpEffectTime >= mEffectEndTime)
        {
            ChestCloseUI();
            mbIsOnPickUpEffect = false;
            mCoinText.GetComponent<TextMeshProUGUI>().text = mGold.ToString();
            ShowItems();
        }
    }
    private void SetLightColor()
    {

        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickLights[i].GetComponent<Image>().color = mLightColors[mPickedIndex][i];
        }
    }
    private void SpawnCoin()
    {
        GameObject newCoin = Instantiate(mFlyCoin, mChest.transform.position, Quaternion.identity, transform);
    }
    private void SpawnItem()
    {
        for (int i = 0; i < mPickUps.Count; i++)
        {
            GameObject newItem = Instantiate(mFlyItem, mChest.transform.position, Quaternion.identity, mPickLightMask.transform);
            newItem.GetComponent<FlyItem>().EndPoint = mEndPoints[i];
            newItem.GetComponent<FlyItem>().SetImage(GetRandomItemImg());
        }
    }
    private void SetEndPoints()
    {
        mEndPoints = new List<Vector3>();
        RectTransform rt = mPickLightMask.GetComponent<RectTransform>();
        mEndPoints.Add(mChest.GetComponent<Transform>().position + new Vector3(0, rt.rect.height, 0));                           // 중앙 상단
        mEndPoints.Add(mChest.GetComponent<Transform>().position + new Vector3(-rt.rect.width / 2f, rt.rect.height + 150, 0));   // 좌측 상단1
        mEndPoints.Add(mChest.GetComponent<Transform>().position + new Vector3(rt.rect.width / 2f, rt.rect.height + 150, 0));    // 우측 상단1
        mEndPoints.Add(mChest.GetComponent<Transform>().position + new Vector3(-rt.rect.width / 2f, rt.rect.height / 2f, 0));    // 좌측 상단2
        mEndPoints.Add(mChest.GetComponent<Transform>().position + new Vector3(rt.rect.width / 2f, rt.rect.height / 2f, 0));     // 우측 상단2
    }
    private void ShowItems()
    {
        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickedItems[i].GetComponent<Image>().sprite = GetSprite(mPickUps[i].Item1, mPickUps[i].Item2);
            mPickedItems[i].SetActive(true);
            mItemEffects[i].SetActive(true);
        }
    }
    private Sprite GetRandomItemImg()
    {
        int ItemType = UnityEngine.Random.Range(0, 2); // 0: Weapon, 1: Accessory
        int[] index = { Constants.MAX_WEAPON_NUMBER, Constants.MAX_ACCESSORY_NUMBER, 2 };
        int ItemIndex = UnityEngine.Random.Range(0, index[ItemType]);
        return GetSprite(ItemType, ItemIndex);
    }
    private void DisableAllObject()
    {
        mBG.SetActive(false);
        mChest.SetActive(false);
        mPickLightMask.SetActive(false);
        foreach (var light in mPickLights)
            light.SetActive(false);
        foreach (var item in mPickedItems)
            item.SetActive(false);
        foreach (var item in mItemEffects)
            item.SetActive(false);
        mCoin.SetActive(false);
        mCoinText.SetActive(false);
        mTreasureText.SetActive(false);
        mButtonOpen.SetActive(false);
        mButtonClose.SetActive(false);
    }
    private int GetChoice() // 1, 3, 5 중 하나를 반환
    {
        var greed = GameManager.instance.CharacterStats[(int)Enums.EStat.Greed];
        WeightedRandomPicker<int> chestPicker = new WeightedRandomPicker<int>();
        for (int i = 0; i < mChestRarity.Length; i++)
        {
            chestPicker.Add(i, (mChestRarity[i] + greed) / (double)mChestRarity[i]);
        }

        mPickedIndex = chestPicker.GetRandomPick();  // 0 ~ 2 반환
        return (mPickedIndex << 1) | 1;           // 1, 3, 5로 변환
    }
    private void SetGold()
    {
        mGold = (mPickedIndex + 1) * 40 + UnityEngine.Random.Range(20 * mPickedIndex, 50 * mPickedIndex);
        GameManager.instance.GetCoin(mGold);
        mGold = (int)Math.Ceiling(mGold * (1 + GameManager.instance.CharacterStats[(int)Enums.EStat.Greed]));
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
                switch (index)
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
}