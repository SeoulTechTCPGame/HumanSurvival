using UnityEngine;

public class Gralic : Weapon
{
    [SerializeField] AudioClip mFireClip;
    private GameObject mNewObj;
    private float mTimer = 0;
    private bool mbExist = false;

    private void Update()
    {
        transform.position = GameManager.instance.Player.transform.position + new Vector3(0, 0.5f, 0);
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
            if(!mbExist)
            {
                mNewObj = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                SoundManager.instance.PlayOverlapSound(mFireClip);
                mNewObj.transform.position = GameManager.instance.Player.transform.position + new Vector3(0, 0.5f, 0);
                mNewObj.transform.localScale = new Vector3(3.5f * WeaponTotalStats[((int)Enums.EWeaponStat.Area)], 3.5f * WeaponTotalStats[((int)Enums.EWeaponStat.Area)], 0);
                mbExist = true;
            }
            else
            {
                Destroy(mNewObj);
                mbExist = false;
            }
            mTimer = 0;
        }
    }
}