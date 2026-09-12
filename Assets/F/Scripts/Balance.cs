using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public int balance;
    public TextMeshProUGUI balanceText;
    void Start()
    {
        
    }

    void Update()
    {
        balanceText.text = balance.ToString();
    }
}
