using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

[System.Serializable]
public class MapLayout
{
    public TowerPlaceable[] towerPlaceables;
    public SpriteRenderer background;
    public Sprite newBackground;
    public SplineContainer spline;
    public SplineContainer newSpline;
}

[System.Serializable]
public class ShopLayout
{
    public GameObject bgObj;
    public GameObject treesButton;
    public GameObject upgradesButton;
    public GameObject pot1;
    public GameObject pot2;
    public GameObject pot3;
}

[System.Serializable]
public class ShopLayoutUpdate
{
    public Sprite newBg;
    public Sprite newTrees;
    public Sprite newUpgrade;
    public Sprite newPot1;
    public Sprite newPot2;
    public Sprite newPot3;
}

public class EnemyWaveManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] public Wave[] waves;
    [SerializeField] public MapLayout mapLayout;
    [SerializeField] private bool updateSpline;
    [SerializeField] private bool updateShop;
    [SerializeField] public ShopLayout shopLayout;
    [SerializeField] public ShopLayoutUpdate shopUpdate;
    [SerializeField] private bool updateCurrency;
    [SerializeField] private GameObject currencyBox;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private Sprite newCurrencyBox;
    [SerializeField] private bool isFirstManager;
    [SerializeField] private bool isLastManager;

    public int waveNumber;

    private GameObject prevWaveManager;
    [SerializeField] GameObject nextWaveManager;

    private int enemyNumber;

    public List<GameObject> enemies = new();

    private float timeLeft;
    void Start()
    {
        Debug.Log($"Reporting in action! -{gameObject.name}");
        if (!isFirstManager)
        {
            mapLayout.background = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
            mapLayout.background.sprite = mapLayout.newBackground;

            if (updateSpline)
            {
                mapLayout.spline = GameObject.FindGameObjectWithTag("Spline").GetComponent<SplineContainer>();
                mapLayout.spline.gameObject.SetActive(false);
                mapLayout.newSpline.tag = "Spline";
            }

            prevWaveManager = GameObject.FindGameObjectWithTag("Wave manager");

            prevWaveManager.gameObject.SetActive(false);
        }

        if (updateShop)
        {
            shopLayout.bgObj.GetComponent<UnityEngine.UI.Image>().sprite = shopUpdate.newBg;
            shopLayout.treesButton.GetComponent<UnityEngine.UI.Button>().image.sprite = shopUpdate.newTrees;
            shopLayout.upgradesButton.GetComponent<UnityEngine.UI.Button>().image.sprite = shopUpdate.newUpgrade;

            shopLayout.pot1.GetComponent<UnityEngine.UI.Image>().sprite = shopUpdate.newPot1;
            shopLayout.pot2.GetComponent<UnityEngine.UI.Image>().sprite = shopUpdate.newPot2;
            shopLayout.pot3.GetComponent<UnityEngine.UI.Image>().sprite = shopUpdate.newPot3;
        }

        if (updateCurrency)
        {
            currencyBox.GetComponent<UnityEngine.UI.Image>().sprite = newCurrencyBox;
            currencyText.color = new Color(113, 65, 71);
        }

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
                //Debug.Log($"Timeleft is {timeLeft}, apllying multiplier, random ceiling is {wave.randomRange}");
                if (wave.randomizeSpawnTime)
                {
                    timeLeft *= Random.Range(0, wave.randomRange);
                }
                //Debug.Log($"Waiting for {timeLeft}");
                yield return new WaitForSeconds(timeLeft);
                GameObject spawnedAble = Instantiate(wave.enemies[j].enemyObject, spawnPoint.transform);
                spawnedAble.GetComponent<Enemy>().order = enemyNumber;
                enemies.Add(spawnedAble);
                enemyNumber++;
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

            yield return waitTillEnemiesDead();
        }
        if (!isLastManager)
        {
            nextWaveManager.SetActive(true);
            nextWaveManager.tag = "Wave manager";
            Debug.Log($"Next wave manager has been set up! By: {gameObject.name}");
            Debug.Log($"Goodbye world! -{gameObject.name}");
            Destroy(gameObject);
        }
    }

    private IEnumerator waitTillEnemiesDead()
    {
        yield return new WaitUntil(() => enemies.Count == 0);
    }
}
