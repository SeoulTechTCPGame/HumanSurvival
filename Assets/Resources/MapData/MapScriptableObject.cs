using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/Map")]

public class MapScriptableObject : ScriptableObject
{
    public string StageNameEN;
    public string StageNameKR;
    public float PlayTime;
    public float DoubleSpeed;
    public float GoldCoinBonus;
    public float LuckBonus;
    public float ExperienceBonus;
    public string StageExplainEN;
    public string StageExplainKR;
}
