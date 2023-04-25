using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class CharacterInfoOfSelectionScene : MonoBehaviour
{
    //Name, MaxStamina, Recovery, Defense, Speed, Damage, ProjectileSpeed, Durationn, AttackRange, Cooldown, NumberOfProjectiles, Magnet, Luck, Growth
    private TextMeshProUGUI[] textMeshes;
    [SerializeField] TMP_Text mMoneyText;

    void Start()
    {
        SetMoneyText();
        textMeshes = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void LoadCharacterData(CharacterScriptableObject characterData)
    {
        textMeshes[0].SetText(characterData.characterType.ToString());
        textMeshes[1].SetText("체력: " + characterData.MaxHealth);
        textMeshes[2].SetText("회복량: " + characterData.Recovery);
        textMeshes[3].SetText("방어력: " + characterData.Armor);
        textMeshes[4].SetText("이동속도: " + characterData.MoveSpeed);
        textMeshes[5].SetText("공격력: " + characterData.Might);
        textMeshes[6].SetText("투사체 속도: " + characterData.ProjectileSpeed);
        textMeshes[7].SetText("지속시간: " + characterData.Duration);
        textMeshes[8].SetText("공격 범위: " + characterData.Area);
        textMeshes[9].SetText("쿨타임: " + characterData.Cooldown);
        textMeshes[10].SetText("투사체 수: " + characterData.Amount);
        textMeshes[11].SetText("자석: " + characterData.MagnetBonus);
        textMeshes[12].SetText("행운: " + characterData.Luck);
        textMeshes[13].SetText("성장: " + characterData.Growth);
    }

    void SetMoneyText(){
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}