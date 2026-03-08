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

    private bool _stopSpawning = false;
    // Start is called before the first frame update

    private void Start()
    {
        StartSpawning();
    }
    public void StartSpawning()
    {
        //StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnEnemiesRoutine());
    }
    // spawn game objects every 5 seconds
    // create a coroutine of type IEnumerator -- yield events
    // while loop

    //IEnumerator SpawnEnemyRoutine()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    while (_stopSpawning == false)
    //    {
    //        Vector3 posToSpawn = new Vector3(11.5f, Random.Range(-4f, 4f), 0);
    //        GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
    //        newEnemy.transform.parent = _enemyContainer.transform;
    //        yield return new WaitForSeconds(5.0f);
    //    }
    //    // while loop (infinite loop )
    //    // instantiate enemy prefab
    //    //yield wait for 5 sec
    //}
    
    IEnumerator SpawnEnemiesRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        // every 3 - 7 seconds, spawn in a powerup
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(11.5f, Random.Range(-4f, 4f), 0);
            int randomEnemy = Random.Range(0, 3);
            Instantiate(enemies[randomEnemy], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));// here 8 is exclusive...
        }
    }
    

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
