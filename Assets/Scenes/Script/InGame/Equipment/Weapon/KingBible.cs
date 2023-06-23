using UnityEngine;

public class KingBible : Weapon
{
    private GameObject mNewObj;
    private bool mbExist = false;
    private float mDistance = 4f;  // 오브젝트와 타겟 사이의 거리
    private float mSpeed = 120f;  // 공전 속도
    private float mTimer = 0;
    private Vector3 pos;

    private void Update()
    {
        transform.RotateAround(GameManager.instance.Player.transform.position, Vector3.back, mSpeed * WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)] * Time.deltaTime);
    }
    private Vector3 getStartPosition(Vector3 pos)
    {
        return pos + Vector3.up * mDistance;
    }
    public override void Attack()
    {
        GameObject objPre;
        if (IsEvoluction())
            objPre = SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex];
        else
            objPre = SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        if(!mbExist && mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            // for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            // {
            //     mNewObj = Instantiate(objPre);
            //     mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;

            //     Vector3 spawnPosition = getStartPosition(GameManager.instance.Player.transform.position);
            //     //여러 개 동시 발사 시 시작 위치를 다르게 설정
            //     mNewObj.transform.position = spawnPosition; //시작 위치
            // }

            mNewObj = Instantiate(objPre);
            mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;

            Vector3 spawnPosition = getStartPosition(GameManager.instance.Player.transform.position);
            //여러 개 동시 발사 시 시작 위치를 다르게 설정
            mNewObj.transform.position = spawnPosition; //시작 위치
            mbExist = true;
            mTimer = 0;
        }
        else
        {
            if (mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Duration)])
            {
                Destroy(mNewObj);
                mbExist = false;
                mTimer = 0;
            }
        }
        mTimer += Time.deltaTime;
        
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
}