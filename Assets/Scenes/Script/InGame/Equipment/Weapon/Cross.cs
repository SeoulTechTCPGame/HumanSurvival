using UnityEngine;

public class Cross : Weapon
{
    [SerializeField] Animator mAnimator;
    [SerializeField] float mAcceleration = 2f;
    private float mCoolTimer = 0;
    private bool mbUsed = false;

    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!mbUsed) return;
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        float cooldown = WeaponTotalStats[(int)Enums.EWeaponStat.Cooldown];
        float speed = WeaponTotalStats[(int)Enums.EWeaponStat.ProjectileSpeed];
        float acceleration = mAcceleration * (1 + speed / 100);

        mCoolTimer += Time.deltaTime;
        if (mCoolTimer >= cooldown)
        {
            int numProjectiles = (int)WeaponTotalStats[(int)Enums.EWeaponStat.Amount];
            for (int i = 0; i < numProjectiles; i++)
            {
                // 무기 생성
                GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newObs.transform.position = GameManager.instance.Player.transform.position;
                Cross newCross = newObs.GetComponent<Cross>();
                newCross.mbUsed = true;
                // 방향 조절
                Vector3 direction = FindClosestEnemyDirection();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                newObs.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                // 무기 발사
                Rigidbody2D rb = newObs.GetComponent<Rigidbody2D>();
                rb.velocity = direction * (speed - acceleration * Time.deltaTime);
                Debug.Log("forward");
                if (rb.velocity.magnitude <= 0)
                {
                    rb.velocity = -direction * WeaponTotalStats[(int)Enums.EWeaponStat.ProjectileSpeed];
                    Debug.Log("reverse");
                }
            }
            mCoolTimer = 0;
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