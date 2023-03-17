using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 mOffset;
    void Start()
    {
        mOffset = transform.position - Player.transform.position;
    }
    void LateUpdate()
    {
        transform.position = Player.transform.position + mOffset;
    }
}