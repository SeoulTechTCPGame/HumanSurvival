using UnityEngine;

public class ExperiencePickUp : MonoBehaviour, ICollectible
{
    public float ExpGranted;
    [SerializeField] AudioClip mClip;

    public void Collect()
    {
        //스크립트 명으로 오브젝트 찾기
        Character character = GameManager.instance.Character;
        //Todo : character grouth stat 
        character.GetExp(ExpGranted);
        gameObject.SetActive(false);
        SoundManager.instance.PlayBuutonSoundTheOther(mClip);
    }
}