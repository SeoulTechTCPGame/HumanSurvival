using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] StageData mStageData;
    [SerializeField] EnemySpawner mEnemySpawner;
    private int mEventIndexer;
    private void Start()
    {
        mEnemySpawner = FindObjectOfType<EnemySpawner>();
    }
    private void Update()
    {
        if (mEventIndexer >= mStageData.StageEvents.Count) return;
        //게임 시간> stageData의 time일때 이벤트 실행
        if (GameManager.instance.GameTime > mStageData.StageEvents[mEventIndexer].Time)
        {
            Debug.Log(mStageData.StageEvents[mEventIndexer].Message);
            switch (mStageData.StageEvents[mEventIndexer].EventType)
            {
                case EStageEventType.SpawnEnemy:
                    SpawnEnemy(false);
                    break;
                case EStageEventType.SpawnEnemyBoss:
                    SpawnEnemy(true);
                    break;
                case EStageEventType.SpawnObjcet://chest spawn
                    break;
                case EStageEventType.WinStage:
                    break;
            }
            mEventIndexer += 1;
        }   
    }
    private void SpawnEnemy(bool bBoss)
    {
        StageEvent currentEvent = mStageData.StageEvents[mEventIndexer];
        mEnemySpawner.AddGroupToSpawn(currentEvent.EnemyToSpawn,currentEvent.EnemyCount,bBoss);
        if (currentEvent.BRepeatedEvent == true)
        {
            mEnemySpawner.AddReapeatedSpawn(currentEvent, bBoss);
        }
    }
}
