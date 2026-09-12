using Unity.Mathematics;
using UnityEngine;

public class TowerPlaceable : MonoBehaviour
{
    public GameObject tower;
    public string ID;
    public bool taken;

    public Shop shop;
    public Map map;
    
    private Balance balance;
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        balance = GameObject.FindGameObjectWithTag("Balance").GetComponent<Balance>();

        gameObject.SetActive(false);
    }

    public void Clicked()
    {
        tower = shop.purchasedTower;
        Debug.Log($"Placing tree {tower.name}");
        Instantiate(tower, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Placed tree");
        balance.balance -= shop.cost;
        map.placed = true;
    }
}
