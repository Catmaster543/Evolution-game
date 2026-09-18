using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Map : MonoBehaviour
{
    [SerializeField] public List<TowerPlaceable> spots = new();
    [SerializeField] public GameObject[] pineTowers;
    [SerializeField] public GameObject[] palmTowers;
    [SerializeField] public GameObject[] sakuraTowers;
    [SerializeField] public AudioSource theSoundOfDoom;

    public bool placed = false;

    public bool tutorialBuying = false;

    private Shop shop;
    private Map map;

    public TowerPlaceable lastPlacedSpot;

    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        theSoundOfDoom = gameObject.GetComponent<AudioSource>();
        StartCoroutine(CheckPlaceableSpots());
        DeActivateAllSpots();
    }

    public IEnumerator PlaceATree()
    {
        Debug.Log("Placing a tree");
        Debug.Log("Turning on all last spots");
        ActivateAllSpots();
        Debug.Log("Checking tree placement");
        yield return CheckPlaceableSpots();
        DeActivateAllSpots();
        Debug.Log($"Checked all spots, result: {spots}, count: {spots.Count}");
        foreach (TowerPlaceable spot in spots)
        {
            if (!spot.taken)
            {
                Debug.Log($"Spot {spot.name} is empty, turning it on!");
                spot.gameObject.SetActive(true);
            }
            else if (spot.tower.GetComponent<Tower>().treeType == shop.purchasedTreeType)
            {
                if (spot.tower.GetComponent<Tower>().treeType == "Pine")
                {
                    if (spot.tower.GetComponent<Tower>().level < map.pineTowers.Length-1)
                    {
                        Debug.Log($"Spot {spot.name} has a pine tree at level {spot.tower.GetComponent<Tower>().level}, which is smaller then {map.pineTowers.Length}");
                        spot.gameObject.SetActive(true);
                        spot.tower.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                }
                else if (spot.tower.GetComponent<Tower>().treeType == "Palm")
                {
                    if (spot.tower.GetComponent<Tower>().level < map.palmTowers.Length-1)
                    {
                        spot.gameObject.SetActive(true);
                        spot.tower.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                }
                else if (spot.tower.GetComponent<Tower>().treeType == "Sakura")
                {
                    if (spot.tower.GetComponent<Tower>().level < map.sakuraTowers.Length-1)
                    {
                        spot.gameObject.SetActive(true);
                        spot.tower.GetComponent<SpriteRenderer>().color = Color.yellow;
                    }
                }
                Debug.Log($"Spot {spot.name} is used, turning it on");
            }
        }
        Debug.Log("Turned on all spots, waiting for placement");
        yield return new WaitUntil(() => placed);
        Debug.Log("Tree placed, finishing placement");
        FinishPlacingTree(shop.purchasedTower);
    }

    public void FinishPlacingTree(GameObject tower)
    {
        Debug.Log("Finishing tree placement");
        foreach (TowerPlaceable spot in spots)
        {
            if (spot.tower != null)
            {
                spot.tower.GetComponent<SpriteRenderer>().color = Color.white;
            }
            spot.gameObject.SetActive(false);
        }
        placed = false;
    }

    public IEnumerator CheckPlaceableSpots()
    {
        Debug.Log("Checking all placeable spots.. -Map");
        spots.Clear();
        Debug.Log("Removed current spots list");
        GameObject[] spawnableSpots = GameObject.FindGameObjectsWithTag("Tower spot");
        foreach (GameObject spot in spawnableSpots)
        {
            spots.Add(spot.GetComponentInChildren<TowerPlaceable>());
        }
        Debug.Log($"Created new list of spots; {spots}, amount: {spots.Count}");
        Debug.Log("Now deactivating spots..");
        yield return null;
    }

    public void DeActivateAllSpots()
    {
        GameObject[] spawnableSpots = GameObject.FindGameObjectsWithTag("Tower spot");
        foreach (GameObject spot in spawnableSpots)
        {
            spot.SetActive(false);
        }
        Debug.Log("All spots deactivated -Map");
    }

    public void ActivateAllSpots()
    {
        foreach (TowerPlaceable spot in spots)
        {
            spot.gameObject.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("First level");
    }
}
