using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UserAchi : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mAchiText;
    [SerializeField] Toggle mCompleteHide;
    [SerializeField] GameObject[] mAchiObject;

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

    void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }

    void SetCollectionText()
    {
        for(int i = 0; i < Constants.MaxAchievementNumber; i++){
            if(UserInfo.instance.UserDataSet.Achievements[i]){
                achiCount++;
            }
        }
        mAchiText.text = "잠금 해제됨 : " + achiCount.ToString() + " / " + Constants.MaxAchievementNumber;
    }

    public void CompleteHide()
    {
        if(mCompleteHide.GetComponent<Toggle>().isOn)
        {
            for(int i = 1; i <= Constants.MaxAchievementNumber; i++)
            {
                if(UserInfo.instance.UserDataSet.Achievements[i])
                {
                    mAchiObject[i - 1].SetActive(false);
                }
            }
        }
        else
        {
            for(int i = 1; i <= Constants.MaxAchievementNumber; i++)
            {
                if(UserInfo.instance.UserDataSet.Achievements[i])
                {
                    mAchiObject[i - 1].SetActive(true);
                }
            }
        }
    }
}
