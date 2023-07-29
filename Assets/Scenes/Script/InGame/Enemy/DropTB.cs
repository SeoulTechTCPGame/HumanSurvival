using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTB : MonoBehaviour
{
    [SerializeField] GameObject mTreasureBox;
    private void OnDisable()
    {
        if (GameManager.instance!=null&&GameManager.instance.IsGMAlive)
        {
            Transform t = Instantiate(mTreasureBox).transform;
            t.position = transform.position;
        }
    }
}
