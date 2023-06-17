using UnityEngine;
public class MagicWand : Weapon
{
    [SerializeField] Animator mAnimator;

    private float mTimer = 0;
    private bool mbUseWand = false;
    private int mTouch = 0;
    private int mTouchLimit;
    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!mbUseWand) return;  //MagicWand 사용 안할 때 Update를 안 함
        if (mTouchLimit <= mTouch)
        {
            mAnimator.SetBool("Hit", true);
            Destroy(this.gameObject);
        }
    }
    public override void Attack()
    {
        GameObject objPre;
        if (IsEvoluction()) 
            objPre = SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex];
        else
            objPre = SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        mTimer += Time.deltaTime;
        if (mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                //무기 세팅
                GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newobs.transform.position = GameManager.instance.Player.transform.position;
                MagicWand newWand = newobs.GetComponent<MagicWand>();
                newWand.mbUseWand = true;
                newWand.mTouchLimit = (int)WeaponTotalStats[(int)Enums.EWeaponStat.Piercing];
                Vector3 direction = FindClosestEnemyDirection();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //무기가 바라보는 방향 조절
                newobs.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);     //180은 이 스프라이트에 맞게 보정한 값
                //무기 발사
                Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
                rb.velocity = direction * WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)];
            }
            mTimer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DestructibleObj"))
        {
            if (col.gameObject.TryGetComponent(out DestructibleObject destructible))
            {
                destructible.TakeDamage(WeaponTotalStatList[(int)Enums.EWeaponStat.Might], WeaponIndex);
            }
        }
        if (col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(WeaponTotalStatList[(int)Enums.EWeaponStat.Might], WeaponIndex);
            if (WeaponIndex == 6 && BEvolution)
            {
                GameManager.instance.Character.RestoreHealth(1);
                GameManager.instance.EvoGralicRestoreCount++;
                if (GameManager.instance.EvoGralicRestoreCount == 60)
                {
                    GameManager.instance.EvoGralicRestoreCount = 0;
                    WeaponTotalStatList[((int)Enums.EWeaponStat.Might)] += 1;
                }
            }
        }
        if (col.gameObject.tag == "Monster")
        {
            mTouch++;
        }
    }
    private GameObject FindClosestEnemy() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        //적 탐색
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance) //소환된 무기와 몬스터 가장 짧은 거리 업데이트
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
    public Vector3 FindClosestEnemyDirection()
    {
        GameObject closestEnemy = FindClosestEnemy();
        Debug.Log(closestEnemy);
        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            return direction.normalized;
        }
        else
        {
            return Vector3.right;
        }
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
}