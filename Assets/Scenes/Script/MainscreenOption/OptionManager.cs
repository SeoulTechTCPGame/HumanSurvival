using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System;

public class OptionManager : MonoBehaviour
{
    [SerializeField] GameObject BGPanel; //OptionBackground
    [SerializeField] GameObject DefaultPanel;   //OptionMenu
    [SerializeField] GameObject DataPanel;   //DataRecovery
    [SerializeField] GameObject WarningPanel;   // 경고
    [SerializeField] GameObject ParsingErrorPanel;
    [SerializeField] TMP_Text buttonText; //DataRecovery텍스트
    [SerializeField] TMP_Text moneyText;    //돈 표시

    private void Awake()
    {
        SetMoneyText();
        BGPanel.SetActive(true);
        DefaultPanel.SetActive(true);
        DataPanel.SetActive(false);
        WarningPanel.SetActive(false);
        ParsingErrorPanel.SetActive(false);
    }
    private void Start()
    {
        // 저장된 볼륨 값 로드
        SoundManager soundManager = SoundManager.instance;

        // defaultPanel의 자식 요소들을 가져와서 슬라이더를 초기화
        Slider[] sliders = DefaultPanel.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            switch (slider.name)
            {
                case "BgmSlider":
                    slider.value = soundManager.bgmVolume;
                    slider.onValueChanged.AddListener(OnBgmVolumeChanged);
                    break;
                case "SoundEffectSlider":
                    slider.value = soundManager.soundEffectVolume;
                    slider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
                    break;
            }
        }
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
    void OnBgmVolumeChanged(float value)
    {
        // BGM 볼륨 값을 변경
        SoundManager soundManager = SoundManager.instance;
        soundManager.bgmVolume = value;
        Debug.Log(soundManager.bgmVolume);
    }
    void OnSoundEffectVolumeChanged(float value)
    {
        // 사운드 이펙트 볼륨 값을 변경
        SoundManager soundManager = SoundManager.instance;
        soundManager.soundEffectVolume = value;
        Debug.Log(soundManager.soundEffectVolume);
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
