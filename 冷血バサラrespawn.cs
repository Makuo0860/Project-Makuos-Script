using UnityEngine;
using System.Collections;

public class 冷血バサラrespawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [Header("Score")]
    [SerializeField] private int appearScore = 1000;

    [Header("Appear Time")]
    [SerializeField] private float minAppearTime = 5f;
    [SerializeField] private float maxAppearTime = 15f;

    [Header("Stay Time")]
    [SerializeField] private float minStayTime = 10f;
    [SerializeField] private float maxStayTime = 20f;

    private bool running = false;

    void Update()
    {
        if (!running &&
            ScoreManager.Instance.Score >= appearScore)
        {
            running = true;
            StartCoroutine(SpawnLoop());
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(minAppearTime, maxAppearTime));

            enemy.SetActive(true);

            yield return new WaitForSeconds(
                Random.Range(minStayTime, maxStayTime));

            enemy.SetActive(false);
        }
    }
}
