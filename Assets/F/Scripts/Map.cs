using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] List<TowerPlaceable> spots = new();


    public bool placed = false;

    private Shop shop;
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        CheckPlaceableSpots();
    }
    void Update()
    {
        
    }

    public IEnumerator PlaceATree()
    {
        Debug.Log("Placing a tree");
        foreach (TowerPlaceable spot in spots)
        {
            spot.gameObject.SetActive(true);
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
}
