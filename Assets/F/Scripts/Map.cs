using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Map : MonoBehaviour
{
    [SerializeField] List<TowerPlaceable> spots = new();
    [SerializeField] public GameObject[] pineTowers;
    [SerializeField] public GameObject[] palmTowers;
    [SerializeField] public GameObject[] sakuraTowers;

    public bool placed = false;

    private Shop shop;

    public TowerPlaceable lastPlacedSpot;

    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        CheckPlaceableSpots();
        DeActivateAllSpots();
    }
    void Update()
    {
        
    }

    public IEnumerator PlaceATree()
    {
        Debug.Log("Placing a tree");
        foreach (TowerPlaceable spot in spots)
        {
            if (!spot.taken)
            {
                spot.gameObject.SetActive(true);
            }
            else if (spot.tower.GetComponent<Tower>().treeType == shop.purchasedTreeType)
            {
                spot.gameObject.SetActive(true);
                spot.tower.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
        Debug.Log("Turned on all spots, waiting for placement");
        yield return new WaitUntil(() => placed);
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

    public void CheckPlaceableSpots()
    {
        GameObject[] spawnableSpots = GameObject.FindGameObjectsWithTag("Tower spot");
        foreach (GameObject spot in spawnableSpots)
        {
            spots.Add(spot.GetComponentInChildren<TowerPlaceable>());
        }
    }

    public void DeActivateAllSpots()
    {
        GameObject[] spawnableSpots = GameObject.FindGameObjectsWithTag("Tower spot");
        foreach (GameObject spot in spawnableSpots)
        {
            spot.SetActive(false);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Main menu");
    }
}
