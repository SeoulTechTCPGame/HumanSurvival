using UnityEngine;
using TMPro;

public class UserCollection : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mCollectText;

    private int mCollectionCount = 0;

    private void Start()
    {
        SetMoneyText();
        SetCollectionText();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<SceneMove>().ToBack();
        }
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
    private void SetCollectionText(){
        for(int i = 0; i < UserInfo.instance.CollectionManager.Collections.Count; i++)
        {
            if(UserInfo.instance.UserDataSet.BCollection[i])
            {
                mCollectionCount++;
            }
        }
        mCollectText.text = "Collection : " + mCollectionCount.ToString() + " / " + UserInfo.instance.CollectionManager.Collections.Count;
    }
}