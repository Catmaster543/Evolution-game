using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndDialog : MonoBehaviour
{
    public int i = 0;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI guideTextComponent;
    public string guideText;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;
    public bool isTyping;
    
    public GameObject endChoices;
    public GameObject endFadeScreen;

    private EnemyWaveManager enemyWaveManager;
    private Shop shop;
    private Map map;

    public bool placing = false;
    void Start()
    {
        textComponent.text = string.Empty;
        guideTextComponent.text = string.Empty;
        enemyWaveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
        shop = GameObject.FindGameObjectWithTag("Shopkeeper").GetComponent<Shop>();
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (i == 8)
            {
                StartCoroutine(playEndAnimations());
                //gameObject.SetActive(false);
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

        StartCoroutine(TypeGuideLine());
    }
    public void NextLine()
    {
        guideTextComponent.text = string.Empty;

        if (isTyping)
        {
            textComponent.text = lines[index];
            isTyping = false;
            StopAllCoroutines();
            StartCoroutine(TypeGuideLine());
            i++;
        }
        else
        {
            StopCoroutine(TypeGuideLine());
            guideTextComponent.text = string.Empty;
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

    System.Collections.IEnumerator TypeGuideLine()
    {
        guideTextComponent.text = string.Empty;
        yield return new WaitForSeconds(2);

        foreach (char c in guideText.ToCharArray())
        {
            guideTextComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //---

    public IEnumerator playEndAnimations()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Fade-out");
        float animLength3 = gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength3);

        endFadeScreen.SetActive(true);
        float animLength = endFadeScreen.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength);

        endChoices.SetActive(true);
        float animLength2 = endChoices.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animLength2);

        gameObject.SetActive(false);
    }
}
