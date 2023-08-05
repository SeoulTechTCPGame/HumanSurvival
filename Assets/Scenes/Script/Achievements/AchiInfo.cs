using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
    private string mExplain;
    private string mObtain;

    private void Awake() 
    {
        if (mAchi > UserInfo.instance.AchiManager.Achievements.Count)
            Destroy(gameObject);
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
        this.mExplain = UserInfo.instance.AchiManager.Achievements[mAchi - 1].Explain;
        this.mObtain = UserInfo.instance.AchiManager.Achievements[mAchi - 1].Obtain;

        if (UserInfo.instance.UserDataSet.BAchievements[mAchi - 1]) 
        {
            mThisAchiToggle.GetComponent<Toggle>().isOn = true;
            mThisAchiIamge.sprite = UserInfo.instance.AchiManager.Achievements[mAchi - 1].Sprite;
        }
        mThisAchiName.text = this.mExplain;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (UserInfo.instance.UserDataSet.BAchievements[mAchi - 1])
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