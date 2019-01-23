using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject enemyFastPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public float timeBetweenEnemies = 0.5f;

    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }
    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Instantiate(enemyFastPrefab, spawnPoint.position, spawnPoint.rotation);

    }
}
