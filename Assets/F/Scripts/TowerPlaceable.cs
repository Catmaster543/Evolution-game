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

    bool diaConnected;
    bool diasConnected;

    public TutDialogue tutDialogue;
    public TutsDialogue tutsDialogue;
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        balance = GameObject.FindGameObjectWithTag("Balance").GetComponent<Balance>();

        if (GameObject.FindGameObjectWithTag("Dia") != null)
        {
            tutDialogue = GameObject.FindGameObjectWithTag("Dia").GetComponent<TutDialogue>();
            diaConnected = true;
        }
        else
        {
            diaConnected = false;
        }

        if (GameObject.FindGameObjectWithTag("Dias") != null)
        {
            tutsDialogue = GameObject.FindGameObjectWithTag("Dias").GetComponent<TutsDialogue>();
            diasConnected = true;
        }
        else
        {
            diasConnected = false;
        }
    }

    public void Clicked()
    {
        if (GameObject.FindGameObjectWithTag("Dias") != null)
        {
            tutsDialogue = GameObject.FindGameObjectWithTag("Dias").GetComponent<TutsDialogue>();
            diasConnected = true;
        }
        else
        {
            diasConnected = false;
        }
        Debug.Log($"I ({gameObject.name}) have been clicked");
        if (tower != null)
        {
            if (tower.GetComponent<Tower>().treeType == shop.purchasedTreeType)
            {
                int level = tower.GetComponent<Tower>().level;
                if (shop.purchasedTreeType == "Pine" && level + 1 < map.pineTowers.Length)
                {
                    Destroy(tower);
                    tower = Instantiate(map.pineTowers[level+1], transform.position, quaternion.identity);
                    Debug.Log($"Place a {map.pineTowers[level + 1]}");
                    tower.GetComponent<Tower>().level = level + 1;
                    tower.GetComponent<Tower>().treeType = "Pine";
                }
                else if (shop.purchasedTreeType == "Palm" && level + 1 < map.palmTowers.Length)
                {
                    Destroy(tower);
                    tower = Instantiate(map.palmTowers[level+1], transform.position, quaternion.identity);
                    Debug.Log($"Place a {map.pineTowers[level + 1]}");
                    tower.GetComponent<Tower>().level = level + 1;
                    tower.GetComponent<Tower>().treeType = "Palm";
                }
                else if (shop.purchasedTreeType == "Sakura" && level + 1 < map.sakuraTowers.Length)
                {
                    Destroy(tower);
                    tower = Instantiate(map.sakuraTowers[level + 1], transform.position, quaternion.identity);
                    Debug.Log($"Place a {map.pineTowers[level + 1]}");
                    tower.GetComponent<Tower>().level = level + 1;
                    tower.GetComponent<Tower>().treeType = "Sakura";
                }
                balance.balance -= shop.cost;
            }
        }
        else
        {
            tower = Instantiate(shop.purchasedTower, gameObject.transform.position, Quaternion.identity);
            Debug.Log($"Placing tree {tower.name}");
            Debug.Log("Placed tree");
            balance.balance -= shop.cost;
        }
        if (diaConnected)
        {
            tutDialogue.placing = false;
            tutDialogue.i = tutDialogue.i + 1;
        }
        if (diasConnected)
        {
            tutsDialogue.buying = false;
            tutsDialogue.i = 7;
        }
        if (shop.purchasedTreeType == "Pine")
        {
            shop.pinePrice = Mathf.Round(shop.pinePrice * shop.pineMultiplier);
            shop.pineCostText.text = shop.pinePrice.ToString();
        }
        else if (shop.purchasedTreeType == "Palm")
        {
            shop.palmPrice = Mathf.Round(shop.palmPrice * shop.palmMultiplier);
            shop.palmCostText.text = shop.palmPrice.ToString();
        }
        else if (shop.purchasedTreeType == "Sakura")
        {
            shop.sakuraPrice = Mathf.Round(shop.sakuraPrice * shop.sakuraMultiplier);
            shop.sakuraCostText.text = shop.sakuraPrice.ToString();
        }
        taken = true;
        map.lastPlacedSpot = this;
        map.placed = true;
    }

    public void MoveTower()
    {
        if (tower != null)
        {
            tower.transform.position = transform.position;
        }   
    }
}
