using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public float HpRegenerationTimer;
    [SerializeField] HealthBar mHpBar;
    [SerializeField] float mCurrentHp;
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
    }
    private void Update()
    {
        // 장신구 레벨업시 스텟이 적용되려면 Start가 아니라 Update에서도 계속 값을 업데이트해야지 적용된다.
        mMaxHp = GameManager.instance.CharacterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.EStat.MaxHealth];
        mArmor = GameManager.instance.CharacterStats[(int)Enums.EStat.Armor];
        //체력 재생력
        HpRegenerationTimer += Time.deltaTime * GameManager.instance.CharacterStats[(int)Enums.EStat.Recovery];
        if (HpRegenerationTimer > 1f)
        {
            RestoreHealth(1);
            HpRegenerationTimer -= 1f;

        }
    }
    public void RestoreHealth(float amount)
    {
        if (mCurrentHp < mMaxHp)
        {
            amount = amount * (1 + GameManager.instance.CharacterStats[(int)Enums.EStat.Recovery]);
            mCurrentHp += amount;
            if (mCurrentHp > mMaxHp) mCurrentHp = mMaxHp;
            mHpBar.SetState(mCurrentHp, mMaxHp);
        }
    }
    public void TakeDamage(float damage, int weaponIndex)
    {
        if (mbDead == true) return;
        if (damage - mArmor <= 0)
        {
            mCurrentHp -= Time.deltaTime * 0 * 2;
        }
        else
        {
            mCurrentHp -= Time.deltaTime * (damage - mArmor) * 2;
        }
        if (mCurrentHp <= 0)
        {
            GameManager.instance.GameOverPanelUp();
            mbDead = true;
        }
        mHpBar.SetState(mCurrentHp, mMaxHp);
    }
    public void TempLoad()
    {
        GameManager.instance.LevelUp();
    }
    public void GetExp(float exp)
    {
        exp=exp*(1+GameManager.instance.CharacterStats[(int)Enums.EStat.Growth] /100);
        mExp += exp;
        GameManager.instance.Exp = mExp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += Constants.DELTA_EXP;
            GameManager.instance.MaxExp = mMaxExp;
            GameManager.instance.LevelUp();
        }
    }
}