using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    [SerializeField] EnemySpawner enemySpawner;
    int eventIndexer;

    private void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count) return;
        //시간에 따른 스테이지 이벤트 
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
    private void SpawnEnemy(bool isBose)
    {
        for(int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            enemySpawner.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn);

        }
    }
}
