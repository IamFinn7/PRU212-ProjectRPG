using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossPrefab;

    [SerializeField] private bool canSpawn = true;
    [SerializeField] private int maxEnemies = 3;
    [SerializeField] private float spawnRadius = 3f;
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    private int enemyKilled = 0;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;

            if (enemies.Count < maxEnemies)
            {
                Vector2 spawnPosition = (Vector2) transform.position + Random.insideUnitCircle * spawnRadius;

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                enemies.Add(newEnemy);
                newEnemy.GetComponent<EnemiesController>().OnEnemyDestroyed += removeEnemyFromList;
            }
        }
    }


    private void SpawnBoss(){
        Vector2 spawnPosition = (Vector2) transform.position + Random.insideUnitCircle * spawnRadius;
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }
    private void removeEnemyFromList()
    {
        enemyKilled += 1;
        if ( enemyKilled == maxEnemies ){
            SpawnBoss();
        }
    }
}