using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class Monster
{
    public int speed; //몬스터의 이동 속도
    public float power; //몬스터의 공격력
    public float knockback; //몬스터 피격 시 넉백(밀리는) 정도에 대한 수치
    public float maxKnockback; //몬스터 넉백 정도는 증가할 수 있는데 그 정도의 상한
    public float deathKB; //몬스터 사망 시 넉백(밀리는) 정도에 대한 수치
    public int xp; //드랍되는 경험치의 양(수치) 이다.
    public int end; //레벨 업 상한선
    public int level; //초기 레벨 수치
    public float maxHP;
}

[Serializable]
public class MonsterList
{
    public Dictionary<string, Monster> monsters;
}


public class EnemyData : MonoBehaviour
{
    private void Start()
    {
        Dictionary<string, Monster> monsterDic = new Dictionary<string, Monster>();

        Monster bat = new Monster();
        bat.level = 1;
        bat.maxHP = 0.5f;
        bat.speed = 140;
        bat.power = 5;
        bat.knockback = 1;
        bat.maxKnockback = 3;
        bat.deathKB = 2;
        bat.xp = 1;
        bat.end = 29;
        


        Monster skeleton = new Monster();
        skeleton.level = 1;
        skeleton.maxHP = 1.5f;
        skeleton.speed = 100;
        skeleton.power = 10;
        skeleton.knockback = 1;
        skeleton.maxKnockback = 3;
        skeleton.deathKB = 5;
        skeleton.xp = 2;
        skeleton.end = 18;

        Monster ghoul = new Monster();
        ghoul.level = 1;
        ghoul.maxHP = 1.0f;
        ghoul.speed = 100;
        ghoul.power = 10;
        ghoul.knockback = 0.8f;
        ghoul.maxKnockback = 3;
        ghoul.deathKB = 4;
        ghoul.xp = 1;
        ghoul.end = 20;




        monsterDic["Bat"] = bat;
        monsterDic["Skeleton"] = skeleton;
        monsterDic["Ghoul"] = ghoul;

        MonsterList Monster = new MonsterList();
        Monster.monsters = monsterDic;

        //ToJson 부분
        //string jsonData = DictionaryJsonUtility.ToJson(monsterDic, true);

        string path = Application.dataPath + "/Data";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //File.WriteAllText(path + "/MonsterData.txt", jsonData);

        //FromJson 부분
        string fromJsonData = File.ReadAllText(path + "/MonsterData.txt");

        MonsterList MonsterFromJson = new MonsterList();
       // MonsterFromJson.monsters = DictionaryJsonUtility.FromJson<string, Monster>(fromJsonData);
        print(MonsterFromJson.monsters);
    }

}