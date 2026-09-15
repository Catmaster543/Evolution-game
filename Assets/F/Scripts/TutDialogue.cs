using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TutDialogue : MonoBehaviour
{   
    public int i = 0;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;
    public bool isTyping;

    private EnemyWaveManager enemyWaveManager;
    private Shop shop;
    private Map map;

    public bool placing = false;
    void Start()
    {
        textComponent.text = string.Empty;
        enemyWaveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (i == 5 && !placing)
            {
                Debug.Log("Tutorial placing has started");
                shop.purchasedTreeType = "Pine";
                shop.purchasedTower = shop.pineTree;
                StartCoroutine(map.PlaceATree());
                placing = true;
            }
            else if (i == 5 && placing)
            {
                
            }
            else if (i == 12)
            {
                enemyWaveManager.spawn = true;
                gameObject.SetActive(false);
            }
            else
            {
                NextLine();
            }
            
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    System.Collections.IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        i++;
        isTyping = false;
    }
    public void NextLine()
    {
        if (isTyping)
        {
            textComponent.text = lines[index];
            isTyping = false;
            StopAllCoroutines();
            i++;
        }
        else
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                textComponent.text = string.Empty;
            }
        }
    }
}
