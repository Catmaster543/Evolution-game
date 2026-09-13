using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float maxhp;

    [SerializeField] private TextMeshProUGUI hpText;
    bool sayImDead = true;

    [SerializeField] GameObject gameoverScreen;
    void Update()
    {
        if (hp <= 0)
        {
            if (sayImDead)
            {
                gameoverScreen.SetActive(true);
                sayImDead = false;
            }
        }

        hpText.text = hp.ToString();
    }
}
