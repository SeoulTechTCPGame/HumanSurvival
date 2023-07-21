using UnityEngine;
using UnityEngine.UI;

public class OptionalController : MonoBehaviour
{
    [SerializeField] Slider mBgmSlider;
    [SerializeField] Slider mSoundEffectSlider;
    [SerializeField] Toggle mFullScreenToggle;

    private SoundManager mSoundManager;

    private void Start()
    {
        mSoundManager = SoundManager.instance;

        // 저장된 설정 값을 슬라이더에 반영
        LoadSettings();

        mBgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        mSoundEffectSlider.onValueChanged.AddListener(OnSoundEffectVolumeChanged);
        mFullScreenToggle.onValueChanged.AddListener(OnFullScreenToggleChanged);
    }
    private void LoadSettings()
    {
        // BGM 볼륨 로드
        float bgmVolume = mSoundManager.BgmVolume;
        mBgmSlider.value = bgmVolume;

        // 사운드 이펙트 볼륨 로드
        float soundEffectVolume = mSoundManager.SoundEffectVolume;
        mSoundEffectSlider.value = soundEffectVolume;

        // 전체 화면 토글 로드
        bool isFullScreen = mSoundManager.IsFullScreen;
        mFullScreenToggle.isOn = isFullScreen;
        OnFullScreenToggleChanged(isFullScreen);
    }
    public void OnBgmVolumeChanged(float value)
    {
        // BGM 볼륨 값을 변경
        mSoundManager.BgmVolume = value;
        mSoundManager.BgmAudioSource.volume = value;

        mSoundManager.SaveSettings();
    }
    public void OnSoundEffectVolumeChanged(float value)
    {
        // 사운드 이펙트 볼륨 값을 변경
        mSoundManager.SoundEffectVolume = value;
        mSoundManager.SoundEffectAudioSource.volume = value;

        mSoundManager.SaveSettings();
    }
    public void OnFullScreenToggleChanged(bool value)
    {
        mSoundManager.IsFullScreen = value;

        if (value)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
        }

        mSoundManager.SaveSettings();
    }
}