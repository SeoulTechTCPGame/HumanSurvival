using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] AudioClip mClip;
    [SerializeField] Toggle mToggle;
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
        } else if (mToggle != null)
        {
            SoundManager.instance.PlayButtonSound();
        }
        else
        {
            SoundManager.instance.PlaySoundEffect(mClip);
        }
    }
}