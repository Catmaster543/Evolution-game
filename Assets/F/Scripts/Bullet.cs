using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] private int doomsDay;

    private float clocker;
    public Tower originTower;
    void Update()
    {
        clocker += Time.deltaTime;

        if (clocker >= doomsDay)
        {
            Destroy(gameObject);
        }
    }
}
