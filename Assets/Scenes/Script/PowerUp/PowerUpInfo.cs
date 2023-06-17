using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
class PowerUpData
{
    public string Explain;
    public int AccessoryLevel;
}

[System.Serializable]
class PowerUpInfoData
{
    public PowerUpData[] PowerUp;
}

public class PowerUpInfo : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] int mAccessoryIndex;
    [SerializeField] GameObject mContext;
    [SerializeField] GameObject mExplainBG;

    private Image mThisAccessoryIamge;
    private TMP_Text mThisAccessoryName;
    private Image mAccessoryImage;
    private TMP_Text mAccessoryName;
    private TMP_Text mAccessoryExplain;
    private Button mChargeButton;
    private GameObject mChargeObject;
    private GameObject mActiveObject;
    public TMP_Text AccessoryCash;
    public List<Toggle> AccessoryToggle;

    private PowerUpInfoData mInfoData;
    private string mExplain;
    public int AccessoryMaxLevel;

    void Awake() 
    {
        Transform accessory = mContext.transform.GetChild(mAccessoryIndex - 1);
        mThisAccessoryName = accessory.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        mThisAccessoryIamge = accessory.transform.GetChild(1).GetComponent<Image>();
        mAccessoryImage = mExplainBG.transform.Find("AccessoryImage").GetComponent<Image>();
        mAccessoryName = mExplainBG.transform.Find("AccessoryName").GetComponent<TextMeshProUGUI>();
        mAccessoryExplain = mExplainBG.transform.Find("AccessoryExplain").GetComponent<TextMeshProUGUI>();
        mChargeObject = mExplainBG.transform.Find("ChargeObject").gameObject;
        mActiveObject = mExplainBG.transform.Find("ActiveObject").gameObject;
        mChargeButton = mChargeObject.transform.Find("ChargeButton").GetComponent<Button>();
        AccessoryCash = mChargeObject.transform.Find("Charge").GetComponent<TextMeshProUGUI>();
        for(int i = 2; i < accessory.transform.childCount; i++)
        {
            AccessoryToggle.Add(accessory.transform.GetChild(i).GetComponent<Toggle>());
        }
    }

    void Start()
    {   
        mInfoData = JsonUtility.FromJson<PowerUpInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mExplain = mInfoData.PowerUp[mAccessoryIndex-1].Explain;
        this.AccessoryMaxLevel = mInfoData.PowerUp[mAccessoryIndex-1].AccessoryLevel;

        for (int i = 0; i < UserInfo.instance.UserDataSet.PowerUpLevel[mAccessoryIndex - 1]; i++)
        {
            AccessoryToggle[i].isOn = true;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData) 
    {
        mAccessoryName.text = mThisAccessoryName.text;
        mAccessoryExplain.text = this.mExplain;
        mAccessoryImage.GetComponent<Image>().sprite = mThisAccessoryIamge.GetComponent<Image>().sprite;
        mChargeButton.GetComponent<ChargePowerUp>().NowAccessoryIndex = mAccessoryIndex - 1;
        AccessoryCash.text = UserInfo.instance.UserDataSet.NowPowerUpCash[mAccessoryIndex - 1].ToString();
        if(UserInfo.instance.UserDataSet.PowerUpLevel[mAccessoryIndex - 1] == AccessoryMaxLevel)
        {
            mChargeObject.SetActive(false);
            mActiveObject.SetActive(true);
        }
        else
        {
            mChargeObject.SetActive(true);
            mActiveObject.SetActive(false);
        }
    }
}