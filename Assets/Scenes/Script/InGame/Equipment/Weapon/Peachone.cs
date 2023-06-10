using UnityEngine;

public class Peachone : Weapon
{
    [SerializeField] Animator animator;
    public GameObject Bird = null;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    public Vector3 ProjectileScale;
    private bool mbHolding = false;
    private static Color[] mColors = { new Color(0.85f, 0.13f, 0.19f, 1), new Color(1, 0.46f, 0, 1), new Color(0.89f, 0.87f, 0.88f, 1)
            , new Color(0.51f, 0.93f, 0.17f, 1), new Color(0.13f, 0.89f, 0.51f, 1), new Color(0.13f, 0.75f, 1, 1), new Color(0.17f, 0.14f, 0.91f, 1), new Color(0.84f, 0.27f, 0.86f, 1) };
    private int mColorCnt = 0;
    float Timer = 100;
    bool UsePeach = false;
    private void FixedUpdate()
    {
        if (!UsePeach || mbHolding)
        {
            return;
        }
        Timer += Time.deltaTime;

        transform.position = calculateBezierPoint();
        if (Timer > 1.0f)
        {
            mbHolding = true;
            Destroy(gameObject, 1f);
            animator.SetTrigger("Hold");
            GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    public void Fire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjPeachone = newobs.GetComponent<Peachone>();
        newObjPeachone.StartPoint = sourceP;
        newObjPeachone.ControlPoint = secondPoint;
        newObjPeachone.EndPoint = dstTransform;
        newObjPeachone.UsePeach = true;
        newObjPeachone.Timer = 0;
        newObjPeachone.transform.localScale = ProjectileScale;
        newobs.transform.position = sourceP.position; //시작 위치
    }
    public void EvoFire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjPeachone = newobs.GetComponent<Peachone>();
        newObjPeachone.StartPoint = sourceP;
        newObjPeachone.ControlPoint = secondPoint;
        newObjPeachone.EndPoint = dstTransform;
        newObjPeachone.UsePeach = true;
        newObjPeachone.Timer = 0;
        newObjPeachone.transform.localScale = ProjectileScale;
        newobs.GetComponent<TrailRenderer>().material.color = mColors[mColorCnt & 7];
        mColorCnt++;
        newobs.transform.position = sourceP.position; //시작 위치
    }
    public override void Attack()
    {
        if (isEvoluction())
            evoAttack(SkillFiringSystem.instance.evolutionWeaponPrefabs[WeaponIndex], SkillFiringSystem.instance.Circles[1]);
        else
            Attack(SkillFiringSystem.instance.weaponPrefabs[WeaponIndex], SkillFiringSystem.instance.Circles[0]);
    }
    public void Attack(GameObject peachPre, GameObject bounderyPre)
    {
        Timer += Time.deltaTime;
        if (Timer > WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, true, this, StartPoint);
            Timer = 0;
            ProjectileScale = transform.localScale * WeaponTotalStats[((int)Enums.WeaponStat.Area)];
            mColorCnt = 0;
        }
    }
    public void evoAttack(GameObject peachPre, GameObject bounderyPre)
    {
        Timer += Time.deltaTime;
        if (Timer > WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<EvoPeachBoundery>().CreateCircle(peachPre, bounderyPre, true, this, StartPoint);
            Timer = 0;
            ProjectileScale = transform.localScale * WeaponTotalStats[((int)Enums.WeaponStat.Area)];
            mColorCnt = 0;
        }
    }
    private Vector3 calculateBezierPoint()
    {
        float u = 1f - Timer;
        float tt = Timer * Timer;
        float uu = u * u;
        Vector3 nowPos = uu * StartPoint.position;
        nowPos += 2f * u * Timer * ControlPoint;
        nowPos += tt * EndPoint.position;

        return nowPos;
    }
    public void SpawnBlueBird(GameObject bird)
    {
        Bird = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        var newObjBird = Bird.GetComponent<Bird>();
        newObjBird.PlayerTransform = GameManager.instance.player.transform;
        StartPoint = newObjBird.transform;
    }
    private void SpawnWhiteBird(GameObject bird, Weapon pairWeapon)
    {
        Bird = pairWeapon.GetComponent<EbonyWings>().Bird = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        Bird.GetComponent<Bird>().PlayerTransform = GameManager.instance.player.transform;
    }
    public override void EvolutionProcess()
    {
        var equipManageSys = GameManager.instance.equipManageSys;
        var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex]]];
        var bird = SkillFiringSystem.instance.Birds[2];
        pairWeapon.GetComponent<EbonyWings>().EvolutionProcess();

        Destroy(Bird, pairWeapon.WeaponTotalStats[(int)Enums.WeaponStat.Cooldown] - Timer);
        Destroy(pairWeapon.GetComponent<EbonyWings>().Bird, pairWeapon.WeaponTotalStats[(int)Enums.WeaponStat.Cooldown] - Timer);
        SpawnWhiteBird(bird, pairWeapon);
        StartPoint = Bird.GetComponent<Bird>().transform;
        pairWeapon.GetComponent<EbonyWings>().StartPoint = pairWeapon.GetComponent<EbonyWings>().Bird.GetComponent<Bird>().transform;
    }
}