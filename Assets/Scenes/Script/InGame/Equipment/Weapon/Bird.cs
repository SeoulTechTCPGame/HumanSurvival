using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Transform mPlayerTransform;
    private float mMaxDist = 4f;

    // Start is called before the first frame update
    void Start()
    {
        if (isOutOfRange())
        {
            //transform.position = Vector3.MoveTowards();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    bool isOutOfRange()
    {
        var distSq = Mathf.Pow(transform.position.x - mPlayerTransform.position.x, 2) + Mathf.Pow(transform.position.y - mPlayerTransform.position.y, 2);
        if (distSq > mMaxDist)
            return true;
        return false;
    }
}
