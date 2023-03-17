using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;
    public EnemyScriptableObject[] spawnData;
    int level;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        //float형 시간에 따라 int형 레벨 설정
        level =Mathf.Min(Mathf.FloorToInt( GameManager.instance.gameTime / 10f),spawnData.Length-1);

        //레벨을 활용해 몬스터 각각의 소환 타이밍 변경하기
        if (timer >(spawnData[level].SpawnTime))
        {
            Spawn();
            timer = 0;
        }
    }


    private void Spawn()
    {
        //player의 위치 값에 랜덤 pos를 더해 스폰 지점 설정
        Vector3 position = GenerateRandomPos();
        position += player.transform.position;

        GameObject newEnemy= GameManager.instance.pool.Get("enemy",level);
        newEnemy.transform.position = position;
        newEnemy.transform.parent = transform;

        newEnemy.GetComponent<Enemy>().InitEnemy(spawnData[level]);
    }

    private Vector3 GenerateRandomPos()
    {
        Vector3 position = new Vector3();
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }


        position.z = 0;
        return position;
    }
}
