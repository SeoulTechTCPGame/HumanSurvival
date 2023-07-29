using UnityEngine;
using TMPro;

public class CharacterInfoOfSelectionScene : MonoBehaviour
{
    [SerializeField] TMP_Text mMoneyText;
    [SerializeField] TMP_Text mName;
    [SerializeField] TMP_Text mMaxStamina;
    [SerializeField] TMP_Text mRecovery;
    [SerializeField] TMP_Text mDefense;
    [SerializeField] TMP_Text mSpeed;
    [SerializeField] TMP_Text mDamage;
    [SerializeField] TMP_Text mProjectileSpeed;
    [SerializeField] TMP_Text mDurationn;
    [SerializeField] TMP_Text mAttackRange;
    [SerializeField] TMP_Text mCooldown;
    [SerializeField] TMP_Text mNumberOfProjectiles;
    [SerializeField] TMP_Text mMagnet;
    [SerializeField] TMP_Text mLuck;
    [SerializeField] TMP_Text mGrowth;

    private void Start()
    {
        SetMoneyText();
    }
    public void LoadCharacterData(CharacterScriptableObject characterData)
    {
        mName.SetText(characterData.CharacterType.ToString());
        mMaxStamina.SetText(characterData.MaxHealth.ToString());
        mRecovery.SetText(characterData.Recovery == 0 ? "-" : "+" + characterData.Recovery.ToString());
        mDefense.SetText(characterData.Armor == 0 ? "-" : "+" + characterData.Armor.ToString());
        mSpeed.SetText(characterData.MoveSpeed == 1 ? "-" : "+" + ((characterData.MoveSpeed - 1) * 100).ToString() + "%");
        mDamage.SetText(characterData.Might == 1 ? "-" : "+" + ((characterData.Might - 1) * 100).ToString() + "%");
        mProjectileSpeed.SetText(characterData.ProjectileSpeed == 1 ? "-" : "+" + ((characterData.ProjectileSpeed - 1) * 100).ToString() + "%");
        mDurationn.SetText(characterData.Duration == 1 ? "-" : "+" + ((characterData.Duration - 1) * 100).ToString() + "%");
        mAttackRange.SetText(characterData.Area == 1 ? "-" : ((characterData.Area - 1) < 1 ? "+" + ((characterData.Area - 1) * 100).ToString() + "%" : "+" + (characterData.Area * 100).ToString() + "%"));
        mCooldown.SetText(characterData.Cooldown == 1 ? "-" : "-" + ((1 - characterData.Cooldown) * 100).ToString() + "%");
        mNumberOfProjectiles.SetText(characterData.Amount == 0 ? "-" : "+" + characterData.Amount.ToString());
        mMagnet.SetText(characterData.MagnetBonus == 1 ? "-" : "+" + ((characterData.MagnetBonus - 1) * 100).ToString() + "%");
        mLuck.SetText(characterData.Luck == 1 ? "-" : "+" + ((characterData.Luck - 1) * 100).ToString() + "%");
        mGrowth.SetText(characterData.Growth == 1 ? "-" : "+" + ((characterData.Growth - 1) * 100).ToString() + "%");
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}