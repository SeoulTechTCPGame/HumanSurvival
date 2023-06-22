using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWand : Weapon
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
        if (!mbUseWand) return;
        if (mTouchLimit <= mTouch)
        {
            mAnimator.SetBool("Hit", true);
            Destroy(this.gameObject);
        }
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        mTimer += Time.deltaTime;
        float cooldown = WeaponTotalStats[(int)Enums.EWeaponStat.Cooldown];
        if (mTimer > cooldown)
        {
            int numProjectiles = ((int)WeaponTotalStats[(int)Enums.EWeaponStat.Amount]);
            Vector3 initialDirection = new Vector3(0f, 1f, 0f);

            for (int i = 0; i < numProjectiles; i++)
            {
                //방향 계산
                float randomAngle = Random.Range(0f, 360f);
                Quaternion rotation = Quaternion.Euler(0f, 0f, randomAngle);
                Vector3 direction = rotation * initialDirection;
                direction.Normalize();

                //무기 세팅
                GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newObs.transform.position = GameManager.instance.Player.transform.position;
                FireWand newWand = newObs.GetComponent<FireWand>();
                newWand.mbUseWand = true;
                newWand.mTouchLimit = (int)WeaponTotalStats[(int)Enums.EWeaponStat.Piercing];
                
                //무기 발사
                Rigidbody2D rb = newObs.GetComponent<Rigidbody2D>();
                rb.velocity = direction * WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)];
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
        if (col.gameObject.tag == "Monster")
        {
            mTouch++;
        }
    }
}