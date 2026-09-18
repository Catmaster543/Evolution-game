using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public float speed;

    private float realSpeed;

    [SerializeField] private int damage;

    [SerializeField] public int value;

    private SplineAnimate spline;

    private SplineContainer splineContainer;

    private EnemyWaveManager waveManager;

    private Balance balance;

    public int order;

    private Bullet lastBullet;

    private Map map;
    
    void Start()
    {
        splineContainer = GameObject.FindGameObjectWithTag("Spline").GetComponent<SplineContainer>();
        waveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();
        balance = GameObject.FindGameObjectWithTag("Balance").GetComponent<Balance>();
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();

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
            lastBullet.originTower.enemiesInRange.Remove(gameObject);
            balance.balance += value;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            lastBullet = bullet;
            hp -= bullet.damage;
            map.theSoundOfDoom.Play();
            Destroy(bullet.gameObject);
        }
        else if (collision.gameObject.tag == "End")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.hp -= damage;
            waveManager.enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
