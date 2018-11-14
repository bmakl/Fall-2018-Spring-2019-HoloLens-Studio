using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Level Stats")]
    public int health = 100;
    public GameObject endPoint;  //if enemy collides with this - 1 health
    public int waveCount = 1;   //counts the wave and is ther multi for num of enemies spawned

    [Header("Money")]
    public int money;

    [Header("Spawner")]
    public GameObject[] enemyPrefab;
    //public GameObject enemyFastPrefab;
    public Transform spawnPoint;

    public int enemyType = 0;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public float timeBetweenEnemies = 0.5f;
    public int waveSize = 10;

    public int Wave = 1; // what wave we are on for math

    private int waveIndex = 0;
    private int enemyCount = 0; // how many enemies have spawned in the wave
    public int enemyChange = 1; // change to the type of enemy at this number of enemies spawned
    private int enemyDif = 1; // the enemy type that is being spawned

    void Update()
    {
        Spawner(); // so it spawns without the start button
        EnemyType();

        if(enemyCount>= 10)
        {
            enemyCount = 0;
        }


    }
    IEnumerator SpawnWave()
    {
        waveIndex = waveCount * waveSize; // how many spawn per wave
        for (int i = 0; i < waveIndex; i++)
        {
            enemyCount++;
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies); 
        }

    }
    void Spawner()
    {
        if (countdown <= 0f) // spawns waves 5sec apart
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        int enemyIndex = enemyType;

        Instantiate(enemyPrefab[enemyIndex], spawnPoint.position, spawnPoint.rotation);
        //Instantiate(enemyFastPrefab, spawnPoint.position, spawnPoint.rotation);

    }

    void EnemyType()
    {
        
        int TypeEnemy = enemyCount;

        switch (TypeEnemy)
        {
            case 3:
                enemyType = 1;
                break;
            case 6:
                enemyType = 1;
                break;
            case 9:
                enemyType = 1;
                break;

            default:
                enemyType = 0;
                break;
        }

    }
}
