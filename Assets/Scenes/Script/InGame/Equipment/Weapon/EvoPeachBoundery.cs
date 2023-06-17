using UnityEngine;

public class EvoPeachBoundery : MonoBehaviour
{
    private float mDistance = 8f;  // 오브젝트와 타겟 사이의 거리
    private float mSpeed = 40f;  // 공전 속도
    private float mTimer = 0;
    private float mDuration;
    private float mRealDuration;
    private float mCooldown = 2f;
    private float mAccCooldown = 0;
    private bool mbClockwise = true;
    private GameObject mPeachObj;
    private Transform mBirdTransform;
    private Transform mPlayerTransform;

    private void FixedUpdate()
    {
        mTimer += Time.deltaTime;
        if (mTimer > mRealDuration)
        {
            Destroy(gameObject);
        }

        transform.position = GetStartPosition(GameManager.instance.Player.transform.position);
        if (mbClockwise)
            transform.RotateAround(GameManager.instance.Player.transform.position, Vector3.back, mSpeed * mTimer);
        else
            transform.RotateAround(GameManager.instance.Player.transform.position, -Vector3.back, mSpeed * mTimer);
        if (mTimer <= mDuration && mTimer >= mAccCooldown)
        {
            // peachone 소환
            var startPos = mBirdTransform.position;
            Vector3 pos = GetSecondPosition();
            mPeachObj.GetComponent<Peachone>().EvoFire(mPeachObj, transform, pos, mBirdTransform);
            mAccCooldown += mCooldown;
        }
    }

    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, bool isCW, Weapon peachone, Transform birdTransform)
    {
        GameObject newobs = Instantiate(bounderyPre, GameObject.Find("SkillFiringSystem").transform);
        newobs.transform.position = GetStartPosition(GameManager.instance.Player.transform.position);
        var newPeachoneBoundery = newobs.GetComponent<EvoPeachBoundery>();
        newPeachoneBoundery.mBirdTransform = birdTransform;
        newPeachoneBoundery.mDuration = peachone.WeaponTotalStats[((int)Enums.EWeaponStat.Duration)] * 2.5f;
        newPeachoneBoundery.mRealDuration = newPeachoneBoundery.mDuration + 1.5f;
        newPeachoneBoundery.mSpeed = mSpeed * peachone.WeaponTotalStats[((int)Enums.EWeaponStat.ProjectileSpeed)];
        newPeachoneBoundery.mCooldown = mCooldown / peachone.WeaponTotalStats[((int)Enums.EWeaponStat.Amount)];
        newPeachoneBoundery.mPeachObj = peachPre;
        newPeachoneBoundery.mPlayerTransform = GameManager.instance.Player.transform;
        if (!isCW)
            newPeachoneBoundery.mbClockwise = false;
    }

    private Vector3 GetStartPosition(Vector3 pos)
    {
        return pos + Vector3.left * mDistance;
    }
    private Vector3 GetSecondPosition()
    {
        return mPlayerTransform.position + Vector3.up * mDistance;
    }
}
