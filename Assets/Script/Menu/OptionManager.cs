using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.IO;
using System;

public class OptionManager : MonoBehaviour
{
    public GameObject DefaultPanel; //OptionBackground
    public GameObject FirstPanel;   //OptionMenu_1Page
    public GameObject SecondPanel;  //OptionMenu_2Page
    public GameObject ThirdPanel;   //DataRecovery
    public GameObject DataRecovery;
    public GameObject WarningPanel;
    public GameObject ParsingErrorPanel;
    public TMP_Text buttonText; //DataRecovery�ؽ�Ʈ
    public TMP_Text moneyText;
    //Panel�ʱ�ȭ
    private void Awake()
    {
        SetMoneyText();
        DefaultPanel.SetActive(true);
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        ThirdPanel.SetActive(false);
        WarningPanel.SetActive(false);
        ParsingErrorPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene("MainScreen");
        }
    }

    //�ڷΰ��� ��ư
    public void ClickBackButton()
    {
        if (FirstPanel.activeSelf == true)
        {
            SceneManager.LoadScene("MainScreen");
        }
        else if (SecondPanel.activeSelf == true)
        {
            SecondPanel.SetActive(false);
            FirstPanel.SetActive(true);
            DataRecovery.SetActive(true);
        }
        else
        {
            buttonText.text = "data\nrecovery";
            ThirdPanel.SetActive(false);
            FirstPanel.SetActive(true);
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
