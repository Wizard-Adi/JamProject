using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[SerializeField]
    //private GameObject _enemyPrefab;
    //[SerializeField]
    //private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] enemies;

    public int PlayerScore = 0;

    public int minSpawnTime = 3;
    public int maxSpawnTime = 8;

    private bool _stopSpawning = false;
    // Start is called before the first frame update

    private void Start()
    {
        StartSpawning();
    }

    public void Update()
    {
        if (PlayerScore <= 100)
        {
            minSpawnTime = 3;
            maxSpawnTime = 8;
        }
        else if (PlayerScore <= 200)
        {
            minSpawnTime = 2;
            maxSpawnTime = 6;
        }
        else if(PlayerScore >= 201)
        {
            minSpawnTime = 1;
            maxSpawnTime = 5;
        }
    }

    public void StartSpawning()
    {
        //StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemiesRoutine());
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        // every 3 - 7 seconds, spawn in a powerup
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(20.5f, Random.Range(-4f, 4f), 0);
            int randomEnemy = Random.Range(0, 3);
            Instantiate(enemies[randomEnemy], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime)); 
        }
    }
    

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

    public void OnShelterDestroy()
    {
        _stopSpawning = true;
    }
}
