using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Wave
{
    public string name;
    public int spawnInterval;
    public int spacingToNextWave;
    public bool randomizeSpawnTime;
    public float randomRange;
    public SpawnAbleEnemy[] enemies;
}

[System.Serializable]
public class SpawnAbleEnemy
{
    public GameObject enemyObject;
    public int count;
}

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private Wave[] waves;

    private float clocker;
    private bool clock;

    private float timeLeft;
    void Start()
    {
        StartCoroutine(waitForNextWave());
    }
    void Update()
    {
        
    }
    
    private IEnumerator waitTillSpawn(Wave wave)
    {
        int j = 0;
        foreach (SpawnAbleEnemy enemy in wave.enemies)
        {
            Debug.Log($"j is {j}");
            for (int i = 0; i < wave.enemies[j].count; i++)
            {
                timeLeft = wave.spawnInterval;
                Debug.Log($"Timeleft is {timeLeft}, apllying multiplier, random ceiling is {wave.randomRange}");
                if (wave.randomizeSpawnTime)
                {
                    timeLeft *= Random.Range(0, wave.randomRange);
                }
                Debug.Log($"Waiting for {timeLeft}");
                yield return new WaitForSeconds(timeLeft);
                GameObject spawnable = wave.enemies[j].enemyObject;
                Instantiate(spawnable);
            }
            j++;
        }
    }

    private IEnumerator waitForNextWave()
    {
        foreach (Wave wavey in waves)
        {
            yield return waitTillSpawn(wavey);

            yield return new WaitForSeconds(wavey.spacingToNextWave);
        }
    }
}
