using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyFlight flight;

    public Transform player;

    public Transform[] patrolPoints;

    public float findDistance = 300f;

    public float loseDistance = 500f;

    public float arriveDistance = 50f;

    int currentPoint = 0;


    bool chasing = false;

    [Header("AI")]
    [SerializeField] private Transform target;

    [Header("Weapon")]
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform missileSpawn;
    [SerializeField] private float fireInterval = 2f;
    [SerializeField] private float fireDistance = 300f;
    [SerializeField] private float fireAngle = 15f;
    [Header("Lock On")]
    [SerializeField] private float lockOnTime = 1.5f;
    [SerializeField] private AudioClip warningClip;
    [SerializeField] private AudioSource warningAudio;

    private float lockTimer = 0f;
    private bool isLocked = false;
    private bool warningPlayed = false;

    private float fireTimer;

    void Start()
    {
        if (flight == null)
            flight = GetComponent<EnemyFlight>();

        if (patrolPoints.Length > 0)
            flight.target = patrolPoints[0];

        warningAudio.volume = 0.5f;
    }

    void Update()
    {
        float dist =
            Vector3.Distance(transform.position,
            player.position);

        if (!chasing)
        {
            if (Vector3.Distance(
                transform.position,
                patrolPoints[currentPoint].position)
                < arriveDistance)
            {
                currentPoint++;

                if (currentPoint >= patrolPoints.Length)
                    currentPoint = 0;

                flight.target =
                    patrolPoints[currentPoint];
            }

            if (dist < findDistance)
            {
                chasing = true;
                flight.target = player;
            }
        }
        else
        {
            flight.target = player;

            if (dist > loseDistance)
            {
                chasing = false;

                flight.target =
                    patrolPoints[currentPoint];
            }
        }
        fireTimer += Time.deltaTime;

        Attack();
    }

    private void Attack()
    {
        if (player == null)
            return;

        Vector3 dir = player.position - transform.position;

        float distance = dir.magnitude;

        float angle = Vector3.Angle(transform.forward, dir);

        bool canLock =
            distance <= fireDistance &&
            angle <= fireAngle;

        if (canLock)
        {
            lockTimer += Time.deltaTime;

            if (!warningPlayed)
            {
                warningAudio.PlayOneShot(warningClip);
                warningPlayed = true;
            }

            if (lockTimer >= lockOnTime)
            {
                isLocked = true;
            }
        }
        else
        {
            lockTimer = 0f;
            isLocked = false;

            // ‰¹‚ÍŽ~‚ß‚È‚¢
            warningPlayed = false;
        }

        if (!isLocked)
            return;

        if (fireTimer < fireInterval)
            return;

        fireTimer = 0f;
        lockTimer = 0f;
        isLocked = false;
        warningPlayed = false;


        FireMissile();
    }

    private void FireMissile()
    {
        Debug.Log("ƒ~ƒTƒCƒ‹”­ŽË");
        GameObject missile = Instantiate(
            missilePrefab,
            missileSpawn.position,
            missileSpawn.rotation);

        EnemyHomingMissile homing = missile.GetComponent<EnemyHomingMissile>();

        if (homing != null)
        {
            homing.Target = player;
        }
    }

    private void OnDestroy()
    {
        if (warningAudio != null && warningAudio.isPlaying)
        {
            warningAudio.Stop();
        }
    }
}