using UnityEngine;

public class QuitInGame : MonoBehaviour
{
    [SerializeField] AudioClip mClip;
    public void DestroyGM()
    {
        Destroy(GameObject.Find("GameManager"));
        SoundManager.instance.ChangeBGM(mClip);
    }
}