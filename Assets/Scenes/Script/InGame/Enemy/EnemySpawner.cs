using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnGroup
{
    public EnemyScriptableObject EnemyData;
    public int Count;
    public bool BBoss;
    public float RepeatTimer;
    public float TimeBetweenSpawn;
    public int RepeatCount;

    public EnemiesSpawnGroup(EnemyScriptableObject enemyData,int count,bool isBoss)
    {
        this.EnemyData = enemyData;
        this.Count = count;
        this.BBoss = isBoss;
    }
    public void SetRepeatSpawn(float timeBetweenSpawns,int repeatCount)
    {
        this.TimeBetweenSpawn = timeBetweenSpawns;
        this.RepeatCount = repeatCount;
        RepeatTimer = TimeBetweenSpawn;
    }
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyScriptableObject[] SpawnData;
    [SerializeField] Vector2 mSpawnArea;
    [SerializeField] GameObject mPlayer;
    private List<EnemiesSpawnGroup> mEnemiesSpawnGroupList;
    private List<EnemiesSpawnGroup> mRepeatedSpawnGroupList;

    private void Update()
    {
        EnemyWaveSpawn();
        EnemyWaveRepeatedSpawnGroups();
    }
    public void AddGroupToSpawn(EnemyScriptableObject enemyToSpawn, int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);
        if (mEnemiesSpawnGroupList == null)
        {
            mEnemiesSpawnGroupList = new List<EnemiesSpawnGroup>();    
        }
        mEnemiesSpawnGroupList.Add(newGroupToSpawn);
    }
    public void AddReapeatedSpawn(StageEvent stageEvent, bool bBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.EnemyToSpawn, stageEvent.EnemyCount, bBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.RepeatEverySeconds,stageEvent.RepeatCount);
        if (mRepeatedSpawnGroupList == null)
        {
            mRepeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }
        mRepeatedSpawnGroupList.Add(repeatSpawnGroup);
    }
    public void SpawnEnemy(EnemyScriptableObject enemyToSpawn)
    {
        Vector3 position;
        //player의 위치 값에 랜덤 pos를 더해 스폰 지점 설정
        if (enemyToSpawn.SpriteType == 4)//flower wall
        {
            position = mPlayer.transform.position;
        }
        else
        {
            position= GenerateRandomPos();
            position += mPlayer.transform.position;
        }

        GameObject newEnemy= GameManager.instance.Pool.Get("enemy",enemyToSpawn.SpriteType);
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
    private void EnemyWaveRepeatedSpawnGroups()
    {
        if (mRepeatedSpawnGroupList == null) return;
        for (int i = mRepeatedSpawnGroupList.Count-1;i>=0; i--)
        {
            mRepeatedSpawnGroupList[i].RepeatTimer -= Time.deltaTime;
            if (mRepeatedSpawnGroupList[i].RepeatTimer < 0)
            {
                mRepeatedSpawnGroupList[i].RepeatTimer = mRepeatedSpawnGroupList[i].TimeBetweenSpawn;
                AddGroupToSpawn(mRepeatedSpawnGroupList[i].EnemyData, mRepeatedSpawnGroupList[i].Count, mRepeatedSpawnGroupList[i].BBoss);
                mRepeatedSpawnGroupList[i].RepeatCount -= 1;
                if (mRepeatedSpawnGroupList[i].RepeatCount <= 0)
                {
                    mRepeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }
    private void EnemyWaveSpawn()
    {
        if (mEnemiesSpawnGroupList == null) return;
        if (mEnemiesSpawnGroupList.Count > 0)
        {
            SpawnEnemy(mEnemiesSpawnGroupList[0].EnemyData);
            mEnemiesSpawnGroupList[0].Count -= 1;
            if (mEnemiesSpawnGroupList[0].Count <= 0)
            {
                mEnemiesSpawnGroupList.RemoveAt(0);
            }
        }
    }
    private Vector3 GenerateRandomPos()
    {
        Vector3 position = new Vector3();
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-mSpawnArea.x, mSpawnArea.x);
            position.y = mSpawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-mSpawnArea.y, mSpawnArea.y);
            position.x = mSpawnArea.x * f;
        }
        position.z = 0;
        return position;
    }
}