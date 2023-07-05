using UnityEngine;

public class Whip : Weapon
{
    private float mTimer = 0;
    private bool mbUse = false;

    void Update()
    {
        if (!mbUse) return;
    }
    private void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        mTimer += Time.deltaTime;
        if (mTimer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
            newObs.transform.position = GameManager.instance.Player.transform.position;
            Whip newWhip = newObs.GetComponent<Whip>();
            newWhip.mbUse = true;
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
    }
}