using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject wrathPrefab;
    public GameObject gluttonyPrefab;
    public GameObject lustPrefab;
    private int enemiesPerRow = 10;
    private float enemiesSpawnXPosition = -10f;
    private float xSpacing = 2f;
    private float ySpacing = 1.2f;
    private float SpawnY = 6.4f;
    public float moveInterval;

    private List<GameObject> activeEnemies = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveInterval = 4;
        StartCoroutine(SpawnTopEnemyWave());
        StartCoroutine(SpawnMiddleEnemyWave());
        StartCoroutine (SpawnBottomEnemyWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (activeEnemies.Count == 0)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnTopEnemyWave());
            StartCoroutine(SpawnMiddleEnemyWave());
            StartCoroutine(SpawnBottomEnemyWave());
        }
      
    }
    /// <summary>
    /// Spawns a row of enemies at the top of an enemy wave
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTopEnemyWave()
    {
        for (int i = 0; i < enemiesPerRow; i++)
        {
            Vector3 spawnPos = new Vector3(enemiesSpawnXPosition + (i * xSpacing), SpawnY, 0);
            GameObject enemy = Instantiate(wrathPrefab, spawnPos, Quaternion.identity);
            activeEnemies.Add(enemy);  
        }
        moveInterval -= 0.1f;
        yield return new WaitUntil(() => activeEnemies.Count == 0);
    }

    /// <summary>
    /// Spawns a row of enemies in the middle of an enemy wave
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnMiddleEnemyWave()
    {
        for (int i = 0; i < enemiesPerRow; i++)
        {
            Vector3 spawnPos = new Vector3(enemiesSpawnXPosition + (i * xSpacing), SpawnY - ySpacing, 0);
            GameObject enemy = Instantiate(gluttonyPrefab, spawnPos, Quaternion.identity);
            activeEnemies.Add(enemy);
        }
        yield return new WaitUntil(() => activeEnemies.Count == 0);
    }

    /// <summary>
    /// Spawns a row of enemies at the bottom of an enemy wave
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnBottomEnemyWave()
    {
        for (int i = 0; i < enemiesPerRow; i++)
        {
            Vector3 spawnPos = new Vector3(enemiesSpawnXPosition + (i * xSpacing), SpawnY - (ySpacing *2), 0);
            GameObject enemy = Instantiate(lustPrefab, spawnPos, Quaternion.identity);
            activeEnemies.Add(enemy);
        }
        yield return new WaitUntil(() => activeEnemies.Count == 0);
    }

    /// <summary>
    /// removes an enemy prefab from the list
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemyFromList(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
    }

    


}
