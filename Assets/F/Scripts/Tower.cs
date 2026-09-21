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
    [SerializeField] public int level;
    [SerializeField] private float shootSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private float range;
    [SerializeField] public AudioSource shootSound;
    [SerializeField] float accuracy;
    [SerializeField] float damage;

    [SerializeField] private Upgrade upgrade1;
    [SerializeField] private Upgrade upgrade2;
    [SerializeField] private Upgrade upgrade3;

    private float clocker;

    private float realAccuracy;
    private float realDamage;
    private float realShootRate;
    private float realRange;

    private float realRealAccuracy;

    private EnemyWaveManager enemyWaveManager;

    public List<GameObject> enemiesInRange = new();

    private Vector3 posRange;
    CircleCollider2D rangeCollider;
    void Start()
    {
        enemyWaveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();
        rangeCollider = gameObject.GetComponent<CircleCollider2D>();
        shootSound = gameObject.GetComponent<AudioSource>();

        upgrade1 = new GameObject().AddComponent<Upgrade>();
        upgrade1 = new GameObject().AddComponent<Upgrade>();
        upgrade1 = new GameObject().AddComponent<Upgrade>();

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
        if (enemiesInRange.Count != 0)
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

            if (upgrade1 != null)
            {
                if (upgrade1.accuracyMult != 0)
                {
                    realAccuracy += accuracy * upgrade1.accuracyMult;
                }
                if (upgrade1.damageMult != 0)
                {
                    realDamage += damage * upgrade1.damageMult;
                }
                if (upgrade1.rangeMult != 0)
                {
                    realRange += range * upgrade1.rangeMult;
                }
                if (upgrade1.shootRateMult != 0)
                {
                    realShootRate += shootRate * upgrade1.shootRateMult;
                }
            }
            if (upgrade2 != null)
            {
                if (upgrade2.accuracyMult != 0)
                {
                    realAccuracy += accuracy * upgrade2.accuracyMult;
                }
                if (upgrade2.damageMult != 0)
                {
                    realDamage += damage * upgrade2.damageMult;
                }
                if (upgrade2.rangeMult != 0)
                {
                    realRange += range * upgrade2.rangeMult;
                }
                if (upgrade2.shootRateMult != 0)
                {
                    realShootRate += shootRate * upgrade2.shootRateMult;
                }
            }
            if (upgrade3 != null)
            {
                if (upgrade3.accuracyMult != 0)
                {
                    realAccuracy += accuracy * upgrade3.accuracyMult;
                }
                if (upgrade3.damageMult != 0)
                {
                    realDamage += damage * upgrade3.damageMult;
                }
                if (upgrade3.rangeMult != 0)
                {
                    realRange += range * upgrade3.rangeMult;
                }
                if (upgrade3.shootRateMult != 0)
                {
                    realShootRate += shootRate * upgrade3.shootRateMult;
                }
            }

            Vector3 dest = target.transform.position;
            dest.z = 0;

            realRealAccuracy = Random.Range(-1f * accuracy, 1f * accuracy);

            float angle = Mathf.Atan2(target.transform.position.x, target.transform.position.y * Mathf.Rad2Deg);

            int rng = Random.Range(1, 4);

            Vector3 direction = new Vector3();

            if (rng == 1)
            {
                direction = new Vector3(dest.x + realRealAccuracy - gameObject.transform.position.x, dest.y + realRealAccuracy - gameObject.transform.position.y).normalized;
                Debug.Log($"Target is at {direction}, real enemy position is: {target.transform.position}, the calculated accuracy is: {realAccuracy} x using: {dest.x} + {realRealAccuracy} - {gameObject.transform.position.x}");
            }
            else if (rng == 2)
            {
                direction = new Vector3(dest.x + realRealAccuracy - gameObject.transform.position.x, dest.y - realAccuracy - gameObject.transform.position.y).normalized;
                Debug.Log($"Target is at {direction}, real enemy position is: {target.transform.position}, the calculated accuracy is: {realRealAccuracy}");
            }
            else if (rng == 3)
            {
                direction = new Vector3(dest.x - realRealAccuracy - gameObject.transform.position.x, dest.y + realAccuracy - gameObject.transform.position.y).normalized;
                Debug.Log($"Target is at {direction}, real enemy position is: {target.transform.position}, the calculated accuracy is: {realRealAccuracy}");
            }
            else if (rng == 4)
            {
                direction = new Vector3(dest.x - realRealAccuracy - gameObject.transform.position.x, dest.y - realAccuracy - gameObject.transform.position.y).normalized;
                Debug.Log($"Target is at {direction}, real enemy position is: {target.transform.position}, the calculated accuracy is: {realRealAccuracy}");
            }
 
            GameObject proj = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, angle));

            shootSound.Play();

            Bullet bbullet = proj.GetComponent<Bullet>();
            bbullet.originTower = this;
            bbullet.damage = damage;

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
