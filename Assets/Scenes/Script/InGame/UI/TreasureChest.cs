using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rito;
using static UnityEditor.Progress;
using static UnityEngine.TouchScreenKeyboard;
using UnityEngine.UI;

public class TreasureChest : MonoBehaviour
{
    // 임시, 다른 곳에서 가져올 예정
    [SerializeField] Sprite[] mWeaponImages;
    [SerializeField] Sprite[] mAccessoryImages;
    [SerializeField] Sprite[] mEtcImages;
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

    private bool mbIsOn;
    private bool mbIsOnUiEffect;
    private bool mbIsOnPickUpEffect;
    private int mRotSpeed;
    private float mUiEffectTime;
    private float mPickUpEffectTime;
    private float mLaunchForce = 5f;    // 동전이 솟구칠 힘
    private float mSpinForce = 10f;     // 동전의 회전력
    private static int[] mChestRarity;
    private List<Tuple<int, int, int>> mPickUps;

    static TreasureChest()
    {
        mChestRarity = new int[3] { 100, 10, 3 }; // 동, 은, 금 상자들의 드랍 확률
    }
    private void Start()
    {
        mbIsOn = false;
        mbIsOnPickUpEffect = false;
        mbIsOnUiEffect = false;
        mRotSpeed = 180;
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
        
        // 픽업 이펙트 진행

        ShowItems();
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
        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickLights[i].SetActive(true);
        }
        mbIsOnPickUpEffect = true;
    }
    private void PickUpEffect()
    {
        SpawnCoin();
        mPickUpEffectTime += Time.fixedDeltaTime;
        if (mPickUpEffectTime >= 40.0f)
        {
            ChestCloseUI();
            mbIsOnPickUpEffect = false;
        }
    }
    private void SpawnCoin()
    {
        GameObject newCoin = Instantiate(mCoin, mChest.transform.position, Quaternion.identity);
        Rigidbody2D rb = newCoin.AddComponent<Rigidbody2D>();
        float randomXForce = UnityEngine.Random.Range(-1f, 1f);

        rb.AddForce(new Vector2(0, mLaunchForce), ForceMode2D.Impulse);
        rb.AddTorque(mSpinForce, ForceMode2D.Impulse);
        rb.AddForce(new Vector2(randomXForce, 0), ForceMode2D.Impulse);
    }
    private void ShowItems()
    {
        for (int i = 0; i < mPickUps.Count; i++)
        {
            mPickedItems[i].GetComponent<Image>().sprite = GetSprites(mPickUps[i].Item1)[mPickUps[i].Item2];
            mPickedItems[i].SetActive(true);
            mItemEffects[i].SetActive(true);
        }
    }
    private void DisableAllObject()
    {
        mBG.SetActive(false);
        mChest.SetActive(false);
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

        int pickedIndex = chestPicker.GetRandomPick();  // 0 ~ 2 반환
        pickedIndex = (pickedIndex << 1) | 1;           // 1, 3, 5로 변환
        return pickedIndex;
    }
    private Sprite[] GetSprites(int type)
    {
        switch (type)
        {
            case 0:
                return mWeaponImages;
            case 1:
                return mAccessoryImages;
            default:
                return mEtcImages;
        }
    }
}