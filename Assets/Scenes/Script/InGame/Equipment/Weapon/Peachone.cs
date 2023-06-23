using UnityEngine;

public class Peachone : Weapon
{
    public GameObject Bird = null;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    public Vector3 ProjectileScale;
    [SerializeField] Animator mAnimator;
    private bool mbHolding = false;
    private static Color[] mColors = { new Color(0.85f, 0.13f, 0.19f, 1), new Color(1, 0.46f, 0, 1), new Color(0.89f, 0.87f, 0.88f, 1)
            , new Color(0.51f, 0.93f, 0.17f, 1), new Color(0.13f, 0.89f, 0.51f, 1), new Color(0.13f, 0.75f, 1, 1), new Color(0.17f, 0.14f, 0.91f, 1), new Color(0.84f, 0.27f, 0.86f, 1) };
    private int mColorCnt = 0;
    private float mTimer = 100;
    private bool mbUsePeach = false;

    private void FixedUpdate()
    {
        if (!mbUsePeach || mbHolding)
        {
            return;
        }
        mTimer += Time.deltaTime;

        transform.position = CalculateBezierPoint();
        if (mTimer > 1.0f)
        {
            mbHolding = true;
            Destroy(gameObject, 1f);
            mAnimator.SetTrigger("Hold");
            GetComponent<CircleCollider2D>().enabled = true;
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
            if(WeaponIndex == 6 && BEvolution)
            {
                GameManager.instance.Character.RestoreHealth(1);
                GameManager.instance.EvoGralicRestoreCount++;
                if(GameManager.instance.EvoGralicRestoreCount == 60)
                {
                    GameManager.instance.EvoGralicRestoreCount = 0;
                    WeaponTotalStatList[((int)Enums.EWeaponStat.Might)] += 1;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boundary")
        {
            Destroy(this.gameObject);
        }
    }
    public override void Attack()
    {
        if (IsEvoluction())
            EvoSpawnCircle(SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex], SkillFiringSystem.instance.Circles[1]);
        else
            SpawnCircle(SkillFiringSystem.instance.weaponPrefabs[WeaponIndex], SkillFiringSystem.instance.Circles[0]);
    }
    public override void EvolutionProcess()
    {
        var equipManageSys = GameManager.instance.EquipManageSys;
        var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex]]];
        var bird = SkillFiringSystem.instance.Birds[2];
        pairWeapon.GetComponent<EbonyWings>().EvolutionProcess();

        Destroy(Bird, pairWeapon.WeaponTotalStats[(int)Enums.EWeaponStat.Cooldown] - mTimer);
        Destroy(pairWeapon.GetComponent<EbonyWings>().Bird, pairWeapon.WeaponTotalStats[(int)Enums.EWeaponStat.Cooldown] - mTimer);
        SpawnWhiteBird(bird, pairWeapon);
        StartPoint = Bird.GetComponent<Bird>().transform;
        pairWeapon.GetComponent<EbonyWings>().StartPoint = pairWeapon.GetComponent<EbonyWings>().Bird.GetComponent<Bird>().transform;
    }
    public void Fire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjPeachone = newObs.GetComponent<Peachone>();
        newObjPeachone.StartPoint = sourceP;
        newObjPeachone.ControlPoint = secondPoint;
        newObjPeachone.EndPoint = dstTransform;
        newObjPeachone.mbUsePeach = true;
        newObjPeachone.mTimer = 0;
        newObjPeachone.transform.localScale = ProjectileScale;
        newObs.transform.position = sourceP.position; //시작 위치
    }
    public void EvoFire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjPeachone = newObs.GetComponent<Peachone>();
        newObjPeachone.StartPoint = sourceP;
        newObjPeachone.ControlPoint = secondPoint;
        newObjPeachone.EndPoint = dstTransform;
        newObjPeachone.mbUsePeach = true;
        newObjPeachone.mTimer = 0;
        newObjPeachone.transform.localScale = ProjectileScale;
        newObs.GetComponent<TrailRenderer>().material.color = mColors[mColorCnt & 7];
        mColorCnt++;
        newObs.transform.position = sourceP.position; //시작 위치
    }
    public void SpawnCircle(GameObject peachPre, GameObject bounderyPre)
    {
        mTimer += Time.deltaTime;
        if (mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, true, this, StartPoint);
            mTimer = 0;
            ProjectileScale = transform.localScale * WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
            mColorCnt = 0;
        }
    }
    public void EvoSpawnCircle(GameObject peachPre, GameObject bounderyPre)
    {
        mTimer += Time.deltaTime;
        if (mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<EvoPeachBoundery>().CreateCircle(peachPre, bounderyPre, true, this, StartPoint);
            mTimer = 0;
            ProjectileScale = transform.localScale * WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
            mColorCnt = 0;
        }
    }
    public void SpawnBlueBird(GameObject bird)
    {
        Bird = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        var newObjBird = Bird.GetComponent<Bird>();
        newObjBird.PlayerTransform = GameManager.instance.Player.transform;
        StartPoint = newObjBird.transform;
    }
    private Vector3 CalculateBezierPoint()
    {
        float u = 1f - mTimer;
        float tt = mTimer * mTimer;
        float uu = u * u;
        Vector3 nowPos = uu * StartPoint.position;
        nowPos += 2f * u * mTimer * ControlPoint;
        nowPos += tt * EndPoint.position;

        return nowPos;
    }
    private void SpawnWhiteBird(GameObject bird, Weapon pairWeapon)
    {
        Bird = pairWeapon.GetComponent<EbonyWings>().Bird = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        Bird.GetComponent<Bird>().PlayerTransform = GameManager.instance.Player.transform;
    }
}