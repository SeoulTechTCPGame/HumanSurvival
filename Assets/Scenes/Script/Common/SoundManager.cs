using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // 볼륨 값 변수들
    public float bgmVolume = 1.0f; // BGM 볼륨
    public float soundEffectVolume = 1.0f; // 사운드 이펙트 볼륨

    public AudioClip buttonSoundClip; // 버튼 소리 파일
    private AudioSource audioSource; // 소리를 재생할 오디오 소스

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
        // 오디오 소스 설정
        audioSource = GetComponent<AudioSource>();
    }

    // BGM 재생
    public void PlayBGM(AudioClip bgmClip)
    {
        // 마스터 볼륨과 BGM 볼륨 곱 연산 적용
        float volume = bgmVolume;
    }

    // 사운드 이펙트 재생
    public void PlaySoundEffect(AudioClip soundEffectClip)
    {
        // 마스터 볼륨과 사운드 이펙트 볼륨 곱 연산 적용
        float volume = soundEffectVolume;
    }
    //버튼 클릭 사운드 재생
    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSoundClip);
    }
}