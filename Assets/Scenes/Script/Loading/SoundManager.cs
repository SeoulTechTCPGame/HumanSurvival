using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public float BgmVolume = 1.0f; // BGM 볼륨
    public float SoundEffectVolume = 1.0f; // 사운드 이펙트 볼륨
    public AudioClip ButtonSoundClip; // 버튼 소리 파일
    private AudioSource mAudioSource; // 소리를 재생할 오디오 소스

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
    public void PlayBGM(AudioClip bgmClip)
    {
        mAudioSource.clip = bgmClip;
        mAudioSource.volume = BgmVolume;
        mAudioSource.loop = true;
        mAudioSource.Play();
    }
    public void StopBGM()
    {
        mAudioSource.Stop();
    }
    public void PlaySoundEffect(AudioClip soundEffectClip)
    {
        mAudioSource.PlayOneShot(soundEffectClip, SoundEffectVolume);
    }
    public void PlayButtonSound()
    {
        mAudioSource.PlayOneShot(ButtonSoundClip);
    }
    public void EnableVFX(bool value)
    {
        if (value)
        {
            // Enable VFX
        }
        else
        {
            // Disable VFX
        }
    }
    public void EnableDamageDisplay(bool value)
    {
        if (value)
        {
            // Enable showing damage
        }
        else
        {
            // Disable showing damage
        }
    }
    public void HideStage(bool value)
    {
        if (value)
        {
            // Hide the stage
        }
        else
        {
            // Show the stage
        }
    }
}