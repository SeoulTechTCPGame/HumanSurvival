using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.TouchScreenKeyboard;

public class TreasureChest : MonoBehaviour
{
    private GameObject      mChests;
    private GameObject[]    mPickLights;
    private GameObject[]    mPickSprites;
    private GameObject      mCoin;
    private bool            mbIsOn;
    private int             mRotSpeed;
    private float           mTime;

    void Start()
    {
        mbIsOn = false;
        mRotSpeed = 60;
        mTime = 0;

        var children = GetComponentsInChildren<GameObject>();
        mChests = children[0];
        mPickLights = children[1].GetComponentsInChildren<GameObject>();
        mPickSprites = children[2].GetComponentsInChildren<GameObject>();
        mCoin = children[3];

        UnloadChestUI();
    }

    void Update()
    {
        if (mbIsOn && mTime < 0.99f)
        {
            transform.Rotate(0, 0, mRotSpeed * Time.unscaledDeltaTime);

            transform.localScale = Vector3.one * (mTime);

            mTime += 0.02f;
            if (mTime >= 0.99f)
                transform.rotation = Quaternion.identity;
        }
    }
    public void LoadChestUI()
    {
        mTime = 0;
        mbIsOn = true;
        GetComponent<GameObject>().SetActive(true);
        GameManager.instance.Player.enabled = false;
    }
    public void UnloadChestUI()
    {
        mbIsOn = false;
        GetComponent<GameObject>().SetActive(false);
        GameManager.instance.Player.enabled = true;
    }
}
