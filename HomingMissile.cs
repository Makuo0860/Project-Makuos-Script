using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField, Min(0)]
    float time = 1;
    [SerializeField]
    float lifeTime = 2;
    [SerializeField]
    bool limitAcceleration = false;
    [SerializeField, Min(0)]
    float maxAcceleration = 100;
    [SerializeField]
    Vector3 minInitVelocity;
    [SerializeField]
    Vector3 maxInitVelocity;
    [SerializeField] float speed = 500f;
    [SerializeField] float rotateSpeed = 180f;


    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;
    public GameObject effectPrefab;

    public Transform Target
    {
        set
        {
            target = value;
        }
        get
        {
            return target;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thisTransform = transform;
        position = thisTransform.position;

        // ã@éÒï˚å¸Ç÷î≠éÀ
        velocity = transform.forward * 200f;

        // É^Å[ÉQÉbÉgÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç»Ç¢èÍçáÇæÇØàÍî‘ãﬂÇ¢ìGÇíTÇ∑
        if (target == null)
        {
            FindNearestEnemy();
        }

        StartCoroutine(nameof(Timer));
    }

    // Update is called once per frame
void Update()
{
    if (target == null) return;

    Vector3 dir = (target.position - transform.position).normalized;

    Quaternion targetRot = Quaternion.LookRotation(dir);

    transform.rotation = Quaternion.RotateTowards(
        transform.rotation,
        targetRot,
        rotateSpeed * Time.deltaTime);

    transform.position += transform.forward * speed * Time.deltaTime;
}

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);

        Destroy(gameObject);
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        float minDistance = Mathf.Infinity;
        Transform nearest = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy.transform;
            }
        }

        target = nearest;
    }
}
