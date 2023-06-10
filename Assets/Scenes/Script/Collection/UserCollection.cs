using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserCollection : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mCollectText;

    int mCollectionCount = 0;

    private void Start()
    {
        SetMoneyText();
        SetCollectionText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            GetComponent<SceneMove>().ToBack();
        }
    }

    private void SetMoneyText(){
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }

    private void SetCollectionText(){
        for(int i = 0; i < Constants.MaxCollectionNumber; i++){
            if(UserInfo.instance.UserDataSet.Collection[i]){
                mCollectionCount++;
            }
        }
        mCollectText.text = "Collection : " + mCollectionCount.ToString() + " / " + Constants.MaxCollectionNumber;
    }
}
