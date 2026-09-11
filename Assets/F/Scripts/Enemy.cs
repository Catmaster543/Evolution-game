using UnityEngine;
using UnityEngine.Splines;

public class Enemy : MonoBehaviour
{
    public float hp;
    public float maxHp;

    public float speed;

    private float realSpeed;

    private SplineAnimate spline;

    private SplineContainer splineContainer;
    void Start()
    {
        splineContainer = GameObject.FindGameObjectWithTag("Spline1").GetComponent<SplineContainer>();

        spline = gameObject.GetComponent<SplineAnimate>();

        spline.Container = splineContainer;

        realSpeed = 15 * speed;

        spline.Duration = realSpeed;

        spline.Play();
    }
}
