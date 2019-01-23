using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

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

    private int waveIndex = 0;
    private int enemyCount = 0; // how many enemies have spawned in the wave
    public int enemyChange = 1; // change to the type of enemy at this number of enemies spawned
    private int enemyDif = 1; // the enemy type that is being spawned

    void Update()
    {
        Spawner(); // so it spawns without the start button
        if (enemyCount >= enemyChange)    // changes the enemy type 
        {
            enemyType = enemyDif;   // logic needs to eb changed later after design
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
}
