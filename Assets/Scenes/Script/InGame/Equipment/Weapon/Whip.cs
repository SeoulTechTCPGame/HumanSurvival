using UnityEngine;

public class Whip : Weapon
{
    [SerializeField] AudioClip mFireClip;
    private GameObject mNewObj;
    private float mTimer = 0;
    private bool mbUse = false;
    private bool mCharacterDirectionRight = true;

    private void Update()
    {
        if (!mbUse) return;
        transform.parent.transform.position = GameManager.instance.Player.transform.position;
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        mTimer += Time.deltaTime;
        if (GameManager.instance.Player.PreMovement.x > 0)
        {
            mCharacterDirectionRight = true;
        }
        else if (GameManager.instance.Player.PreMovement.x < 0)
        {
            mCharacterDirectionRight = false;
        }
        
        if (mTimer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            mNewObj = new GameObject("Whips");
            mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;
            for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                // 무기 생성
                GameObject newObs = Instantiate(objPre, GameObject.Find("Whips").transform);
                SoundManager.instance.PlayOverlapSound(mFireClip);
                newObs.transform.localScale *= objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
                Whip newWhip = newObs.GetComponent<Whip>();
                newWhip.mbUse = true;
                // 위치 조정, 뒤집기
                if ((mCharacterDirectionRight && i % 2 == 0) || (!mCharacterDirectionRight && i % 2 != 0))
                {
                    newObs.transform.Translate(new Vector3(2f, i + 0.5f, 0), Space.World);  //우측
                    newObs.GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    newObs.transform.Translate(new Vector3(-2f, i + 0.5f, 0), Space.World);  //좌측
                    newObs.GetComponent<SpriteRenderer>().flipX = true;
                }
                Destroy(mNewObj, 0.5f);
            }
            mTimer = 0;
        }
    }
}