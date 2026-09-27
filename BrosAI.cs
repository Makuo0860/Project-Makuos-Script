using UnityEngine;

public class BrosAI : MonoBehaviour
{
    [Header("References")]
    public EnemyFlight flight;
    public Transform player;

    [Header("Follow")]
    public Vector3 followOffset = new Vector3(-20, 10, -40);

    [Header("Search")]
    public float searchDistance = 400f;

    [Header("Attack")]
    public float loseDistance = 600f;

    private Transform currentEnemy;

    [SerializeField] GameObject missilePrefab;
    [SerializeField] Transform missileSpawn;

    [SerializeField] float fireInterval = 2f;

    float fireTimer;
    private GameObject followTarget;

    enum State
    {
        Follow,
        Attack
    }

    private State state = State.Follow;

    void Start()
    {
        followTarget = new GameObject("FollowTarget");
        followTarget.transform.position =
    player.position +
    player.TransformDirection(followOffset);

        flight.target = followTarget.transform;
    }

    void Update()
    {
        switch (state)
        {
            case State.Follow:
                FollowPlayer();

                currentEnemy = FindNearestEnemy();

                if (currentEnemy != null)
                {
                    state = State.Attack;
                }
                break;

            case State.Attack:
                AttackEnemy();
                break;
        }
    }

    void FollowPlayer()
    {
        followTarget.transform.position =
            player.position +
            player.TransformDirection(followOffset);

        flight.target = followTarget.transform;
    }

    void AttackEnemy()
    {
        if (currentEnemy == null)
        {
            state = State.Follow;
            return;
        }

        flight.target = currentEnemy;

        float dist =
            Vector3.Distance(transform.position,
                             currentEnemy.position);

        if (dist > loseDistance)
        {
            currentEnemy = null;
            state = State.Follow;
        }

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            fireTimer = 0;

            GameObject missile =
                Instantiate(
                    missilePrefab,
                    missileSpawn.position,
                    missileSpawn.rotation);

            HomingMissile hm =
                missile.GetComponent<HomingMissile>();

            hm.Target = currentEnemy;
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");

        float min = searchDistance;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float d =
                Vector3.Distance(transform.position,
                                 enemy.transform.position);

            if (d < min)
            {
                min = d;
                nearest = enemy.transform;
            }
        }

        return nearest;
    }
}