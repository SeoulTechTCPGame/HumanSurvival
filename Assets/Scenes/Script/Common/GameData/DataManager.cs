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
        // Scene에 이미 인스턴스가 존재 하는지 확인 후 처리
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}