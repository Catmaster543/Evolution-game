using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float maxhp;

    [SerializeField] private TextMeshProUGUI hpText;
    bool sayImDead = true;
    void Update()
    {
        if (hp <= 0)
        {
            if (sayImDead)
            {
                Debug.Log("I'm died X(");
                sayImDead = false;
            }
            // Game over
        }

        hpText.text = hp.ToString();
    }
}
