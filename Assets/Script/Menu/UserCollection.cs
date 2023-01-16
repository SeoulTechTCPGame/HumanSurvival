using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserCollection : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text collectText;

    int collectionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetMoneyText();
        SetCollectionText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    void SetMoneyText(){
        moneyText.text = UserInfo.money.ToString();
    }

    void SetCollectionText(){
        for(int i = 0; i < UserInfo.itemCount; i++){
            if(UserInfo.userItem[i]){
                collectionCount++;
            }
        }
        collectText.text = "Collection : " + collectionCount.ToString() + " / " + UserInfo.itemCount;
    }
}
