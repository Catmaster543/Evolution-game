using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] private int doomsDay;

    private float clocker;
    void Update()
    {
        clocker += Time.deltaTime;

        if (clocker >= doomsDay)
        {
            Destroy(gameObject);
        }
    }
}
