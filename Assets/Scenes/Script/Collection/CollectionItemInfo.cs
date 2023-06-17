using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[System.Serializable]
class CollectionInfo
{
    public string Name;
    public string Explain;
    public string Rank;
}

[System.Serializable]
class CollectionInfoData
{
    public CollectionInfo[] Collection;
}

public class CollectionItemInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int mItemIndex;
    [SerializeField] GameObject mContext;
    [SerializeField] GameObject mExplainBG;
    [SerializeField] Sprite mBlackImage;
    [SerializeField] Sprite mGreenImage;
    [SerializeField] Sprite mPinkImage;
    [SerializeField] Sprite mPurpleImage;

    private Image mThisItemIamge;
    private Image mExplainBGItemImage;
    private TMP_Text mExplainBGItemName;
    private TMP_Text mExplainBGItemExplain;
    private CollectionInfoData mInfoData;
    private string mItemName;
    private string mExplain;
    private string mRank;

    private void Awake() 
    {
        Transform item = mContext.transform.GetChild(mItemIndex - 1);

        mThisItemIamge = item.transform.GetComponent<Image>();
        mExplainBGItemName = mExplainBG.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        mExplainBGItemExplain = mExplainBG.transform.Find("ItemExplain").GetComponent<TextMeshProUGUI>();
        mExplainBGItemImage = mExplainBG.transform.Find("ItemImage").GetComponent<Image>();
    }
    private void Start()
    {
        mInfoData = JsonUtility.FromJson<CollectionInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mItemName = mInfoData.Collection[mItemIndex-1].Name;
        this.mExplain = mInfoData.Collection[mItemIndex-1].Explain;
        this.mRank = mInfoData.Collection[mItemIndex-1].Rank;

        
        if (!UserInfo.instance.UserDataSet.BCollection[mItemIndex]) {
            this.mItemName = "???";
            this.mExplain = "아직 발견하지 못했습니다.";
            if(this.mRank == "black")
            {
                mThisItemIamge.GetComponent<Image>().sprite = mBlackImage;
            }
            else if(this.mRank == "green")
            {
                mThisItemIamge.GetComponent<Image>().sprite = mGreenImage;
            }
            else if(this.mRank == "pink")
            {
                mThisItemIamge.GetComponent<Image>().sprite = mPinkImage;
            }
            else
            {
                mThisItemIamge.GetComponent<Image>().sprite = mPurpleImage;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData) 
    {
        mExplainBGItemName.text = this.mItemName;
        mExplainBGItemExplain.text = this.mExplain;
        mExplainBGItemImage.GetComponent<Image>().sprite = mThisItemIamge.GetComponent<Image>().sprite;
    }
}