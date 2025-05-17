using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements; //usun号 po testach 

public class WaveSpawner : MonoBehaviour
{
    [Header("WaveManagment")]
    public int currentWave;
    public float waveValue;

    [Header("SpawnTimer")]
    public float waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    private float xMax, yMax;
    public float xMin, yMin;
    //usun号 po testach 
    public Text textElement;

    public List<Enemy> enemies = new List<Enemy>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        GenerateWave();   
    }

    private void Update()
    {
        string CurrValue = currentWave.ToString();
        textElement.text = "Wave: " + CurrValue; 
    }

    private void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                Vector2 randomSpawnPoint = new(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], randomSpawnPoint, Quaternion.identity); // spawn 1st enemy in list
                enemiesToSpawn.RemoveAt(0); // remove it 
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0; // if no enemys remain, end wave
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        if (waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currentWave++;
            waveDuration += 1;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = currentWave * 10;
        Debug.Log("wave Value: " + waveValue);//usun号 po testach 
        GenerateEnemy();


        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
        Debug.Log("spawn int: " + spawnInterval);//usun号 po testach 
        Debug.Log("wave Dur: " + waveDuration);//usun号 po testach 
    }

    public void GenerateEnemy()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randomEnemyId = Random.Range(0, enemies.Count);
            int randomEnemyCost = enemies[randomEnemyId].cost;

            if (waveValue - randomEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randomEnemyId].enemyPrefab);
                waveValue -= randomEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear(); ;
        enemiesToSpawn = generatedEnemies;
    }

    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyPrefab;
        public int cost;
    }

    private void OnDrawGizmos()
    {
        Vector3[] lines;

        Gizmos.color = Color.green;
        xMax = -xMin;
        yMax = -yMin;
        lines = new Vector3[4]
        {
            new Vector3(xMax, yMin),
            new Vector3(xMin, yMax),
            new Vector3(xMax, yMax),
            new Vector3(xMin, yMin),
        };
        Gizmos.DrawLineList(lines);
    }
}
