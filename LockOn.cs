using UnityEngine;

public class LockOn : MonoBehaviour
{
    public Transform currentTarget;
    public float lockDistance = 500f;

    public bool IsLocked
    {
        get { return currentTarget != null; }
    }

    void Update()
    {
        if (currentTarget != null)
        {
            float distance = Vector3.Distance(transform.position, currentTarget.position);

            if (distance > lockDistance)
            {
                currentTarget = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            LockEnemy();
        }
    }

    void LockEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        currentTarget = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < lockDistance && distance < minDistance)
            {
                minDistance = distance;
                currentTarget = enemy.transform;
            }
        }

        if (currentTarget != null)
        {
            Debug.Log("ロックオン: " + currentTarget.name);
        }
        else
        {
            Debug.Log("ロックオンできる敵がいません");
        }
    }
}