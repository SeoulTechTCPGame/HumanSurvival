using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float BgmVolume = 1.0f; // BGM 볼륨
    public float SoundEffectVolume = 1.0f; // 사운드 이펙트 볼륨
    public AudioClip ButtonSoundClip; // 버튼 소리 파일
    public AudioClip[] Bgm;

    private AudioSource mAudioSource; // 소리를 재생할 오디오 소스
    private string mCurrentScene; // 현재 씬의 이름을 저장할 변수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        mAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        mCurrentScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
        PlayBgm(mCurrentScene);
    }
    #region
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
        if (mAudioSource == null)
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

        mAudioSource.clip = bgmClip;
        mAudioSource.volume = BgmVolume;
        mAudioSource.Play();
    }
    private void StopBgm()
    {
        mAudioSource.Stop();
    }
    public void PlaySoundEffect(AudioClip soundEffectClip)
    {
        mAudioSource.PlayOneShot(soundEffectClip, SoundEffectVolume);
    }
    public void PlayButtonSound()
    {
        mAudioSource.PlayOneShot(ButtonSoundClip, SoundEffectVolume);
    }
    public void PlayBuutonSoundTheOther(AudioClip soundEffectClip)
    {
        mAudioSource.PlayOneShot(soundEffectClip, SoundEffectVolume);
    }
    #endregion
    public void EnableVFX(bool value)
    {
        // VFX 활성화 또는 비활성화 처리
    }
    public void EnableDamageDisplay(bool value)
    {
        // 데미지 표시 활성화 또는 비활성화 처리
    }
    public void HideStage(bool value)
    {
        // 스테이지 숨기기 또는 표시 처리
    }
}