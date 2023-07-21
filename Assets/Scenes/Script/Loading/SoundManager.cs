using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float BgmVolume { get; set; } = 1.0f; // BGM 볼륨
    public float SoundEffectVolume { get; set; } = 1.0f; // 사운드 이펙트 볼륨
    public bool IsFullScreen { get; set; } = true; // 전체 화면 토글 상태

    public AudioClip ButtonSoundClip; // 버튼 소리 파일
    public AudioClip[] Bgm;
    public AudioSource BgmAudioSource; // BGM을 재생할 오디오 소스
    public AudioSource SoundEffectAudioSource; // 사운드 이펙트를 재생할 오디오 소스

    private string mCurrentScene; // 현재 씬의 이름을 저장할 변수
    private AudioClip currentSoundEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            BgmAudioSource = gameObject.AddComponent<AudioSource>();
            SoundEffectAudioSource = gameObject.AddComponent<AudioSource>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        mCurrentScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadSettings();

        PlayBgm(mCurrentScene);
    }
    #region 저장
    private void LoadSettings()
    {
        // BGM 볼륨 로드
        BgmVolume = PlayerPrefs.GetFloat("SoundManager_BgmVolume", 1.0f);

        // 사운드 이펙트 볼륨 로드
        SoundEffectVolume = PlayerPrefs.GetFloat("SoundManager_SoundEffectVolume", 1.0f);

        // 전체 화면 토글 로드
        IsFullScreen = PlayerPrefs.GetInt("SoundManager_IsFullScreen", 0) == 1;
    }
    public void SaveSettings()
    {
        // BGM 볼륨 저장
        PlayerPrefs.SetFloat("SoundManager_BgmVolume", BgmVolume);

        // 사운드 이펙트 볼륨 저장
        PlayerPrefs.SetFloat("SoundManager_SoundEffectVolume", SoundEffectVolume);

        // 전체 화면 토글 저장
        PlayerPrefs.SetInt("SoundManager_IsFullScreen", IsFullScreen ? 1 : 0);

        // PlayerPrefs 데이터를 디스크에 저장
        PlayerPrefs.Save();
    }
    #endregion
    #region 사운드 재생
    public void ChangeBGM(AudioClip clip)
    {
        BgmAudioSource.clip = clip;
        BgmAudioSource.volume = BgmVolume;
        BgmAudioSource.Play();
    }
    public void PlayButtonSound()
    {
        SoundEffectAudioSource.PlayOneShot(ButtonSoundClip, SoundEffectVolume);
    }
    public void PlaySoundEffect(AudioClip soundEffectClip)
    {
        SoundEffectAudioSource.PlayOneShot(soundEffectClip, SoundEffectVolume);
    }
    public void PlayRateSound(AudioClip soundEffectClip)
    {
        SoundEffectAudioSource.PlayOneShot(soundEffectClip, SoundEffectVolume * Constants.SOUND_EFFECT_RATE);
    }
    public void PlayOverlapSound(AudioClip soundEffectClip)
    {
        if (currentSoundEffect != null && currentSoundEffect == soundEffectClip)
        {
            // 현재 사운드 이펙트가 이미 재생 중인 경우, 중첩 재생을 피하기 위해 종료합니다.
            return;
        }
        currentSoundEffect = soundEffectClip;
        SoundEffectAudioSource.PlayOneShot(soundEffectClip, SoundEffectVolume);

        StartCoroutine(ResetCurrentSoundEffect(soundEffectClip.length));
    }
    private IEnumerator ResetCurrentSoundEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentSoundEffect = null;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != mCurrentScene)
        {
            if (scene.name == "InGame")
            {
                // 이전 씬과 다른 BGM인 경우 새로운 BGM 재생
                StopBgm();
                PlayBgm(scene.name);
            }
            else if (scene.name == "GameResult")
            {
                StopBgm();
                PlayBgm(scene.name);
            }
            else
            {
                // 이전 씬과 동일한 BGM인 경우 이어서 재생
            }
        }
    }
    private void PlayBgm(string sceneName)
    {
        if (BgmAudioSource == null)
        {
            Debug.LogError("AudioSource 컴포넌트가 없습니다!");
            return;
        }

        AudioClip bgmClip;
        if (sceneName == "InGame")
        {
            // InGame 씬에 진입한 경우
            bgmClip = Bgm[(int)Enums.EBgm.Stage1]; // InGame 씬에 해당하는 BGM
        }
        else
        {
            // 나머지 씬에 진입한 경우
            bgmClip = Bgm[(int)Enums.EBgm.BGM]; // 나머지 씬에 해당하는 BGM
        }

        BgmAudioSource.clip = bgmClip;
        BgmAudioSource.volume = BgmVolume;
        BgmAudioSource.Play();
    }
    private void StopBgm()
    {
        BgmAudioSource.Stop();
    }
    #endregion
}