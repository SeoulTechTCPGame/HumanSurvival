using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f; //20초

    [Header("# Player Info")]
    public Character character;
    public int level;
    public int kill;
    public int exp;
    public int coin;

    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerMovement player;
    public GameObject gameoverPanel;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {

        gameTime += Time.deltaTime;
        if (gameTime >maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GameOverPanelUp()
    {
        Debug.Log("Game over");
        player.enabled = false; // Character object 비활성화
        pool.enabled = false;
        gameoverPanel.SetActive(true); // 판넬 활성화

    }
    public void GetCoin(int amount)
    {
        Debug.Log("coin: "+coin);
        coin += amount;
    }
}
