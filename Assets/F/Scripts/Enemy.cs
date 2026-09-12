using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public float speed;

    private float realSpeed;

    [SerializeField] private int damage;

    private SplineAnimate spline;

    private SplineContainer splineContainer;

    private EnemyWaveManager waveManager;

    public int order;
    
    void Start()
    {
        splineContainer = GameObject.FindGameObjectWithTag("Spline").GetComponent<SplineContainer>();
        waveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();

        spline = gameObject.GetComponent<SplineAnimate>();

        spline.Container = splineContainer;

        realSpeed = 15 * speed;

        spline.Duration = realSpeed;

        spline.Play();
    }

    void Update()
    {
        if (hp <= 0)
        {
            waveManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit!");
        Debug.Log(collision.name);
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            hp -= bullet.damage;
            Destroy(bullet.gameObject);
        }
        else if (collision.gameObject.tag == "End")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.hp -= damage;
            hp = 0;
        }
    }
}
