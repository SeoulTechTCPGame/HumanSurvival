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
    [SerializeField] Image mItemImage;
    [SerializeField] Image mThisItemIamge;
    [SerializeField] Sprite mBlackImage;
    [SerializeField] Sprite mGreenImage;
    [SerializeField] Sprite mPinkImage;
    [SerializeField] Sprite mPurpleImage;

    [SerializeField] TMP_Text mWeaponItemName;
    [SerializeField] TMP_Text mWeaponExplain;

    CollectionInfoData InfoData;
    string mItemName;
    string mExplain;
    string mRank;

    // Start is called before the first frame update
    void Start(){
        InfoData = JsonUtility.FromJson<CollectionInfoData>(Resources.Load<TextAsset>("GameData/ItemExplainDataKorean").ToString());
        this.mItemName = InfoData.Collection[mItemIndex-1].Name;
        this.mExplain = InfoData.Collection[mItemIndex-1].Explain;
        this.mRank = InfoData.Collection[mItemIndex-1].Rank;

        
        if (!UserInfo.instance.UserDataSet.Collection[mItemIndex]) {
            this.mItemName = "???";
            this.mExplain = "아직 발견하지 못했습니다.";
            if(this.mRank == "black"){
                mThisItemIamge.GetComponent<Image>().sprite = mBlackImage;
            }
            else if(this.mRank == "green"){
                mThisItemIamge.GetComponent<Image>().sprite = mGreenImage;
            }
            else if(this.mRank == "pink"){
                mThisItemIamge.GetComponent<Image>().sprite = mPinkImage;
            }
            else{
                mThisItemIamge.GetComponent<Image>().sprite = mPurpleImage;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        mWeaponItemName.text = this.mItemName;
        mWeaponExplain.text = this.mExplain;
        mItemImage.GetComponent<Image>().sprite = mThisItemIamge.GetComponent<Image>().sprite;
    }

}
