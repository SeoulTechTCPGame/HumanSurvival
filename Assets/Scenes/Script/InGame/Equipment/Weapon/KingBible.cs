    using UnityEngine;

public class KingBible : Weapon
{
    [SerializeField] AudioClip mFireClip;
    private GameObject mNewObj;
    private bool mbExist = false;
    private float mDistance = 4f;  // 오브젝트와 타겟 사이의 거리
    private float mSpeed = 120f;  // 공전 속도
    private float mTimer = 0;

    private void Update()
    {
        transform.parent.transform.position = GameManager.instance.Player.transform.position;
        transform.parent.transform.Translate(Vector3.down);
        transform.parent.transform.Rotate(Vector3.back * mSpeed * WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)] * Time.deltaTime / WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]);

    }
    public override void Attack()
    {
        GameObject objPre;
        if (IsEvoluction())
        {
            objPre = SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex];
            mbExist = false;
        }
        else
            objPre = SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        if(!mbExist && mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            
            mNewObj = new GameObject("KingBibles");
            mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;

            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                GameObject kingBible = Instantiate(objPre);
                SoundManager.instance.PlayOverlapSound(mFireClip);
                kingBible.transform.parent = GameObject.Find("KingBibles").transform;

                Vector3 rotVec = Vector3.forward * 360 * i / WeaponTotalStats[((int)Enums.EWeaponStat.Amount)];
                //여러 개 동시 발사 시 시작 위치를 다르게 설정
                kingBible.transform.Rotate(rotVec);
                kingBible.transform.Translate(kingBible.transform.up * mDistance * WeaponTotalStats[((int)Enums.EWeaponStat.Area)], Space.World);
            }
            mbExist = true;
            mTimer = 0;
        }
        else if (mbExist && mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Duration)])
        {
            Destroy(mNewObj);
            mbExist = false;
            mTimer = 0;
        }
        mTimer += Time.deltaTime;
        
    }
}