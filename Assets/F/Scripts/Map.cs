using System.Collections;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] TowerPlaceable[] spots;


    public bool placed = false;

    private Shop shop;
    void Start()
    {
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
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
        //Instantiate(tower, theSpot.transform);
        foreach (TowerPlaceable spot in spots)
        {
            spot.gameObject.SetActive(false);
        }
        placed = false;
    }
}
