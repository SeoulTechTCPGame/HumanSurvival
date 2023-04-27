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
    public TMP_Text mAccessoryCash;
    public Toggle[] accessoryToggle;

    PowerUpInfoData InfoData;
    string mExplain;
    public int accessoryLevel;

    void Start()
    {   
        // switch(mAccessoryIndex){
        //     case 1:
        //         this.mExplain = "랭크당 피해량이 5% 증가합니다\n(최대 +25%)";
        //         this.accessoryLevel = 5;
        //         break;
            
        //     case 2:
        //         this.mExplain = "랭크당 피격 피해량이 1 줄어듭니다\n(최대 -3)";
        //         this.accessoryLevel = 3;
        //         break;

        //     case 3:
        //         this.mExplain = "랭크당 최대 체력이 10% 증가합니다\n(최대 +30%)";
        //         this.accessoryLevel = 3;
        //         break;

        //     case 4:
        //         this.mExplain = "랭크당 초당 체력 회복량이 0.1 증가합니다\n(최대 0.5)";
        //         this.accessoryLevel = 5;
        //         break;

        //     case 5:
        //         this.mExplain = "랭크당 무기 쿨타임이 2.5% 감소합니다\n(최대 5%)";
        //         this.accessoryLevel = 2;
        //         break;

        //     case 6:
        //         this.mExplain = "랭크당 공격 범위가 5% 증가합니다\n(최대 +10%)";
        //         this.accessoryLevel = 2;
        //         break;

        //     case 7:
        //         this.mExplain = "랭크당 투사체가 10% 빨라집니다\n(최대 20%)";
        //         this.accessoryLevel = 2;
        //         break;

        //     case 8:
        //         this.mExplain = "랭크당 무기의 효과 지속 시간이 15% 연장됩니다\n(최대 +30%)";
        //         this.accessoryLevel = 2;
        //         break;

        //     case 9:
        //         this.mExplain = "투사체를 1개 더 발사합니다\n(모든 무기)";
        //         this.accessoryLevel = 1;
        //         break;

        //     case 10:
        //         this.mExplain = "랭크당 이동 속도가 5% 빨라집니다\n(최대 10%)";
        //         this.accessoryLevel = 2;
        //         break;

        //     case 11:
        //         this.mExplain = "랭크당 아이템 획득 범위가 25% 증가합니다\n(최대 +50%)";
        //         this.accessoryLevel = 2;
        //         break;

        //     case 12:
        //         this.mExplain = "랭크당 행운이 10% 증가합니다\n(최대 +30%)";
        //         this.accessoryLevel = 3;
        //         break;

        //     case 13:
        //         this.mExplain = "랭크당 3%의 추가 경험치를 획득합니다\n(최대 15%)";
        //         this.accessoryLevel = 5;
        //         break;

        //     case 14:
        //         this.mExplain = "랭크당 10%의 추가 골드를 획득합니다\n(최대 +50%)";
        //         this.accessoryLevel = 5;
        //         break;

        //     case 15:
        //         this.mExplain = "랭크당 10%씩 적들의 속도, 체력, 개체수, 스폰율이 증가합니다\n(최대 +50%)";
        //         this.accessoryLevel = 5;
        //         break;

        //     case 16:
        //         this.mExplain = "50% 체력으로 1회 부활합니다";
        //         this.accessoryLevel = 1;
        //         break;
        // }
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
    }
}
