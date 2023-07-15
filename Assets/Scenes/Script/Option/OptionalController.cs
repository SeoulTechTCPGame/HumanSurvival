using UnityEngine;
using UnityEngine.UI;

public class OptionalController : MonoBehaviour
{
    [SerializeField] Slider mBgmSlider;
    [SerializeField] Slider mSoundEffectSlider;
    [SerializeField] Toggle mVFXToggle;
    [SerializeField] Toggle mFullScreenToggle;
    [SerializeField] Toggle mShowDamageToggle;
    [SerializeField] Toggle mHideStageToggle;
    private SoundManager mSoundManager;

    private void Start()
    {
        mSoundManager = SoundManager.instance;

        // 저장된 설정 값을 슬라이더에 반영
        LoadSettings();

        mBgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        mSoundEffectSlider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
        mVFXToggle.onValueChanged.AddListener(OnVFXToggleChanged);
        mFullScreenToggle.onValueChanged.AddListener(OnFullScreenToggleChanged);
        mShowDamageToggle.onValueChanged.AddListener(OnShowDamageToggleChanged);
        mHideStageToggle.onValueChanged.AddListener(OnHideStageToggleChanged);
    }
    private void LoadSettings()
    {
        // BGM 볼륨 로드
        float bgmVolume = mSoundManager.BgmVolume;
        mBgmSlider.value = bgmVolume;

        // 사운드 이펙트 볼륨 로드
        float soundEffectVolume = mSoundManager.SoundEffectVolume;
        mSoundEffectSlider.value = soundEffectVolume;
    }
    public void OnBgmVolumeChanged(float value)
    {
        // BGM 볼륨 값을 변경
        mSoundManager.BgmVolume = value;
        SoundManager.instance.AudioSource.volume = value;
    }
    public void OnSoundEffectVolumeChanged(float value)
    {
        // 사운드 이펙트 볼륨 값을 변경
        mSoundManager.SoundEffectVolume = value;
    }
    public void OnVFXToggleChanged(bool value)
    {
        SoundManager.instance.EnableVFX(value);
    }
    public void OnFullScreenToggleChanged(bool value)
    {
        if (value)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(800, 600, false);
        }
    }
    public void OnShowDamageToggleChanged(bool value)
    {
        SoundManager.instance.EnableDamageDisplay(value);
    }
    public void OnHideStageToggleChanged(bool value)
    {
        SoundManager.instance.HideStage(value);
    }
}