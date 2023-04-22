using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Map")]

public class MapScriptableObject : ScriptableObject
{
    public string StageName;
    public float PlayTime;
    public float DoubleSpeed;
    public float GoldCoinBonus;
    public float LuckBonus;
    public float ExperienceBonus;
    public string StageExplain;
}
