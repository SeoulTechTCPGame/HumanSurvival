using UnityEngine;
using System;      // array의 IndexOf 함수
using System.Linq; // array의 Max, Min 함수 

public class MagicWand : Weapon
{
    [SerializeField] Animator mAnimator;
    [SerializeField] AudioClip mFireClip;
    private float mTimer = 0;
    private bool mbUseWand = false;
    private int mTouchLimit;
    private int mSpeedPower = 10;

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
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        mTimer += Time.deltaTime;
        if (mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                //무기 세팅
                GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                SoundManager.instance.PlayOverlapSound(mFireClip);
                newObs.transform.position = GameManager.instance.Player.transform.position;
                MagicWand newWand = newObs.GetComponent<MagicWand>();
                newWand.mbUseWand = true;
                newWand.mTouchLimit = (int)WeaponTotalStats[(int)Enums.EWeaponStat.Piercing];
                Vector3 direction = FindClosestEnemyDirection();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                //무기가 바라보는 방향 조절
                newObs.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);     //180은 이 스프라이트에 맞게 보정한 값
                //무기 발사
                Rigidbody2D rb = newObs.GetComponent<Rigidbody2D>();
                rb.velocity = direction * WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)] * mSpeedPower;
            }
            mTimer = 0;
        }
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
    private Vector3 FindClosestEnemyDirection()
    {
        Collider2D[] enemies = Physics2D.OverlapAreaAll(GameManager.instance.Player.transform.position + Vector3.left * 15 + Vector3.up * 8, GameManager.instance.Player.transform.position + Vector3.right * 15 + Vector3.down * 8, LayerMask.GetMask("Monster"));
        float[] distance = new float[enemies.Length];

        for(int i = 0; i < enemies.Length; i++)
        {
            distance[i] = Vector3.Distance(GameManager.instance.Player.transform.position, enemies[i].transform.position);
        }
        
        if (enemies.Length == 0)
        {
            Debug.Log("error");
            return Vector3.right;
        }

        Vector3 direction = enemies[Array.IndexOf(distance, distance.Min())].transform.position - GameManager.instance.Player.transform.position;
        return direction.normalized;
    }
}