using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour
{   
    int i = 0;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;
    public bool isTyping;

    public GameObject splaScreen;
    public GameObject menu;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }
    void Update()
    {
        if (i == 8)
        {
            splaScreen.SetActive(false);
            menu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            i++;
            NextLine();
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    System.Collections.IEnumerator TypeLine()
    {
        if (isTyping)
        {
            textComponent.text = lines[index-1];
            isTyping = false;
            StopCoroutine(TypeLine());
        }
        else
        {
            isTyping = true;
            textComponent.text = string.Empty;

            foreach (char c in lines[index].ToCharArray())
            {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
            isTyping = false;
        }
    }
    public void NextLine()
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
