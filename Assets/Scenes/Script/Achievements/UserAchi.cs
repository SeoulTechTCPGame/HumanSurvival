using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserAchi : MonoBehaviour
{
    [SerializeField] TMP_Text       mMoneyText;
    [SerializeField] TMP_Text       mAchiText;
    [SerializeField] Toggle         mCompleteHide;
    [SerializeField] GameObject[]   mAchiObject;

    private int mAchiCount = 0;

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
    public void CompleteHide()
    {
        if (mCompleteHide.GetComponent<Toggle>().isOn)
        {
            for (int i = 0; i <= UserInfo.instance.AchiManager.Achievements.Count; i++)
            {
                if (UserInfo.instance.UserDataSet.BAchievements[i])
                {
                    mAchiObject[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i <= UserInfo.instance.AchiManager.Achievements.Count; i++)
            {
                if (UserInfo.instance.UserDataSet.BAchievements[i])
                {
                    mAchiObject[i].SetActive(true);
                }
            }
        }
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
    private void SetCollectionText()
    {
        for(int i = 0; i < UserInfo.instance.AchiManager.Achievements.Count; i++)
        {
            if(UserInfo.instance.UserDataSet.BAchievements[i])
            {
                mAchiCount++;
            }
        }
        mAchiText.text = "잠금 해제됨 : " + mAchiCount.ToString() + " / " + UserInfo.instance.AchiManager.Achievements.Count;
    }
}
