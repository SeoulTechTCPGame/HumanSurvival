using UnityEngine;
using System.Collections.Generic;

public class Character : MonoBehaviour, IDamageable
{
    public float HpRegenerationTimer;
    [SerializeField] HealthBar mHpBar;
    [SerializeField] float mCurrentHp;
    [SerializeField] AudioClip[] Clips;
    private bool mbDead;
    private float mMaxHp;
    private float mArmor;
    private float mExp;
    private int mMaxExp;
    private void Start()
    {
        mExp = 0;
        mMaxExp = 100;
        mCurrentHp = GameManager.instance.CharacterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.EStat.MaxHealth];
        mMaxHp = GameManager.instance.CharacterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.EStat.MaxHealth];
        mArmor = GameManager.instance.CharacterStats[(int)Enums.EStat.Armor];
        InvokeRepeating("RepeatRecovery", 1, 1);
    }
    private void Update()
    {
        // 장신구 레벨업시 스텟이 적용되려면 Start가 아니라 Update에서도 계속 값을 업데이트해야지 적용된다.
        mMaxHp = GameManager.instance.CharacterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.EStat.MaxHealth];
        mArmor = GameManager.instance.CharacterStats[(int)Enums.EStat.Armor];
    }

    public void RestoreHealth(float amount)
    {
        if (mCurrentHp < mMaxHp)
        {
            amount = amount * (1 + GameManager.instance.CharacterStats[(int)Enums.EStat.Recovery]);
            mCurrentHp += amount;
            if (mCurrentHp > mMaxHp)
            {
                GameManager.instance.RestoreCount += amount - (mCurrentHp - mMaxHp);
                mCurrentHp = mMaxHp;
            }
            else
            {
                GameManager.instance.RestoreCount += amount;
            }
            mHpBar.SetState(mCurrentHp, mMaxHp);
        }
    }
    public void TakeDamage(float damage, int weaponIndex)
    {
        if (mbDead == true) return; 
        SoundManager.instance.PlayOverlapSound(Clips[((int)Enums.ECharacterEffect.Attack)]);
        if (damage - mArmor> 0)
        {
            mCurrentHp -= Time.deltaTime * (damage - mArmor) * 2;
        }
        if (mCurrentHp <= 0)
        {
            if (GameManager.instance.CharacterStats[(int)Enums.EStat.Revival] > 0)
            {
                GameManager.instance.RevivalPanelUp();
            }
            else
            {
                GameManager.instance.GameOverPanelUp();
                mbDead = true;
            }
            
            SoundManager.instance.PlaySoundEffect(Clips[((int)Enums.ECharacterEffect.Die)]);
        }
        mHpBar.SetState(mCurrentHp, mMaxHp);
    }
    public void TempLoad()
    {
        GameManager.instance.LevelUp();
    }
    public void GetExp(float exp)
    {
        exp=exp*GameManager.instance.CharacterStats[(int)Enums.EStat.Growth];
        mExp += exp;
        GameManager.instance.Exp = mExp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += Constants.DELTA_EXP;
            GameManager.instance.MaxExp = mMaxExp;
            GameManager.instance.LevelUp();
            SoundManager.instance.PlaySoundEffect(Clips[((int)Enums.ECharacterEffect.LevelUp)]);
        }
    }
    public void RevivalHp()
    {
        mCurrentHp = mMaxHp / 2;
    }
    private void RepeatRecovery()
    {
        if (mCurrentHp < mMaxHp)
        {
             mCurrentHp +=GameManager.instance.CharacterStats[(int)Enums.EStat.Recovery];
            if (mCurrentHp > mMaxHp) mCurrentHp = mMaxHp;
            mHpBar.SetState(mCurrentHp, mMaxHp);
        }
    }
}