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
    [SerializeField] Toggle mAchiToggle;
    [SerializeField] Image mAchiImage;
    [SerializeField] Image mThisAchiIamge;
    [SerializeField] Image mAchiImageBG;
    [SerializeField] TMP_Text mThisAchiName;
    [SerializeField] TMP_Text mAchiName;
    [SerializeField] TMP_Text mAchiExplain;
    [SerializeField] TMP_Text mAchiObtain;

    AchiInfoData InfoData;
    string mExplain;
    string mObtain;

    // Start is called before the first frame update
    void Start(){
        InfoData = JsonUtility.FromJson<AchiInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mExplain = InfoData.Achievement[mAchi-1].Explain;
        this.mObtain = InfoData.Achievement[mAchi-1].Obtain;

        if (UserInfo.instance.UserDataSet.Achievements[mAchi]) {
            mAchiToggle.GetComponent<Toggle>().isOn = true;
        }
        mThisAchiName.text = this.mExplain;
    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (UserInfo.instance.UserDataSet.Achievements[mAchi]) {
            mAchiName.text = "획득";
            mAchiImageBG.color = new Color(0f, 1f, 1f, 1f);
        }
        else{
            mAchiName.text = "";
            mAchiImageBG.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        
        mAchiExplain.text = this.mExplain;
        mAchiObtain.text = this.mObtain;
        mAchiImage.GetComponent<Image>().sprite = mThisAchiIamge.GetComponent<Image>().sprite;
    }

}
