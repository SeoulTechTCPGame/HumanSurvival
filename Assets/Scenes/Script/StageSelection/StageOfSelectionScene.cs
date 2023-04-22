using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageOfSelectionScene : MonoBehaviour
{
    //StageName, Time, DoubleSpeed, GoldCoinBonus, LuckBonus, ExperienceBonus
    private TextMeshProUGUI[] textMeshes;

    void Start()
    {
        textMeshes = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void LoadMapData(MapScriptableObject mapData)
    {
        textMeshes[0].SetText(mapData.StageName);
        textMeshes[1].SetText("Time: " + mapData.PlayTime);
        textMeshes[2].SetText("DoubleSpeed: " + mapData.DoubleSpeed);
        textMeshes[3].SetText("GoldCoinBonus: " + mapData.GoldCoinBonus);
        textMeshes[4].SetText("LuckBonus: " + mapData.LuckBonus);
        textMeshes[5].SetText("ExperienceBonus: " + mapData.ExperienceBonus);
    }
}
