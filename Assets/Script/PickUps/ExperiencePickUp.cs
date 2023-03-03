using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickUp : MonoBehaviour,ICollectible
{
    public int expGranted;

    public void Collect()
    {
        //스크립트 명으로 오브젝트 찾기
        Character character = FindObjectOfType<Character>();
        character.GetExp(expGranted);
;    }
}
