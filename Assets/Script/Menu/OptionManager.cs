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
    public Text buttontext; //DataRecovery�ؽ�Ʈ
    //Panel�ʱ�ȭ
    private void Awake()
    {
=======
    public GameObject DataRecovery;
    public TMP_Text buttonText; //DataRecovery�ؽ�Ʈ
    public TMP_Text moneyText;
    //Panel�ʱ�ȭ
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
    //�ڷΰ��� ��ư
    public void ClickBackButton()
    {
        //TODO: �� ���¿��� back ��ư�� ������ ����ȭ������ �����Ѵ�.
        /*
        if (FirstPanel.activeSelf == true & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == false) { }
        */
        //�ι�° ���������� ù��° �������� �̵�
=======
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    //�ڷΰ��� ��ư
    public void ClickBackButton()
    {
        //TODO: �� ���¿��� back ��ư�� ������ ����ȭ������ �����Ѵ�.
        
        if (FirstPanel.activeSelf == true & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == false)
        {
            SceneManager.LoadScene("MainScreen");
        }
        
        //�ι�° ���������� ù��° �������� �̵�
>>>>>>> origin/develop-mainCollection
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == true & ThirdPanel.activeSelf == false)
        {
            SecondPanel.SetActive(false);
            FirstPanel.SetActive(true);
<<<<<<< HEAD
        }
        //DataRecovery���� ù��° �������� �̵�
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == true)
        {
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
        }
        //�׿ܴ� button�� onClick���� ���
=======
            DataRecovery.SetActive(true);

        }
        //DataRecovery���� ù��° �������� �̵�
        if (FirstPanel.activeSelf == false & SecondPanel.activeSelf == false & ThirdPanel.activeSelf == true)
        {
            buttonText.text = "data\nrecovery";
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
        }
        //�׿ܴ� button�� onClick���� ���
    }
    
    void SetMoneyText(){
        moneyText.text = UserInfo.money.ToString();
>>>>>>> origin/develop-mainCollection
    }
}
