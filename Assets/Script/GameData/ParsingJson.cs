using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsingJson : MonoBehaviour
{
    [Serializable]
    public class UserData
    {
        public int Gold;
    }

    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/UserData");
    }
}
