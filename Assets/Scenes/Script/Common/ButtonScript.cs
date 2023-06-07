using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        SoundManager.instance.PlayButtonSound();
    }
}