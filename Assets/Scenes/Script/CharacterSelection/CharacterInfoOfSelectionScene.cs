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
        mRecovery.SetText(characterData.Recovery.ToString());
        mDefense.SetText(characterData.Armor.ToString());
        mSpeed.SetText(characterData.MoveSpeed.ToString());
        mDamage.SetText(characterData.Might.ToString());
        mProjectileSpeed.SetText(characterData.ProjectileSpeed.ToString());
        mDurationn.SetText(characterData.Duration.ToString());
        mAttackRange.SetText(characterData.Area.ToString());
        mCooldown.SetText(characterData.Cooldown.ToString());
        mNumberOfProjectiles.SetText(characterData.Amount.ToString());
        mMagnet.SetText(characterData.MagnetBonus.ToString());
        mLuck.SetText(characterData.Luck.ToString());
        mGrowth.SetText(characterData.Growth.ToString());
    }
    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }
}