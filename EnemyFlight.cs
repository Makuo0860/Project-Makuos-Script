using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyFlight : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Flight")]
    public float speed = 150f;
    public float turnSpeed = 45f;
    private float defaultspeed;

    private Rigidbody rb;

    void Start()
    {
        defaultspeed = speed;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(GameObject.FindGameObjectWithTag("ƒoƒTƒ‰")  != null)
        {
            speed = defaultspeed * 1.5f;
        }
        else
        {
            speed = defaultspeed;
        }
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        rb.MoveRotation(
            Quaternion.RotateTowards(
                rb.rotation,
                targetRotation,
                turnSpeed * Time.fixedDeltaTime));

        rb.linearVelocity = transform.forward * speed;
    }
}