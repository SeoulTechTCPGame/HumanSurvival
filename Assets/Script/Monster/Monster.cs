using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int health = 100; //몬스터 체력
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
