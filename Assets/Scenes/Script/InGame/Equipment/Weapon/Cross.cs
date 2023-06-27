using UnityEngine;

public class Cross : Weapon
{
    [SerializeField] Animator mAnimator;
    private float mTimer = 0;
    private bool mbUseWand = false;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!mbUseWand) return;
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        float initialSpeed = WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)];
        float acceleration = (1 + WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)]/100);

        mTimer += Time.deltaTime;
        float cooldown = WeaponTotalStats[(int)Enums.EWeaponStat.Cooldown];
        if (mTimer > cooldown)
        {
            int numProjectiles = ((int)WeaponTotalStats[(int)Enums.EWeaponStat.Amount]);
            Vector3 initialDirection = FindClosestEnemyDirection();

            for (int i = 0; i < numProjectiles; i++)
            {
                //무기 세팅
                GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newObs.transform.position = GameManager.instance.Player.transform.position;
                Cross newWand = newObs.GetComponent<Cross>();
                newWand.mbUseWand = true;

                //무기 발사
                Rigidbody2D rb = newObs.GetComponent<Rigidbody2D>();
                rb.velocity = initialDirection * (initialSpeed - mTimer * acceleration); ;

                //float angleInDegrees = Mathf.Atan2(initialDirection.y, initialDirection.x) * Mathf.Rad2Deg;
                //newObs.transform.rotation = Quaternion.Euler(0f, 0f, angleInDegrees);

            }
            mTimer = 0;
        }
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

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
    }
    private Vector3 FindClosestEnemyDirection()
    {
        GameObject closestEnemy = FindClosestEnemy();
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
}