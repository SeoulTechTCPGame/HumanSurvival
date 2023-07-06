using UnityEngine;

public class Whip : Weapon
{
    public float CriticalRate = 10;
    private float mTimer = 0;
    private bool mbUse = false;
    private Vector3 _interval = new Vector3(5f, 0f, 0f);

    private void Update()
    {
        if (!mbUse) return;
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];

        mTimer += Time.deltaTime;
        if (mTimer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);
                newObs.transform.localScale *= objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
                float power = newObs.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Might)];
                power = Random.Range(0, 100) < CriticalRate ? power : power * 2;
                Whip newWhip = newObs.GetComponent<Whip>();
                newWhip.mbUse = true;
                if (CheckPosition(i, GameManager.instance.Player.PreMovement))
                {
                    newObs.transform.position = GameManager.instance.Player.transform.position + _interval + new Vector3(0f, 2f * i, 0f);
                }
                else
                {
                    newObs.transform.position = GameManager.instance.Player.transform.position - _interval + new Vector3(0f, 2f * i, 0f);
                }
                Destroy(newObs);
            }
        }
    }
    #region
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
            if (WeaponIndex == 0 && BEvolution)
            {
                GameManager.instance.Character.RestoreHealth(8);
            }
        }
    }
    #endregion
    private bool CheckPosition(int index, Vector2 preMovement)
    {
        if ((preMovement.x > 0 && index % 2 == 0) || (preMovement.x < 0 && index % 2 != 0))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}