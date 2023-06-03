using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
class PowerUpData
{
    public string Explain;
    public int accessoryLevel;
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
    [SerializeField] GameObject mAccessoryExplains;

    private Image mThisAccessoryIamge;
    private TMP_Text mThisAccessoryName;
    private Image mAccessoryImage;
    private TMP_Text mAccessoryName;
    private TMP_Text mAccessoryExplain;
    private Button ChargeButton;
    private GameObject mChargeObject;
    private GameObject mActiveObject;
    public TMP_Text mAccessoryCash;
    public List<Toggle> mAccessoryToggle;

    PowerUpInfoData InfoData;
    string mExplain;
    public int accessoryLevel;

    void Awake() {
        Transform Accessory = mContext.transform.GetChild(mAccessoryIndex - 1);
        mThisAccessoryName = Accessory.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        mThisAccessoryIamge = Accessory.transform.GetChild(1).GetComponent<Image>();
        mAccessoryImage = mAccessoryExplains.transform.Find("AccessoryImage").GetComponent<Image>();
        mAccessoryName = mAccessoryExplains.transform.Find("AccessoryName").GetComponent<TextMeshProUGUI>();
        mAccessoryExplain = mAccessoryExplains.transform.Find("AccessoryExplain").GetComponent<TextMeshProUGUI>();
        mChargeObject = mAccessoryExplains.transform.Find("ChargeObject").gameObject;
        mActiveObject = mAccessoryExplains.transform.Find("ActiveObject").gameObject;
        ChargeButton = mChargeObject.transform.Find("ChargeButton").GetComponent<Button>();
        mAccessoryCash = mChargeObject.transform.Find("Charge").GetComponent<TextMeshProUGUI>();
        for(int i = 2; i < Accessory.transform.childCount; i++)
        {
            mAccessoryToggle.Add(Accessory.transform.GetChild(i).GetComponent<Toggle>());
        }
    }

    void Start()
    {   
        InfoData = JsonUtility.FromJson<PowerUpInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mExplain = InfoData.PowerUp[mAccessoryIndex-1].Explain;
        this.accessoryLevel = InfoData.PowerUp[mAccessoryIndex-1].accessoryLevel;

        for (int i = 0; i < UserInfo.instance.UserDataSet.PowerUpLevel[mAccessoryIndex - 1]; i++)
        {
            mAccessoryToggle[i].isOn = true;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData) 
    {
        mAccessoryName.text = mThisAccessoryName.text;
        mAccessoryExplain.text = this.mExplain;
        mAccessoryImage.GetComponent<Image>().sprite = mThisAccessoryIamge.GetComponent<Image>().sprite;
        ChargeButton.GetComponent<ChargePowerUp>().nowAccessoryIndex = mAccessoryIndex - 1;
        mAccessoryCash.text = UserInfo.instance.UserDataSet.nowPowerUpCash[mAccessoryIndex - 1].ToString();
        if(UserInfo.instance.UserDataSet.PowerUpLevel[mAccessoryIndex - 1] == accessoryLevel)
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
