using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] AudioClip mClip;
    private Button mButton;

    private void Start()
    {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(PlayButtonSound);
    }
    private void PlayButtonSound()
    {
        if (mClip == null)
        {
            SoundManager.instance.PlayButtonSound();
        }
        else
        {
            SoundManager.instance.PlayBuutonSoundTheOther(mClip);
        }
    }
}