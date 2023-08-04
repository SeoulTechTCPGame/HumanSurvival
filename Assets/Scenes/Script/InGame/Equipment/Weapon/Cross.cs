using UnityEngine;
using System;      // array의 IndexOf 함수
using System.Linq; // array의 Max, Min 함수 

public class Cross : Weapon
{
    [SerializeField] Animator mAnimator;
    [SerializeField] AudioClip mFireClip;
    private float mCoolTimer = 0;
    private bool mbUsed = false;
    private Vector3 mDirection;
    private int mSpeedPower = 7;
    private Vector3[] mCloestDirection;

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
            mCloestDirection = FindClosestEnemyDirection((int)WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]);
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
                Vector3 direction = mCloestDirection[i];
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
    private Vector3[] FindClosestEnemyDirection(int Amount)
    {
        Collider2D[] enemies = Physics2D.OverlapAreaAll(GameManager.instance.Player.transform.position + Vector3.left * 15 + Vector3.up * 8, GameManager.instance.Player.transform.position + Vector3.right * 15 + Vector3.down * 8, LayerMask.GetMask("Monster"));
        float[] distance = new float[enemies.Length];
        Vector3[] result = new Vector3[Amount];

        for (int i = 0; i < enemies.Length; i++)
        {
            distance[i] = Vector3.Distance(GameManager.instance.Player.transform.position, enemies[i].transform.position);
        }

        if (enemies.Length == 0)
        {
            Debug.Log("error");
            for (int i = 0; i < Amount; i++)
            {
                result[i] = Vector3.right;
            }
            return result;
        }

        for (int i = 0; i < Amount; i++)
        {
            Vector3 direction = enemies[Array.IndexOf(distance, distance.Min())].transform.position - GameManager.instance.Player.transform.position;
            result[i] = direction.normalized;
            distance[Array.IndexOf(distance, distance.Min())] = Mathf.Infinity;
        }

        return result;
    }
}