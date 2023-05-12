using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    [SerializeField] Image mThisAccessoryIamge;
    [SerializeField] Image mAccessoryImage;
    [SerializeField] TMP_Text mThisAccessoryName;
    [SerializeField] TMP_Text mAccessoryName;
    [SerializeField] TMP_Text mAccessoryExplain;
    [SerializeField] Button ChargeButton;
    [SerializeField] GameObject mChargeObject;
    [SerializeField] GameObject mActiveObject;
    public TMP_Text mAccessoryCash;
    public Toggle[] accessoryToggle;

    PowerUpInfoData InfoData;
    string mExplain;
    public int accessoryLevel;

    void Start()
    {   
        InfoData = JsonUtility.FromJson<PowerUpInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mExplain = InfoData.PowerUp[mAccessoryIndex-1].Explain;
        this.accessoryLevel = InfoData.PowerUp[mAccessoryIndex-1].accessoryLevel;

        for (int i = 0; i < UserInfo.instance.UserDataSet.PowerUpLevel[mAccessoryIndex - 1]; i++)
        {
            accessoryToggle[i].isOn = true;
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
