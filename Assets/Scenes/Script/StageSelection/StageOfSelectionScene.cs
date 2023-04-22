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
        textMeshes[1].SetText("게임 시간: " + mapData.PlayTime);
        textMeshes[2].SetText("배속: " + mapData.DoubleSpeed);
        textMeshes[3].SetText("골드 보너스: " + mapData.GoldCoinBonus);
        textMeshes[4].SetText("행운 보너스: " + mapData.LuckBonus);
        textMeshes[5].SetText("경험치 보너스: " + mapData.ExperienceBonus);
    }
}
