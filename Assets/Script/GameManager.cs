using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f; //20ÃÊ

    [Header("# Player Info")]
    public Character character;
    public int level;
    public int kill;
    public int coin;

    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerMovement player;
    public GameObject gameoverPanel;

    private void Awake()
    {
        instance = this;
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
        player.enabled = false;
        //pool.ClearAll();
        gameoverPanel.SetActive(true);
        


    }
}
