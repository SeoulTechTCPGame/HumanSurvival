using UnityEngine;
using System.Collections.Generic;

public class QuitInGame : MonoBehaviour
{
    [SerializeField] AudioClip mClip;
    public void DestroyGM()
    {
        GameObject chest;
        chest = GameObject.Find("chest");
        Destroy(chest);
        Destroy(GameObject.Find("GameManager"));
        SoundManager.instance.ChangeBGM(mClip);
    }
}