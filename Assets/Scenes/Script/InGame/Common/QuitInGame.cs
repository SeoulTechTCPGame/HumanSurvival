using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitInGame : MonoBehaviour
{
    public void DestroyGM()
    {
        Destroy(GameObject.Find("GameManager")); 
    }
}
