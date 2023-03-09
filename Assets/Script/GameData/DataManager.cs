using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterName
{
    Alchemist, Barbarian, Blademaster, Druid,FireMage, KnightHero, Necromancer
}
public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public CharacterName currentCharcter;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void  SetCharacterName(CharacterName c)
    {
        CharacterName currentCharcter=c;
        Debug.Log(currentCharcter);
    }
        
}

