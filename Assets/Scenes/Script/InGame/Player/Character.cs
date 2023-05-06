using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    //예시를 위해 값은 무작위로 넣음
    //ToDo: 스탯들을 변수를 리스트 형식으로 바꾸기 + GameMager에서 가져오기
    [SerializeField] HealthBar HpBar;
    private bool isDead;
    private float currentHp;
    private float maxHp;
    private float armor;

    private float mExp;
    private int mMaxExp;

    public float hpRegenerationTimer;

    void Start()
    {
        mExp = 0;
        mMaxExp = 100;
        currentHp = GameManager.instance.characterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.Stat.MaxHealth];
        maxHp = GameManager.instance.characterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.Stat.MaxHealth];
        armor = GameManager.instance.CharacterStats[(int)Enums.Stat.Armor];
    }
    private void Update()
    {
        // 장신구 레벨업시 스텟이 적용되려면 Start가 아니라 Update에서도 계속 값을 업데이트해야지 적용된다.
        maxHp = GameManager.instance.characterData.MaxHealth * GameManager.instance.CharacterStats[(int)Enums.Stat.MaxHealth];
        armor = GameManager.instance.CharacterStats[(int)Enums.Stat.Armor];
        //체력 재생력
        hpRegenerationTimer += Time.deltaTime * GameManager.instance.CharacterStats[(int)Enums.Stat.Recovery];
        if (hpRegenerationTimer > 1f)
        {
            RestoreHealth(1);
            hpRegenerationTimer -= 1f;

        }
    }
    public void RestoreHealth(float amount)
    {
        if (currentHp < maxHp)
        {
            currentHp += amount;
            if (currentHp > maxHp) currentHp = maxHp;
            HpBar.SetState(currentHp, maxHp);
        }
    }
    public void TakeDamage(float damage, int weaponIndex)
    {
        Debug.Log(currentHp);
        Debug.Log(damage);
        Debug.Log(armor);
        if (isDead == true) return;
        if (damage - armor <= 0)
        {
            currentHp -= Time.deltaTime * 0 * 2;
        }
        else
        {
            currentHp -= Time.deltaTime * (damage - armor) * 2;
        }
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
