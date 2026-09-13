using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float balance;
    public TextMeshProUGUI balanceText;

    void Update()
    {
        balanceText.text = balance.ToString();
    }
}
