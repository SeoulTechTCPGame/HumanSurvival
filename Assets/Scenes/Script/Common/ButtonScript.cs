using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private Button mButton;

    private void Start()
    {
        mButton = GetComponent<Button>();
        mButton.onClick.AddListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        SoundManager.instance.PlayButtonSound();
    }
}