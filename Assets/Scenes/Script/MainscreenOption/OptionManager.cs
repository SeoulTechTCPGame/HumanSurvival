using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System;

public class OptionManager : MonoBehaviour
{
    public GameObject BGPanel; //OptionBackground
    public GameObject DefaultPanel;   //OptionMenu
    public GameObject DataPanel;   //DataRecovery
    public GameObject WarningPanel;
    public GameObject ParsingErrorPanel;
    public TMP_Text buttonText; //DataRecovery텍스트
    public TMP_Text moneyText;
    //Panel초기화
    private void Awake()
    {
        SetMoneyText();
        BGPanel.SetActive(true);
        DefaultPanel.SetActive(true);
        DataPanel.SetActive(false);
        WarningPanel.SetActive(false);
        ParsingErrorPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    //뒤로가기 버튼
    public void ClickBackButton()
    {
        if (DefaultPanel.activeSelf == true)
        {
            SceneManager.LoadScene("MainScreen");
        }
        else
        {
            buttonText.text = "data\nrecovery";
            DataPanel.SetActive(false);
            DefaultPanel.SetActive(true);
        }
    }
    void SetMoneyText()
    {
        moneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
    public void LoadSystemData()
    {
        string filePath = EditorUtility.OpenFilePanel("Json Explorer", "", "json");
        bool IsErrorFile;
        try
        {
            IsErrorFile = !UserDataManager.instance.LoadData(filePath);
        }
        catch (ArgumentException)
        {
            return;
        }

        WarningPanel.SetActive(false);
        if (IsErrorFile)
        {
            LoadParsingError();
        }
        else
        {
            SceneManager.LoadScene("MainScreen");
        }
    }
    public void LoadWarning()
    {
        WarningPanel.SetActive(true);
    }
    public void NoOnWarning()
    {
        WarningPanel.SetActive(false);
    }
    public void LoadParsingError()
    {
        ParsingErrorPanel.SetActive(true);
    }
    public void YesOnParsingError()
    {
        ParsingErrorPanel.SetActive(false);
    }
}
