using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable]
public class PurchaseableTree
{
    public GameObject tower;
    public int price;
    public Button button;
}

[System.Serializable]
public class Shop : MonoBehaviour
{
    public GameObject purchasedTower;
    public PurchaseableTree[] purchaseableTrees;

    public GameObject pineTree;
    public GameObject palmTree;
    public GameObject sakuraTree;

    private Map map;
    private Balance balance;
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        //balance = GameObject.FindGameObjectWithTag()
    }

    void Update()
    {
        
    }

    public void OpenTab()
    {
        
    }

    public void BuyAPineTree()
    {
        //if ()
        purchasedTower = pineTree;
        Debug.Log("Purchased tree set, moving on to placing it");
        StartCoroutine(map.PlaceATree());
    }

    public void BuyAPalmTree(Tower tower)
    {
        purchasedTower = palmTree;
        map.PlaceATree();
    }

    public void BuyASakuraTree(Tower tower)
    {
        purchasedTower = sakuraTree;
        map.PlaceATree();
    }
}
