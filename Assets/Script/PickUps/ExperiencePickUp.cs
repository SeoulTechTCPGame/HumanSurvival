using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUp : MonoBehaviour,ICollectible
{
    public float expGranted;

    public void Collect()
    {
        //스크립트 명으로 오브젝트 찾기
        Character character = GameManager.instance.character;
        //Todo : character grouth stat 
        character.GetExp(expGranted);
        gameObject.SetActive(false);

        ;
    }
}
