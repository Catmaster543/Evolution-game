using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TutsDialogue : MonoBehaviour
{   
    int i = 0;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    private float textSpeed = 0.05f;
    private int index;

    bool gone = false;

    void Start()
    {
        textComponent.text = string.Empty;
        
        StartDialogue();
    }
    void Update()
    {
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
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
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
