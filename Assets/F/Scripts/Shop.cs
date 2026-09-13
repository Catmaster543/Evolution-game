using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable]
public class PurchaseableTree
{
    public GameObject tower;
    public string ID;
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

    public TextMeshProUGUI pineCostText;
    public TextMeshProUGUI palmCostText;
    public TextMeshProUGUI sakuraCostText;

    public float pinePrice;
    public float pineMultiplier;
    public float palmPrice;
    public float palmMultiplier;
    public float sakuraPrice;
    public float sakuraMultiplier;

    public string purchasedTreeType;

    public float cost;

    private Map map;
    private Balance balance;
    void Start()
    {
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        balance = GameObject.FindGameObjectWithTag("Balance").GetComponent<Balance>();

        pineCostText.text = pinePrice.ToString();
        palmCostText.text = palmPrice.ToString();
        sakuraCostText.text = sakuraPrice.ToString();
    }

    void Update()
    {
        
    }

    public void OpenTab()
    {
        
    }

    public void BuyAPineTree()
    {
        if (balance.balance >= pinePrice)
        {
            purchasedTower = pineTree;
            purchasedTreeType = "Pine";
            cost = pinePrice;
            pinePrice = Mathf.Round(pinePrice * pineMultiplier);
            Debug.Log("Purchased pine tree set, moving on to placing it");
            StartCoroutine(map.PlaceATree());
            pineCostText.text = pinePrice.ToString();
        }
    }

    public void BuyAPalmTree()
    {
        Debug.Log("Trying to buy a palm");
        if (balance.balance >= palmPrice)
        {
            purchasedTower = palmTree;
            purchasedTreeType = "Palm";
            cost = palmPrice;
            palmPrice = Mathf.Round(palmPrice * palmMultiplier);
            Debug.Log("Purchased palm tree set, moving on to placing it");
            StartCoroutine(map.PlaceATree());
            palmCostText.text = palmPrice.ToString();
        }
    }

    public void BuyASakuraTree()
    {
        Debug.Log("Trying to buy a sakura");
        if (balance.balance >= sakuraPrice)
        {
            purchasedTower = sakuraTree;
            purchasedTreeType = "Sakura";
            cost = sakuraPrice;
            sakuraPrice = Mathf.Round(sakuraPrice * sakuraMultiplier);
            Debug.Log("Purchased sakura tree set, moving on to placing it");
            StartCoroutine(map.PlaceATree());
            sakuraCostText.text = sakuraPrice.ToString();
        }
    }
}
