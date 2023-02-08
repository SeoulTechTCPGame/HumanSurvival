using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserAchi : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text achiText;

    int achiCount = 0;
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
        for(int i = 0; i < UserInfo.achiCount; i++){
            if(UserInfo.userAchi[i]){
                achiCount++;
            }
        }
        achiText.text = "잠금 해제됨 : " + achiCount.ToString() + " / " + UserInfo.achiCount;
    }
}
