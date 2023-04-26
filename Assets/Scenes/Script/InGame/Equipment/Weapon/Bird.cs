using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;


public class Bird : MonoBehaviour
{
    [SerializeField] Animator BirdAni;
    public Transform PlayerTransform;
    private float mMaxDist;
    private float mSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mSpeed = Random.Range(0.14f, 0.18f);
        mMaxDist = Random.Range(7f, 15f);
        if (isOutOfRange())
        {
            changeHeadDir();
            transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, mSpeed);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isOutOfRange())
        {
            changeHeadDir();
            transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, mSpeed);
        }
    }

    bool isOutOfRange()
    {
        var distSq = Mathf.Pow(transform.position.x - PlayerTransform.position.x, 2) + Mathf.Pow(transform.position.y - PlayerTransform.position.y, 2);
        if (distSq > mMaxDist)
            return true;
        return false;
    }

    void changeHeadDir()
    {
        var dX = PlayerTransform.position.x - transform.position.x;
        var dY = PlayerTransform.position.y - transform.position.y;
        allResetTrigger();
        if(Mathf.Abs(dX) > Mathf.Abs(dY))
        {
            if(dX > 0)
                BirdAni.SetTrigger("Right");
            else
                BirdAni.SetTrigger("Left");
        }
        else
        {
            if (dY > 0)
                BirdAni.SetTrigger("Up");
            else
                BirdAni.SetTrigger("Down");
        }
    }
    void allResetTrigger()
    {
        foreach(var param in BirdAni.parameters)
        {
            BirdAni.ResetTrigger(param.name);
        }
    }
}
