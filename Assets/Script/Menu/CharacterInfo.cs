using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CharacterInfo : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] int mCharacter;
    [SerializeField] Image mInfoImage;
    [SerializeField] Image mButtonIamge;
    [SerializeField] TMP_Text mCharacterName;
    [SerializeField] TMP_Text mCharacterName1;
    [SerializeField] TMP_Text mCharacterExplain;
    [SerializeField] TMP_Text mCharacterMaxStamina;
    [SerializeField] TMP_Text mCharacterRecovery;
    [SerializeField] TMP_Text mCharacterDefense;
    [SerializeField] TMP_Text mCharacterSpeed;
    [SerializeField] TMP_Text mCharacterDamage;
    [SerializeField] TMP_Text mCharacterProjectileSpeed;
    [SerializeField] TMP_Text mCharacterDuration;
    [SerializeField] TMP_Text mCharacterAttackRange;
    [SerializeField] TMP_Text mCharacterCooldown;
    [SerializeField] TMP_Text mCharacterNumberOfProjectiles;
    [SerializeField] TMP_Text mCharacterMagnet;
    [SerializeField] TMP_Text mCharacterLuck;
    [SerializeField] TMP_Text mCharacterGrowth;

    string mName;
    string mExplain;
    float mMaxStamina;
    float mRecovery;
    float mDefense;
    float mSpeed;
    float mDamage;
    float mProjectileSpeed;
    float mDuration;
    float mAttackRange;
    float mCooldown;
    int mNumberOfProjectiles;
    float mMagnet;
    float mLuck;
    float mGrowth;

    void Start()
    {
        switch (mCharacter)
        {
            case 1:
                this.mName = "캐릭터 이름1";
                this.mExplain = "캐릭터 설명1";
                this.mMaxStamina = 101;
                this.mRecovery = (float)0.1;
                this.mDefense = 1;
                this.mSpeed = 11;
                this.mDamage = 11;
                this.mProjectileSpeed = 11;
                this.mDuration = 11;
                this.mAttackRange = 11;
                this.mCooldown = -1;
                this.mNumberOfProjectiles = 1;
                this.mMagnet = 1;
                this.mLuck = 11;
                this.mGrowth = 11;
                break;

            case 2:
                this.mName = "캐릭터 이름2";
                this.mExplain = "캐릭터 설명2";
                this.mMaxStamina = 102;
                this.mRecovery = (float)0.2;
                this.mDefense = 2;
                this.mSpeed = 12;
                this.mDamage = 12;
                this.mProjectileSpeed = 12;
                this.mDuration = 12;
                this.mAttackRange = 12;
                this.mCooldown = -2;
                this.mNumberOfProjectiles = 2;
                this.mMagnet = 2;
                this.mLuck = 12;
                this.mGrowth = 12;
                break;

            case 3:
                this.mName = "캐릭터 이름3";
                this.mExplain = "캐릭터 설명3";
                this.mMaxStamina = 103;
                this.mRecovery = (float)0.3;
                this.mDefense = 3;
                this.mSpeed = 13;
                this.mDamage = 13;
                this.mProjectileSpeed = 13;
                this.mDuration = 13;
                this.mAttackRange = 13;
                this.mCooldown = -3;
                this.mNumberOfProjectiles = 3;
                this.mMagnet = 3;
                this.mLuck = 13;
                this.mGrowth = 13;
                break;

            case 4:
                this.mName = "캐릭터 이름4";
                this.mExplain = "캐릭터 설명4";
                this.mMaxStamina = 104;
                this.mRecovery = (float)0.4;
                this.mDefense = 4;
                this.mSpeed = 14;
                this.mDamage = 14;
                this.mProjectileSpeed = 14;
                this.mDuration = 14;
                this.mAttackRange = 14;
                this.mCooldown = -4;
                this.mNumberOfProjectiles = 4;
                this.mMagnet = 4;
                this.mLuck = 14;
                this.mGrowth = 14;
                break;

            case 5:
                this.mName = "캐릭터 이름5";
                this.mExplain = "캐릭터 설명5";
                this.mMaxStamina = 105;
                this.mRecovery = (float)0.5;
                this.mDefense = 5;
                this.mSpeed = 15;
                this.mDamage = 15;
                this.mProjectileSpeed = 15;
                this.mDuration = 15;
                this.mAttackRange = 15;
                this.mCooldown = -5;
                this.mNumberOfProjectiles = 5;
                this.mMagnet = 5;
                this.mLuck = 15;
                this.mGrowth = 15;
                break;

            case 6:
                this.mName = "캐릭터 이름6";
                this.mExplain = "캐릭터 설명6";
                this.mMaxStamina = 106;
                this.mRecovery = (float)0.6;
                this.mDefense = 6;
                this.mSpeed = 16;
                this.mDamage = 16;
                this.mProjectileSpeed = 16;
                this.mDuration = 16;
                this.mAttackRange = 16;
                this.mCooldown = -6;
                this.mNumberOfProjectiles = 6;
                this.mMagnet = 6;
                this.mLuck = 16;
                this.mGrowth = 16;
                break;

            case 7:
                this.mName = "캐릭터 이름7";
                this.mExplain = "캐릭터 설명7";
                this.mMaxStamina = 107;
                this.mRecovery = (float)0.7;
                this.mDefense = 7;
                this.mSpeed = 17;
                this.mDamage = 17;
                this.mProjectileSpeed = 17;
                this.mDuration = 17;
                this.mAttackRange = 17;
                this.mCooldown = -7;
                this.mNumberOfProjectiles = 7;
                this.mMagnet = 7;
                this.mLuck = 17;
                this.mGrowth = 17;
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //지정한 텍스트 대입
        mCharacterName.text = this.mName;
        mCharacterName1.text = this.mName;
        mCharacterExplain.text = this.mExplain;
        mCharacterMaxStamina.text = "Max stamina" + this.mMaxStamina.ToString();
        mCharacterRecovery.text = "Recovery" + this.mRecovery.ToString();
        mCharacterDefense.text = "Defense" + this.mDefense.ToString();
        mCharacterSpeed.text = "Speed" + this.mSpeed.ToString() + "%";
        mCharacterDamage.text = "Damage" + this.mDamage.ToString() + "%";
        mCharacterProjectileSpeed.text = "Projectile Speed" + this.mProjectileSpeed.ToString() + "%";
        mCharacterDuration.text = "Duration" + this.mDuration.ToString() + "%";
        mCharacterAttackRange.text = "AttackRange" + this.mAttackRange.ToString() + "%";
        mCharacterCooldown.text = "Cooldown" + this.mCooldown.ToString() + "%";
        mCharacterNumberOfProjectiles.text = "Number of projectiles" + this.mNumberOfProjectiles.ToString();
        mCharacterMagnet.text = "Magnet" + this.mMagnet.ToString() + "%";
        mCharacterLuck.text = "Luck" + this.mLuck.ToString() + "%";
        mCharacterGrowth.text = "Growth" + this.mGrowth.ToString() + "%";
        mInfoImage.GetComponent<Image>().sprite = mButtonIamge.GetComponent<Image>().sprite;  //이미지 대입
    }
}