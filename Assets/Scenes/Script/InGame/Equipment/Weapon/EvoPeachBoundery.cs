using System.Collections;
using System.Collections.Generic;
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
    private bool isClockwise = true;
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

        transform.position = getStartPosition(GameManager.instance.player.transform.position);
        if (isClockwise)
            transform.RotateAround(GameManager.instance.player.transform.position, Vector3.back, mSpeed * mTimer);
        else
            transform.RotateAround(GameManager.instance.player.transform.position, -Vector3.back, mSpeed * mTimer);
        if (mTimer <= mDuration && mTimer >= mAccCooldown)
        {
            // peachone 소환
            var startPos = mBirdTransform.position;
            Vector3 pos = getSecondPosition();
            mPeachObj.GetComponent<Peachone>().Fire(mPeachObj, transform, pos, mBirdTransform);
            mAccCooldown += mCooldown;
        }
    }

    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, bool isCW, Weapon peachone, Transform birdTransform)
    {
        GameObject newobs = Instantiate(bounderyPre, GameObject.Find("SkillFiringSystem").transform);
        newobs.transform.position = getStartPosition(GameManager.instance.player.transform.position);
        var newPeachoneBoundery = newobs.GetComponent<EvoPeachBoundery>();
        newPeachoneBoundery.mBirdTransform = birdTransform;
        newPeachoneBoundery.mDuration = peachone.WeaponTotalStats[((int)Enums.WeaponStat.Duration)] * 2.5f;
        newPeachoneBoundery.mRealDuration = newPeachoneBoundery.mDuration + 1.5f;
        newPeachoneBoundery.mSpeed = mSpeed * peachone.WeaponTotalStats[((int)Enums.WeaponStat.ProjectileSpeed)];
        newPeachoneBoundery.mCooldown = mCooldown / peachone.WeaponTotalStats[((int)Enums.WeaponStat.Amount)];
        newPeachoneBoundery.mPeachObj = peachPre;
        newPeachoneBoundery.mPlayerTransform = GameManager.instance.player.transform;
        if (!isCW)
            newPeachoneBoundery.isClockwise = false;
    }

    private Vector3 getStartPosition(Vector3 pos)
    {
        return pos + Vector3.left * mDistance;
    }
    private Vector3 getSecondPosition()
    {
        return mPlayerTransform.position + Vector3.up * mDistance;
    }
}
