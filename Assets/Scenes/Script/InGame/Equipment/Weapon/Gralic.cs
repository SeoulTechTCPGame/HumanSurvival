using UnityEngine;

public class Gralic : Weapon
{
    bool isExist = false;
    GameObject newobj;
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position = GameManager.instance.player.transform.position + new Vector3(0, 0.5f, 0);
    }

    public override void Attack()
    {
        GameObject objPre;
        if (isEvoluction())
            objPre = SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex];
        else
            objPre = SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        timer += Time.deltaTime;
        if (timer > WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            if(!isExist)
            {
                newobj = Instantiate(objPre);
                newobj.transform.parent = GameObject.Find("SkillFiringSystem").transform;
                newobj.transform.position = GameManager.instance.player.transform.position + new Vector3(0, 0.5f, 0);
                isExist = true;
            }
            else
            {
                Destroy(newobj);
                isExist = false;
            }
            timer = 0;
        }
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
}
