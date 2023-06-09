using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemySpawner enemySpawner;
    int eventIndexer;
    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }
    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count) return;
        //게임 시간> stageData의 time일때 이벤트 실행
        if (GameManager.instance.gameTime > stageData.stageEvents[eventIndexer].time)
        {
            Debug.Log(stageData.stageEvents[eventIndexer].message);
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    SpawnEnemy(false);
                    break;
                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemy(true);
                    break;
                case StageEventType.SpawnObjcet://chest spawn
                    break;
                case StageEventType.WinStage:
                    break;
            }
            eventIndexer += 1;
        }   
    }
    private void SpawnEnemy(bool isBoss)
    {
        StageEvent currentEvent = stageData.stageEvents[eventIndexer];
        enemySpawner.AddGroupToSpawn(currentEvent.enemyToSpawn,currentEvent.enemyCount,isBoss);
        if (currentEvent.isRepeatedEvent == true)
        {
            enemySpawner.AddReapeatedSpawn(currentEvent, isBoss);
        }
    }
}
