using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameResult : MonoBehaviour
{
    [SerializeField] TMP_Text map = null;
    [SerializeField] TMP_Text time = null;
    float gameTime;
    [SerializeField] TMP_Text coin = null;
    [SerializeField] TMP_Text level = null;
    [SerializeField] TMP_Text kill = null;

    Weapon weapon;
    int Weaponname;
    int Weaponlevel;
    int Weapondamage;
    int WeapondamagePerSec;

    void Start()
    {
        level.text = string.Format("{0}", GameManager.instance.level);
        kill.text = string.Format("{0}", GameManager.instance.kill);
        coin.text = string.Format("{0}", GameManager.instance.coin);
        gameTime = GameManager.instance.gameTime;
        float seconds = Mathf.Floor(gameTime % 60);
        float minutes = Mathf.Floor(gameTime / 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
    private void OnDestroy()
    {
        
    }
}