using Enums;
using UnityEngine;

public class PeachBoundery : MonoBehaviour
{
    private float mDistance = 8f;  // 오브젝트와 타겟 사이의 거리
    private float mSpeed = 40f;  // 공전 속도
    private float mTimer = 0;
    private float mFireTimer = 0;
    private float mFireCoolTime = 0;
    private float mDuration;
    private float mRealDuration;
    private float mCooldown = 2f;
    private float mAccCooldown = 0;
    private bool isClockwise = true;
    private bool mProjectileDirUp = true; // 위 아래 번갈아가며
    private GameObject mPeachObj;
    private Transform mBirdTransform;

    private void Update()
    {
        mTimer += Time.deltaTime;
        mFireTimer += Time.deltaTime;
        if (mTimer > mRealDuration)
        {
            Destroy(gameObject);
        }

        transform.position = getStartPosition(GameManager.instance.Player.transform.position);
        if(isClockwise)
            transform.RotateAround(GameManager.instance.Player.transform.position, Vector3.back, mSpeed * mTimer);
        else
            transform.RotateAround(GameManager.instance.Player.transform.position, -Vector3.back, mSpeed * mTimer);

        if(mTimer <= mDuration && mTimer >= mAccCooldown)
        {
            if (mFireTimer >= mFireCoolTime)
            {
                mFireTimer -= mFireCoolTime;
                // peachone 소환
                var startPos = mBirdTransform.position;
                Vector3 pos = startPos + (transform.position - startPos) * 0.2f; // bird와 circle 사이 1:4 지점
                Vector3 perpendicular = Vector3.Cross((transform.position - startPos).normalized, Vector3.forward);
                if (mProjectileDirUp)
                    pos += perpendicular * 10f;
                else
                    pos -= perpendicular * 10f;

                if (isClockwise)
                    mPeachObj.GetComponent<Peachone>().Fire(mPeachObj, transform, pos, mBirdTransform);
                else
                    mPeachObj.GetComponent<EbonyWings>().Fire(mPeachObj, transform, pos, mBirdTransform);
                mProjectileDirUp = !mProjectileDirUp;
                mAccCooldown += mCooldown;
            }
        }
    }
    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, bool isCW, Weapon peachone, Transform birdTransform)
    {
        GameObject newobs = Instantiate(bounderyPre, GameObject.Find("SkillFiringSystem").transform);
        newobs.transform.position = getStartPosition(GameManager.instance.Player.transform.position);
        var newPeachoneBoundery = newobs.GetComponent<PeachBoundery>();
        newPeachoneBoundery.mBirdTransform = birdTransform;
        newPeachoneBoundery.mDuration = peachone.WeaponTotalStats[(int)Enums.EWeaponStat.Duration] * 2.5f;
        newPeachoneBoundery.mRealDuration = newPeachoneBoundery.mDuration + 1.5f;
        newPeachoneBoundery.mSpeed = mSpeed * peachone.WeaponTotalStats[(int)Enums.EWeaponStat.ProjectileSpeed];
        newPeachoneBoundery.mCooldown = mCooldown / peachone.WeaponTotalStats[(int)Enums.EWeaponStat.Amount];
        newPeachoneBoundery.mFireCoolTime = 1 / peachone.WeaponTotalStats[(int)Enums.EWeaponStat.Amount];
        newPeachoneBoundery.mPeachObj = peachPre;
        if (!isCW)
            newPeachoneBoundery.isClockwise = false;
    }
    private Vector3 getStartPosition(Vector3 pos)
    {
        return pos + Vector3.up * mDistance;
    }
}