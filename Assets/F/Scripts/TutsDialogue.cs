using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TutDialogue : MonoBehaviour
{   
    int i = 0;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;
    public bool isTyping;

    bool gone = false;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }
    void Update()
    {
        if (i == 6 && !gone)
        {
            gameObject.SetActive(false);
            gone = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
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
