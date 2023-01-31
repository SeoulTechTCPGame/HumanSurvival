using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;
    public SpawnData[] spawnData;
    int level;
    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        //float�� �ð��� ���� int�� ���� ����
        level =Mathf.Min(Mathf.FloorToInt( GameManager.instance.gameTime / 10f),spawnData.Length-1);

        //������ Ȱ���� ���� ������ ��ȯ Ÿ�̹� �����ϱ�
        if (timer >(spawnData[level].spawnTime))
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        //player�� ��ġ ���� ���� pos�� ���� ���� ���� ����
        Vector3 position = GenerateRandomPos();
        position += player.transform.position;

        GameObject newEnemy= GameManager.instance.pool.Get(0);
        newEnemy.transform.position = position;
        newEnemy.transform.parent = transform;

        //****script �̸� ���� �� ������Ʈ �̸� �ٲٱ�!****
        newEnemy.GetComponent<N_Enemy>().Init(spawnData[level]);
    }

    private Vector3 GenerateRandomPos()
    {
        Vector3 position = new Vector3();
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }


        position.z = 0;
        return position;
    }
}
[System.Serializable]
public class SpawnData
{   
    public float spawnTime;

    public int spriteType;
    public int health;
    public float speed;
}