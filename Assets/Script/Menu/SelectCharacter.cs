using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public CharacterName charname;
    public void OnClickCharacter()
    {
        DataManager.instance.currentCharcter=charname;
        Debug.Log(DataManager.instance.currentCharcter);

    }
}
