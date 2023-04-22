using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public CharacterType currentCharcter;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}