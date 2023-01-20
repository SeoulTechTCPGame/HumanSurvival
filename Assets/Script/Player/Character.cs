using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.System;

public class Character : MonoBehaviour
{
    //캐릭터의 스탯지정
    //예시를 위해 값은 무작위로 넣음
    private int damage = 10;              //피해량
    private int projectileSpeed = 1;     //투사체 속도
    private int duration = 3;            //지속 시간
    private int attackRange = 1;         //공격범위
    private int cooldown = 3;            //쿨타임
    private int numberOfProjectiles = 1;     //투사체 수

    private int _level;
    private int _exp;
    private int _maxExp;
    private int _dExp;

    public Stat ChracterStat;

    void Start()
    {
        _level = 1;
        _exp = 0;
        _maxExp = 100;
        _dExp = 10;

        // TODO user가 메인 화면에서 강화해놓은 스탯들을 기본값으로 받아오기

    }

    //Get,Set함수 자동 구현
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    public int ProjectileSpeed
    {
        get { return projectileSpeed; }
        set { projectileSpeed = value; }
    }
    public int Duration
    {
        get { return duration; }
        set { duration = value; }
    }
    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    public int Cooldown
    {
        get { return cooldown; }
        set { cooldown = value; }
    }
    public int NumberOfProjectiles
    {
        get { return numberOfProjectiles; }
        set { numberOfProjectiles = value; }
    }


    public void GetExp(int exp)
    {
        _exp += exp;
        while (_exp >= _maxExp)
        {
            _exp -= _maxExp;
            _maxExp += _dExp;
            LevelUp();
        }
    }
    public void LevelUp()
    {
        _level++;
        // TODO: 스킬 선택 기능, luck과 희귀도 반영
    }
}
