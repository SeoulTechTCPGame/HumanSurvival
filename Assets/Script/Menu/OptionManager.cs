using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
using UnityEngine.SceneManagement;
using TMPro;
>>>>>>> origin/develop-mainCollection

public class OptionManager : MonoBehaviour
{
    public GameObject DefaultPanel; //OptionBackground
    public GameObject FirstPanel;   //OptionMenu_1Page
    public GameObject SecondPanel;  //OptionMenu_2Page
    public GameObject ThirdPanel;   //DataRecovery
<<<<<<< HEAD
    public Text buttontext; //DataRecoveryÅØ½ºÆ®
    //PanelÃÊ±âÈ­
    private void Awake()
    {
=======
    public GameObject DataRecovery;
    public TMP_Text buttonText; //DataRecoveryï¿½Ø½ï¿½Æ®
    public TMP_Text moneyText;
    //Panelï¿½Ê±ï¿½È­
    private void Awake()
    {
        SetMoneyText();
>>>>>>> origin/develop-mainCollection
        DefaultPanel.SetActive(true);
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
    }

<<<<<<< HEAD
    //µÚ·Î°¡±â ¹öÆ°
    public void ClickBackButton()
    {
        //TODO: ÀÌ »óÅÂ¿¡¼­ back ¹öÆ°À» ´©¸£¸é ¸ÞÀÎÈ­¸éÀ¸·Î °¡¾ßÇÑ´Ù.
        /*
        if (FirstPanel.activeSelf == true & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == false) { }
        */
        //µÎ¹øÂ° ÆäÀÌÁö¿¡¼­ Ã¹¹øÂ° ÆäÀÌÁö·Î ÀÌµ¿
=======
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    //ï¿½Ú·Î°ï¿½ï¿½ï¿½ ï¿½ï¿½Æ°
    public void ClickBackButton()
    {
        //TODO: ï¿½ï¿½ ï¿½ï¿½ï¿½Â¿ï¿½ï¿½ï¿½ back ï¿½ï¿½Æ°ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½È­ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ñ´ï¿½.
        
        if (FirstPanel.activeSelf == true & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == false)
        {
            SceneManager.LoadScene("MainScreen");
        }
        
        //ï¿½Î¹ï¿½Â° ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Ã¹ï¿½ï¿½Â° ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ìµï¿½
>>>>>>> origin/develop-mainCollection
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == true & ThirdPanel.activeSelf == false)
        {
            SecondPanel.SetActive(false);
            FirstPanel.SetActive(true);
<<<<<<< HEAD
        }
        //DataRecovery¿¡¼­ Ã¹¹øÂ° ÆäÀÌÁö·Î ÀÌµ¿
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == true)
        {
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
        }
        //±×¿Ü´Â buttonÀÇ onClick¿¡¼­ »ç¿ë
=======
            DataRecovery.SetActive(true);

        }
        //DataRecoveryï¿½ï¿½ï¿½ï¿½ Ã¹ï¿½ï¿½Â° ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½Ìµï¿½
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == true)
        {
            buttonText.text = "data\nrecovery";
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
        }
        //ï¿½×¿Ü´ï¿½ buttonï¿½ï¿½ onClickï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½
    }
    
    void SetMoneyText(){
        moneyText.text = UserInfo.money.ToString();
>>>>>>> origin/develop-mainCollection
    }
}
