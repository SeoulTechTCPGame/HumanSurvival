using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
class AchiData
{
    public string Explain;
    public string Obtain;
}

[System.Serializable]
class AchiInfoData
{
    public AchiData[] Achievement;
}

public class AchiInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int mAchi;
    [SerializeField] GameObject mContext;
    [SerializeField] GameObject mExplainBG;

    private Toggle mThisAchiToggle;
    private TMP_Text mThisAchiName;
    private Image mThisAchiIamge;
    private Image mAchiImageBG;
    private Image mAchiImage;
    private TMP_Text mAchiName;
    private TMP_Text mAchiExplain;
    private TMP_Text mAchiObtain;
    private AchiInfoData mInfoData;
    private string mExplain;
    private string mObtain;

    private void Awake() 
    {
        Transform achi = mContext.transform.GetChild(mAchi - 1);

        mThisAchiToggle = achi.GetComponent<Toggle>();
        mThisAchiName = achi.transform.Find("ObtainExplain").GetComponent<TextMeshProUGUI>();
        mThisAchiIamge = achi.transform.Find("AchiImage").GetComponent<Image>();
        mAchiImageBG = mExplainBG.GetComponent<Image>();
        mAchiImage = mExplainBG.transform.Find("AchiImage").GetComponent<Image>();
        mAchiName = mExplainBG.transform.Find("AchiName").GetComponent<TextMeshProUGUI>();
        mAchiExplain = mExplainBG.transform.Find("AchiExplain").GetComponent<TextMeshProUGUI>();
        mAchiObtain = mExplainBG.transform.Find("AchiObtain").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        mInfoData = JsonUtility.FromJson<AchiInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mExplain = mInfoData.Achievement[mAchi-1].Explain;
        this.mObtain = mInfoData.Achievement[mAchi-1].Obtain;

        if (UserInfo.instance.UserDataSet.BAchievements[mAchi]) 
        {
            mThisAchiToggle.GetComponent<Toggle>().isOn = true;
        }
        mThisAchiName.text = this.mExplain;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UserInfo.instance.UserDataSet.BAchievements[mAchi])
        {
            mAchiName.text = "획득";
            mAchiImageBG.color = new Color(0f, 1f, 1f, 1f);
        }
        else
        {
            mAchiName.text = "";
            mAchiImageBG.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        
        mAchiExplain.text = this.mExplain;
        mAchiObtain.text = this.mObtain;
        mAchiImage.GetComponent<Image>().sprite = mThisAchiIamge.GetComponent<Image>().sprite;
    }
}