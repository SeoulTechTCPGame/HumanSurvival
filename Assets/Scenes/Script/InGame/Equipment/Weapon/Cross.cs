using UnityEngine;

public class Cross : Weapon
{
    [SerializeField] Animator mAnimator;
    [SerializeField] AudioClip mFireClip;
    private float mCoolTimer = 0;
    private bool mbUsed = false;
    private Vector3 mDirection;
    private int mSpeedPower = 7;
    
    private void Start()
    {
        mAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!mbUsed) return;

        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        rb.AddForce(-mDirection * mSpeedPower, ForceMode2D.Force);

        if (rb.velocity.magnitude <= 0.001)
        {
            transform.Rotate(0f, 0f, 180f);
        }
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        float cooldown = WeaponTotalStats[(int)Enums.EWeaponStat.Cooldown];
        float speed = WeaponTotalStats[(int)Enums.EWeaponStat.ProjectileSpeed];

        mCoolTimer += Time.deltaTime;
        if (mCoolTimer >= cooldown)
        {
            int numProjectiles = (int)WeaponTotalStats[(int)Enums.EWeaponStat.Amount];
            for (int i = 0; i < numProjectiles; i++)
            {
                // 무기 생성
                GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                SoundManager.instance.PlayOverlapSound(mFireClip);
                newObs.transform.position = GameManager.instance.Player.transform.position;
                Cross newCross = newObs.GetComponent<Cross>();
                newCross.mbUsed = true;
                // 무기 방향
                Vector3 direction = FindClosestEnemyDirection();
                newCross.mDirection = direction;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                newObs.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                // 무기 발사
                Rigidbody2D rb = newObs.GetComponent<Rigidbody2D>();
                rb.velocity = direction * speed * mSpeedPower;    //ToDo: 보정값
            }
            mCoolTimer = 0;
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

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        return closestEnemy;
    }
}