using UnityEngine;

public class Whip : Weapon
{
    private GameObject mNewObj;
    public float CriticalRate = 10;
    private float mTimer = 0;
    private bool mbUse = false;

    private void Update()
    {
        if (!mbUse) return;
        transform.parent.transform.position = GameManager.instance.Player.transform.position;
    }
    public override void Attack()
    {
        GameObject objPre = IsEvoluction() ? SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex] : SkillFiringSystem.instance.weaponPrefabs[WeaponIndex];
        mTimer += Time.deltaTime;
        if (mTimer > objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])   //ToDo: 보정값
        {
            mNewObj = new GameObject("Whips");
            mNewObj.transform.parent = GameObject.Find("SkillFiringSystem").transform;
            for (int i = 0; i < objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                // 무기 생성
                GameObject newObs = Instantiate(objPre, GameObject.Find("Whips").transform);
                newObs.transform.localScale *= objPre.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
                float power = newObs.GetComponent<Weapon>().WeaponTotalStats[((int)Enums.EWeaponStat.Might)];
                power = Random.Range(0, 100) < CriticalRate ? power : power * 2;
                Whip newWhip = newObs.GetComponent<Whip>();
                newWhip.mbUse = true;
                // 위치 조정, 뒤집기
                if ((GameManager.instance.Player.PreMovement.x >= 0 && i % 2 == 0) || (GameManager.instance.Player.PreMovement.x < 0 && i % 2 != 0))
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
}