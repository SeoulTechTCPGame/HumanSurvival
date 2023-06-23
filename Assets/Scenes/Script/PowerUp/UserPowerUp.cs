using UnityEngine;
using TMPro;

public class UserPowerUp : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;

    private void Start()
    {
        SetMoneyText();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<SceneMove>().ToBack();
        }
        SetMoneyText();
    }

    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}
