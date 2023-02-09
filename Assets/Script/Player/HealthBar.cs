using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject Player;
    private Vector3 mOffset;

    void Update()
    {
        mOffset = transform.position - Player.transform.position;
        transform.position = Player.transform.position + mOffset;
    }
}
