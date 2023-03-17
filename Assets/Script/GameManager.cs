using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 180 * 10f;

    [Header("# Player Info")]
    public Character character;
    public int level;
    public int kill;
    public float exp;
    public float maxExp;
    public int coin;
    public int[] killCount = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };   // Whip, MagicWand, Knife, Axe, Cross, KingBible, FireWand, Garlic, SantaWater, Peachone, EbonyWings, Runetracer, LightningRing

    [Header("# Game Object")]
    public PoolManager pool;
    public PlayerMovement player;
    public GameObject gameoverPanel;

    //  Singleton Instance 선언
    public static GameManager instance = null;

    private void Awake()
    {
        // Scene에 이미 인스턴스가 존재 하는지 확인 후 처리
        /*if (instance)
        {
            Destroy(this.gameObject);
            return;
        }*/
        // instance를 유일 오브젝트로 만든다
        instance = this;
        // Scene 이동 시 삭제 되지 않도록 처리
        DontDestroyOnLoad(this.gameObject);
        level = 1;
        exp = 0;
        maxExp = 100;
        //Time.timeScale = 1;


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
        Time.timeScale = 0;

        gameoverPanel.SetActive(true); // 판넬 활성화

    }
    public void GetCoin(int amount)
    {
        coin += amount;
        Debug.Log("coin: "+coin);
    }
}
