using UnityEngine;

public class Whip : Weapon
{
    public float CriticalRate = 10;
    private float mTimer = 0;
    private bool mbUse = false;

    private void Update()
    {
        if (!mbUse) return;
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        mTimer += Time.deltaTime;
        if (mTimer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])   //ToDo: 보정값
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
                    newObs.transform.position = GameManager.instance.Player.transform.position + new Vector3(2f, i + 0.5f, 0f);   //우측
                }
                else
                {
                    newObs.transform.position = GameManager.instance.Player.transform.position + new Vector3(-2f, i + 0.5f, 0f);  //좌측
                }
                if (newObs.transform.position.x < GameManager.instance.Player.transform.position.x)
                {
                    newObs.GetComponent<SpriteRenderer>().flipX = true;
                }
                else 
                {
                    newObs.GetComponent<SpriteRenderer>().flipX = false;
                }
                Destroy(newObs, 0.5f);
            }
            mTimer = 0;
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