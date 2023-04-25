using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserPowerUp : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;

    // Start is called before the first frame update
    void Start()
    {
        SetMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
        SetMoneyText();
    }

    void SetMoneyText(){
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}
