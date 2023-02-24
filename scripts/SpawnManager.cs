using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public GameObject enemyPrefab;
    public GameObject enemies;

    public float enemyBurstCount = 8;
    public float spawnTime = 2;

    Transform location;
    float updateTime;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            spawnPoints.Add(child);
    }


    // Update is called once per frame
    void Update()
    {
        if(Time.time > updateTime)
        {
            updateTime = Time.time + spawnTime;
            SpawnEnemy();
        }
        
    }
    public void SpawnEnemy()
    {
        if(enemies.transform.childCount < enemyBurstCount)
        {
            location = spawnPoints[Random.Range(0, transform.childCount)];
            var enemyInstance = Instantiate(enemyPrefab, location);
            enemyInstance.transform.SetParent(enemies.transform);
        }
    }
}
