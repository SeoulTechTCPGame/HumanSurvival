using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;

    float timer;

    private void Update()
    {

        timer -= Time.deltaTime;
        if (timer < 1f)
        {
            SpawnEnemy();
            timer = 0;
        }
    }

    private void SpawnEnemy()
    {
        
        Vector3 position = GenerateRandomPos();
        position += player.transform.position;
        GameObject newEnemy= GameManager.instance.pool.Get(Random.Range(0,3));
        newEnemy.transform.position = position;
        newEnemy.transform.parent = transform;
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
