using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;

public class OptionManager : MonoBehaviour
{
    [SerializeField] GameObject mBGPanel; //OptionBackground
    [SerializeField] GameObject mDefaultPanel;   //OptionMenu
    [SerializeField] GameObject mDataPanel;   //DataRecovery
    [SerializeField] GameObject mWarningPanel;   // 경고
    [SerializeField] GameObject mParsingErrorPanel;
    [SerializeField] TMP_Text mButtonText; //DataRecovery텍스트
    [SerializeField] TMP_Text mMoneyText;    //돈 표시

    private void Awake()
    {
        SetMoneyText();

        mBGPanel.SetActive(true);
        mDefaultPanel.SetActive(true);
        mDataPanel.SetActive(false);
        mWarningPanel.SetActive(false);
        mParsingErrorPanel.SetActive(false);
    }
    private void Start()
    {
        // 저장된 볼륨 값 로드
        SoundManager soundManager = SoundManager.instance;

        // defaultPanel의 자식 요소들을 가져와서 슬라이더를 초기화
        Slider[] sliders = mDefaultPanel.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            switch (slider.name)
            {
                case "BgmSlider":
                    slider.value = soundManager.BgmVolume;
                    slider.onValueChanged.AddListener(OnBgmVolumeChanged);
                    break;
                case "SoundEffectSlider":
                    slider.value = soundManager.SoundEffectVolume;
                    slider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
                    break;
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<SceneMove>().ToBack();
        }
    }
    public void ClickBackButton()
    {
        if (mDefaultPanel.activeSelf == true)
        {
            GetComponent<SceneMove>().ToBack();
        }
        else
        {
            mButtonText.text = "data\nrecovery";
            mDataPanel.SetActive(false);
            mDefaultPanel.SetActive(true);
        }
    }
    public void LoadSystemData()
    {
        string filePath = EditorUtility.OpenFilePanel("Json Explorer", "", "json");
        bool bErrorFile;
        try
        {
            bErrorFile = !UserDataManager.instance.LoadData(filePath);
        }
        catch (ArgumentException)
        {
            return;
        }

        mWarningPanel.SetActive(false);
        if (bErrorFile)
        {
            LoadParsingError();
        }
        else
        {
            GetComponent<SceneMove>().ToBack();
        }
    }
    public void LoadWarning()
    {
        mWarningPanel.SetActive(true);
    }
    public void NoOnWarning()
    {
        mWarningPanel.SetActive(false);
    }
    public void LoadParsingError()
    {
        mParsingErrorPanel.SetActive(true);
    }
    public void YesOnParsingError()
    {
        mParsingErrorPanel.SetActive(false);
    }
    private void OnBgmVolumeChanged(float value)
    {
        // BGM 볼륨 값을 변경
        SoundManager soundManager = SoundManager.instance;
        soundManager.BgmVolume = value;
        Debug.Log(soundManager.BgmVolume);
    }
    private void OnSoundEffectVolumeChanged(float value)
    {
        // 사운드 이펙트 볼륨 값을 변경
        SoundManager soundManager = SoundManager.instance;
        soundManager.SoundEffectVolume = value;
        Debug.Log(soundManager.SoundEffectVolume);
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}