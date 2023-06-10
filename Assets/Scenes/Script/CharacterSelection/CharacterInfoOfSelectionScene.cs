using UnityEngine;
using TMPro;

public class CharacterInfoOfSelectionScene : MonoBehaviour
{
    //Name, MaxStamina, Recovery, Defense, Speed, Damage, ProjectileSpeed, Durationn, AttackRange, Cooldown, NumberOfProjectiles, Magnet, Luck, Growth
    private TextMeshProUGUI[] mTextMeshes;
    [SerializeField] TMP_Text mMoneyText;

    private void Start()
    {
        SetMoneyText();
        mTextMeshes = GetComponentsInChildren<TextMeshProUGUI>();
    }
    public void LoadCharacterData(CharacterScriptableObject characterData)
    {
        mTextMeshes[0].SetText(characterData.CharacterType.ToString());
        mTextMeshes[1].SetText("체력: " + characterData.MaxHealth);
        mTextMeshes[2].SetText("회복량: " + characterData.Recovery);
        mTextMeshes[3].SetText("방어력: " + characterData.Armor);
        mTextMeshes[4].SetText("이동속도: " + characterData.MoveSpeed);
        mTextMeshes[5].SetText("공격력: " + characterData.Might);
        mTextMeshes[6].SetText("투사체 속도: " + characterData.ProjectileSpeed);
        mTextMeshes[7].SetText("지속시간: " + characterData.Duration);
        mTextMeshes[8].SetText("공격 범위: " + characterData.Area);
        mTextMeshes[9].SetText("쿨타임: " + characterData.Cooldown);
        mTextMeshes[10].SetText("투사체 수: " + characterData.Amount);
        mTextMeshes[11].SetText("자석: " + characterData.MagnetBonus);
        mTextMeshes[12].SetText("행운: " + characterData.Luck);
        mTextMeshes[13].SetText("성장: " + characterData.Growth);
    }

    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}