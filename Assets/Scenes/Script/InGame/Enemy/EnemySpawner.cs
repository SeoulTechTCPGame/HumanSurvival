using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnGroup
{
    public EnemyScriptableObject enemyData;
    public int count;
    public bool isBoss;

    public float repeatTimer;
    public float timeBetweenSpawn;
    public int repeatCount;

    public EnemiesSpawnGroup(EnemyScriptableObject enemyData,int count,bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }
public void SetRepeatSpawn(float timeBetweenSpawns,int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawns;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn;
    }
}
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;
    public EnemyScriptableObject[] spawnData;

    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    private void Update()
    {
        EnemyWaveSpawn();
        EnemyWaveRepeatedSpawnGroups();
    }

    private void EnemyWaveRepeatedSpawnGroups()
    {
        if (repeatedSpawnGroupList == null) return;
        for (int i = repeatedSpawnGroupList.Count-1;i>=0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if (repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);
                repeatedSpawnGroupList[i].repeatCount -= 1;
                if (repeatedSpawnGroupList[i].repeatCount <= 0)
                {
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void EnemyWaveSpawn()
    {
        if (enemiesSpawnGroupList == null) return;
        if (enemiesSpawnGroupList.Count > 0)
        {
            SpawnEnemy(enemiesSpawnGroupList[0].enemyData);
            enemiesSpawnGroupList[0].count -= 1;
            if (enemiesSpawnGroupList[0].count <= 0)
            {
                enemiesSpawnGroupList.RemoveAt(0);
            }
        }
    }

    public void AddGroupToSpawn(EnemyScriptableObject enemyToSpawn, int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);
        if (enemiesSpawnGroupList == null)
        {
            enemiesSpawnGroupList = new List<EnemiesSpawnGroup>();    
        }
        enemiesSpawnGroupList.Add(newGroupToSpawn);
    }

    public void AddReapeatedSpawn(StageEvent stageEvent,bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.enemyCount, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds,stageEvent.repeatCount);
        if (repeatedSpawnGroupList == null)
        {
            repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }
        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }

    public void SpawnEnemy(EnemyScriptableObject enemyToSpawn)
    {
        Vector3 position;
        //player의 위치 값에 랜덤 pos를 더해 스폰 지점 설정
        if (enemyToSpawn.SpriteType == 5)//flower wall
        {
            position = player.transform.position;
        }
        else
        {
            position= GenerateRandomPos();
            position += player.transform.position;
        }

        GameObject newEnemy= GameManager.instance.pool.Get("enemy",enemyToSpawn.SpriteType);
        newEnemy.transform.position = position;
        newEnemy.transform.parent = transform;
        switch (enemyToSpawn.SpriteType)
        {   //bat bevy,flower wall
            case 4:
            case 5:
                newEnemy.GetComponentsInChildren<Enemy>()[0].InitEnemy(enemyToSpawn);
                break;
            default:
                newEnemy.GetComponent<Enemy>().InitEnemy(enemyToSpawn);
                break;
        }
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
