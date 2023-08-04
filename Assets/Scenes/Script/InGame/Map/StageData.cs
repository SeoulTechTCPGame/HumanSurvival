using System;
using System.Collections.Generic;
using UnityEngine;

public enum EStageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss, 
    WinStage
}

[Serializable]
public class StageEvent
{
    public EStageEventType EventType;
    public float Time; //time 부터 stage 시작
    public string Message;

    public EnemyScriptableObject EnemyToSpawn;
    public int EnemyCount; //스폰될 적 수
    public bool BRepeatedEvent;
    public float RepeatEverySeconds;
    public int RepeatCount;
}

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> StageEvents;
}