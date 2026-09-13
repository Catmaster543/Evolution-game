using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

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
    }

    public void Clicked()
    {
        if (tower != null)
        {
            if (tower.GetComponent<Tower>().treeType == shop.purchasedTreeType)
            {
                int level = tower.GetComponent<Tower>().level;
                if (shop.purchasedTreeType == "Pine" && level + 1 < map.pineTowers.Length)
                {
                    Destroy(tower);
                    tower = Instantiate(map.pineTowers[level+1], transform.position, quaternion.identity);
                    tower.GetComponent<Tower>().level = level + 1;
                    tower.GetComponent<Tower>().treeType = "Pine";
                }
                else if (shop.purchasedTreeType == "Palm" && level + 1 < map.palmTowers.Length)
                {
                    Destroy(tower);
                    tower = Instantiate(map.palmTowers[level+1], transform.position, quaternion.identity);
                    tower.GetComponent<Tower>().level = level + 1;
                    tower.GetComponent<Tower>().treeType = "Palm";
                }
                
                else if (shop.purchasedTreeType == "Sakura" && level + 1 < map.sakuraTowers.Length)
                {
                    Destroy(tower);
                    tower = Instantiate(map.sakuraTowers[level + 1], transform.position, quaternion.identity);
                    tower.GetComponent<Tower>().level = level + 1;
                    tower.GetComponent<Tower>().treeType = "Sakura";
                }
            }
        }
        else
        {
            tower = Instantiate(shop.purchasedTower, gameObject.transform.position, Quaternion.identity);
            Debug.Log($"Placing tree {tower.name}");
            Debug.Log("Placed tree");
        }
        taken = true;
        map.lastPlacedSpot = this;
        balance.balance -= shop.cost;
        map.placed = true;
    }
}
