using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class CharacterInfoOfSelectionScene : MonoBehaviour
{
    //Name, MaxStamina, Recovery, Defense, Speed, Damage, ProjectileSpeed, Durationn, AttackRange, Cooldown, NumberOfProjectiles, Magnet, Luck, Growth
    private TextMeshProUGUI[] textMeshes;

    void Start()
    {
        textMeshes = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void LoadCharacterData(CharacterScriptableObject characterData)
    {
        textMeshes[0].SetText(characterData.name);
        textMeshes[1].SetText("Max stamina: " + characterData.MaxHealth);
        textMeshes[2].SetText("Recovery: " + characterData.Recovery);
        textMeshes[3].SetText("Defense: " + characterData.Armor);
        textMeshes[4].SetText("Speed: " + characterData.MoveSpeed);
        textMeshes[5].SetText("Damage: " + characterData.Might);
        textMeshes[6].SetText("Projectile Speed: " + characterData.ProjectileSpeed);
        textMeshes[7].SetText("Duration: " + characterData.Duration);
        textMeshes[8].SetText("Attack Range: " + characterData.Area);
        textMeshes[9].SetText("Cooldown: " + characterData.Cooldown);
        textMeshes[10].SetText("Number of projectiles: " + characterData.Amount);
        textMeshes[11].SetText("Magnet: " + characterData.MagnetBonus);
        textMeshes[12].SetText("Luck: " + characterData.Luck);
        textMeshes[13].SetText("Growth: " + characterData.Growth);
    }
}