using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss, 
    SpawnObjcet,
    WinStage
}
[Serializable]
public class StageEvent
{
    public StageEventType eventType;
    public float time; //time 부터 stage 시작
    public string message;

    public EnemyScriptableObject enemyToSpawn;
    public int enemyCount; //스폰될 적 수
    public bool isRepeatedEvent;
    public float repeatEverySeconds;
    public int repeatCount;
}
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
}
