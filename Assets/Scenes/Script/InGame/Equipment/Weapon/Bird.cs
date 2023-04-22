using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bird : MonoBehaviour
{
    private Animator mBirdAni;
    private Transform mPlayerTransform;
    private float mMaxDist = 4f;
    private float mSpeed   = 2f;

    // Start is called before the first frame update
    void Start()
    {
        if (isOutOfRange())
        {
            changeHeadDir();
            transform.position = Vector3.MoveTowards(transform.position, mPlayerTransform.position, mSpeed);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isOutOfRange())
        {
            transform.position = Vector3.MoveTowards(transform.position, mPlayerTransform.position, mMaxDist);
        }
    }

    bool isOutOfRange()
    {
        var distSq = Mathf.Pow(transform.position.x - mPlayerTransform.position.x, 2) + Mathf.Pow(transform.position.y - mPlayerTransform.position.y, 2);
        if (distSq > mMaxDist)
            return true;
        return false;
    }

    void changeHeadDir()
    {
        var dX = transform.position.x - mPlayerTransform.position.x;
        var dY = transform.position.y - mPlayerTransform.position.y;
        if(Mathf.Abs(dX) > Mathf.Abs(dY))
        {
            if(dX > 0)
                mBirdAni.SetTrigger("Up");
            else
                mBirdAni.SetTrigger("Down");
        }
        else
        {
            if (dY < 0)
                mBirdAni.SetTrigger("Left");
            else
                mBirdAni.SetTrigger("Right");
        }
    }
}
