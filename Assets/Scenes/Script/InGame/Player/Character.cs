using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    //예시를 위해 값은 무작위로 넣음
    //ToDo: 스탯들을 변수를 리스트 형식으로 바꾸기 + GameMager에서 가져오기
    [SerializeField] HealthBar HpBar;
    private bool isDead;
    private float currentHp;
    private float maxHp;

    private float mExp;
    private int mMaxExp;

    void Start()
    {   
        mExp = 0;
        mMaxExp = 100;
        currentHp = GameManager.instance.characterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.Stat.MaxHealth];
        maxHp = GameManager.instance.characterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.Stat.MaxHealth];

    }
    public void RestoreHealth(float amount)
    {
        //if(currentHp< CharacterStats[(int)Enums.Stat.MaxHealth])
        if(currentHp<maxHp)
        { 
            currentHp += amount;
            if (currentHp > 100) currentHp = 100;
            HpBar.SetState(currentHp, maxHp);
        }
    }
    public void TakeDamage(float damage, int weaponIndex)
    {
        Debug.Log(currentHp);
        if (isDead == true) return;
        currentHp -= Time.deltaTime*damage*2;
        if (currentHp <= 0)
        {
            GameManager.instance.GameOverPanelUp();
            isDead = true;
        }
        HpBar.SetState(currentHp, maxHp);
    }

    public void TempLoad()
    {
        GameManager.instance.LevelUp();
    }

    public void GetExp(float exp)
    {
        // TODO: stat의 growth 적용하여 경험치 획득
        mExp += exp;
        GameManager.instance.exp = mExp;
        while (mExp >= mMaxExp)
        {
            mExp -= mMaxExp;
            mMaxExp += Constants.DeltaExp;
            GameManager.instance.maxExp = mMaxExp;
            GameManager.instance.LevelUp();
        }
        //Debug.Log("Exp:" + GameManager.instance.exp);
    }
}
