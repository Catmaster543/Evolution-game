using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float maxhp;

    [SerializeField] private TextMeshProUGUI hpText;
    void Update()
    {
        if (hp < 0)
        {
            Debug.Log("I'm died X(");
            // Game over
        }

        hpText.text = hp.ToString();
    }
}
