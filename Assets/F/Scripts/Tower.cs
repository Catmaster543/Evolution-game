using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    void Start()
    {
        enemyWaveManager = GameObject.FindGameObjectWithTag("Wave manager").GetComponent<EnemyWaveManager>();
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
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            GameObject target = GameObject.FindGameObjectWithTag("Enemy");

            GameObject[] enemiesObj = GameObject.FindGameObjectsWithTag("Enemy");
            List<Enemy> enemies = new();
            foreach (GameObject enemy in enemiesObj)
            {
                enemies.Add(enemy.GetComponent<Enemy>());
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy.order <= i)
                {
                    target = enemy.gameObject;
                    i = enemy.order;
                }
            }

            Vector3 dest = target.transform.position;
            dest.z = 0;


            float angle = Mathf.Atan2(target.transform.position.x, target.transform.position.y) * Mathf.Rad2Deg;


            Vector3 direction = (dest - gameObject.transform.position).normalized;

            GameObject proj = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0, 0, angle));

            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * shootSpeed;
        }
    }
}
