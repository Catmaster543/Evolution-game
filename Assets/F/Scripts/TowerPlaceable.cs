using Unity.Mathematics;
using UnityEngine;

public class TowerPlaceable : MonoBehaviour
{
    public GameObject tower;
    public string ID;
    public bool taken;

    public Shop shop;
    public Map map;
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();

        gameObject.SetActive(false);
    }

    public void Clicked()
    {
        tower = shop.purchasedTower;
        Debug.Log($"Placing tree {tower.name}");
        Instantiate(tower, gameObject.transform.position, Quaternion.identity);
        Debug.Log("Placed tree");
        map.placed = true;
    }
}
