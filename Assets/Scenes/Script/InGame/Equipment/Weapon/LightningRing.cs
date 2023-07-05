using UnityEngine;

public class LightningRing : Weapon
{
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
                mNewObj = Instantiate(objPre);
                mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;
                mNewObj.transform.position = GameManager.instance.Player.transform.position + new Vector3(0, 0.5f, 0);
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
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
}