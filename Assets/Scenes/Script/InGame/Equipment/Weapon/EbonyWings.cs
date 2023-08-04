using UnityEngine;

public class EbonyWings : Weapon
{
    public GameObject Bird = null;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    public Vector3 ProjectileScale;

    [SerializeField] AudioClip mFireClip;
    [SerializeField] Animator mAnimator;

    private bool mbHolding = false;
    private static Color[] mColors = { new Color(0.85f, 0.13f, 0.19f, 1), new Color(1, 0.46f, 0, 1), new Color(0.89f, 0.87f, 0.88f, 1)
            , new Color(0.51f, 0.93f, 0.17f, 1), new Color(0.13f, 0.89f, 0.51f, 1), new Color(0.13f, 0.75f, 1, 1), new Color(0.17f, 0.14f, 0.91f, 1), new Color(0.84f, 0.27f, 0.86f, 1) };
    private int mColorCnt = 0;
    private float mTimer = 100;
    private bool mbUseEbony = false;

    private void FixedUpdate()
    {
        if (!mbUseEbony || mbHolding)
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
    public void Fire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        SoundManager.instance.PlaySoundEffect(mFireClip);
        var newObjEbonyWings = newObs.GetComponent<EbonyWings>();
        newObjEbonyWings.StartPoint = sourceP;
        newObjEbonyWings.ControlPoint = secondPoint;
        newObjEbonyWings.EndPoint = dstTransform;
        newObjEbonyWings.mbUseEbony = true;
        newObjEbonyWings.mTimer = 0;
        newObjEbonyWings.transform.localScale = ProjectileScale;
        newObs.transform.position = sourceP.position; //시작 위치
    }
    public void EvoFire(GameObject objPre, Transform dstTransform, Vector3 secondPoint, Transform sourceP)
    {
        GameObject newObs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjEbonyWings = newObs.GetComponent<EbonyWings>();
        newObjEbonyWings.StartPoint = sourceP;
        newObjEbonyWings.ControlPoint = secondPoint;
        newObjEbonyWings.EndPoint = dstTransform;
        newObjEbonyWings.mbUseEbony = true;
        newObjEbonyWings.mTimer = 0;
        newObjEbonyWings.transform.localScale = ProjectileScale;
        newObs.GetComponent<TrailRenderer>().material.color = mColors[mColorCnt & 7];
        mColorCnt++;
        newObs.transform.position = sourceP.position; //시작 위치
    }
    public void SpawnCircle(GameObject peachPre, GameObject bounderyPre)
    {
        mTimer += Time.deltaTime;
        if (mTimer > WeaponTotalStats[((int)Enums.EWeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, false, this, StartPoint);
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
            bounderyPre.GetComponent<EvoPeachBoundery>().CreateCircle(peachPre, bounderyPre, false, this, StartPoint);
            mTimer = 0;
            ProjectileScale = transform.localScale * WeaponTotalStats[((int)Enums.EWeaponStat.Area)];
            mColorCnt = 0;
        }
    }
    public void SpawnBlackBird(GameObject bird)
    {
        Bird = Instantiate(bird, GameObject.Find("SkillFiringSystem").transform);
        var newObjBird = Bird.GetComponent<Bird>();
        newObjBird.PlayerTransform = GameManager.instance.Player.transform;
        StartPoint = newObjBird.transform;
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
        base.EvolutionProcess();
        var equipManageSys = GameManager.instance.EquipManageSys;
        var pairWeapon = equipManageSys.Weapons[equipManageSys.TransWeaponIndex[EquipmentData.EvoWeaponNeedWeaponIndex[WeaponIndex]]];
        pairWeapon.EvolutionProcess();
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
}