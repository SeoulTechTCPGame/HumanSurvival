using UnityEngine;

public class Knife : Weapon
{
    private float mTimer = 0;
    private bool mbUseKnife = false;
    private int mTouch = 0;
    private int mTouchLimit;
    private void Update()
    {
        if (!mbUseKnife) return;  //knife 사용 안할 때 Update를 안 함
        if(mTouchLimit <= mTouch)
        {
            Destroy(this.gameObject);
        }
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
            for (int i = 0; i < WeaponTotalStats[((int)Enums.EWeaponStat.Amount)]; i++)
            {
                GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
                Vector3 spawnPosition = GameManager.instance.Player.transform.position;
                if (i > 1) //여러 개 동시 발사 시 시작 위치를 다르게 설정
                {
                    spawnPosition.y -= ((int)WeaponTotalStats[((int)Enums.EWeaponStat.Area)] * (i-1)) / 2;
                    spawnPosition.y += i * (int)WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
                }
                newobs.transform.position = spawnPosition; //시작 위치
                newobs.GetComponent<Knife>().mbUseKnife = true;
                newobs.GetComponent<Knife>().mTouchLimit = (int)WeaponTotalStats[(int)Enums.EWeaponStat.Piercing];
                Rigidbody2D rb = newobs.GetComponent<Rigidbody2D>();
                rb.velocity = setDirection(newobs) * WeaponTotalStats[(int)Enums.EWeaponStat.ProjectileSpeed];
            }
            mTimer = 0;
        }
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
        if (col.gameObject.tag == "Monster")
        {
            mTouch++;
        }
    }
    private Vector3 setDirection(GameObject obj) {
        //칼날 방향
        if (GameManager.instance.Player.Movement.x > 0)
        {
            obj.GetComponent<SpriteRenderer>().flipY = true;
            if (GameManager.instance.Player.Movement.y > 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 135); }
            else if (GameManager.instance.Player.Movement.y < 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 45); }
            else { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90); }
        }
        else if (GameManager.instance.Player.Movement.x < 0)
        {
            obj.GetComponent<SpriteRenderer>().flipY = false;
            if (GameManager.instance.Player.Movement.y > 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 45); }
            else if (GameManager.instance.Player.Movement.y < 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 135); }
            else { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90); }
        }
        else
        {
            if (GameManager.instance.Player.Movement.y > 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0); }
            else if (GameManager.instance.Player.Movement.y < 0) { obj.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180); }
            else { obj.GetComponent<Transform>().rotation = Quaternion.FromToRotation(Vector3.up, GameManager.instance.Player.PreMovement); }
        }
        //이동 방향을 가져옴
        if (GameManager.instance.Player.PreMovement == Vector2.zero){ obj.GetComponent<SpriteRenderer>().flipY = true; return Vector3.right;}
        else if (GameManager.instance.Player.Movement == Vector2.zero & GameManager.instance.Player.PreMovement != Vector2.zero) { return GameManager.instance.Player.PreMovement;}
        else { return GameManager.instance.Player.Movement; }
    }
    public override void EvolutionProcess() // 무기 진화시 한 번 호출됨
    {

    }
}
