using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss, 
    SpawnEnemyElite,
    SpawnObjcet,
    WinStage
}
[Serializable]
public class StageEvent
{
    public StageEventType eventType;
    public float time;
    public string message;

    public EnemyScriptableObject enemyToSpawn;
    public int count;

}
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
}
