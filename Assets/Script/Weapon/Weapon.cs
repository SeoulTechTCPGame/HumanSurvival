using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //������ ���� ����
    //���ø� ���� ���� �������� ����
    private int damage = 10;              //���ط�
    private int projectileSpeed = 1;     //����ü �ӵ�
    private int duration = 3;            //���� �ð�
    private int attackRange = 1;         //���ݹ���
    private int cooldown = 3;            //��Ÿ��
    private int numberOfProjectiles = 1;     //����ü ��
    private int totalspeed;     //�� �ӵ�

    private void Update()
    {
        transform.position = transform.position + Vector3.right * totalspeed * Time.deltaTime;
    }
    public void Shoot(int speed)
    {
        totalspeed = speed;
    }
    //Get,Set�Լ� �ڵ� ����
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
}
