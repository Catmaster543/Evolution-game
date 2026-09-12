using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] public string treeType;
    [SerializeField] private int level;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private float range;

    private float clocker;
    private EnemyWaveManager enemyWaveManager;

    public List<GameObject> enemiesInRange = new();

    private Vector3 posRange;
    CircleCollider2D rangeCollider;
    void Start()
    {
        enemyWaveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();
        rangeCollider = gameObject.GetComponent<CircleCollider2D>();

        rangeCollider.radius = range;

        posRange = new Vector3(transform.position.x + range, transform.position.y + range);
    }

    void Update()
    {
        clocker += Time.deltaTime;

        if (clocker >= shootRate)
        {
            clocker = 0;

            Shoot();
        }
    }

    int i = 1000;

    private void Shoot()
    {
        if (enemiesInRange != null)
        {
            GameObject target = null;
            foreach (GameObject enemy in enemiesInRange)
            {
                if (enemy.GetComponent<Enemy>().order <= i)
                {
                    target = enemy.gameObject;
                    i = enemy.GetComponent<Enemy>().order;
                }
            }

            Vector3 dest = target.transform.position;
            dest.z = 0;


            float angle = Mathf.Atan2(target.transform.position.x, target.transform.position.y) * Mathf.Rad2Deg;


            Vector3 direction = (dest - gameObject.transform.position).normalized;

            GameObject proj = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, angle));

            Bullet bbullet = proj.GetComponent<Bullet>();
            bbullet.originTower = this;

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * shootSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemiesInRange.Remove(collision.gameObject);
            i = 1000;
        }
    }
}
