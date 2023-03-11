using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserAchi : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mAchiText;

    int achiCount = 0;
    void Start()
    {
        SetMoneyText();
        SetCollectionText();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    void SetMoneyText(){
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }

    void SetCollectionText(){
        for(int i = 0; i < Constants.achiCount; i++){
            if(UserInfo.instance.UserDataSet.Achievements[i]){
                achiCount++;
            }
        }
        mAchiText.text = "잠금 해제됨 : " + achiCount.ToString() + " / " + Constants.achiCount;
    }
}
