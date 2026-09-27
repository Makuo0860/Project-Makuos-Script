using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public GameObject effectPrefab;
    [SerializeField] private Respawn respawn;
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerMissle")
        {
            GameObject effect = Instantiate(effectPrefab,transform.position,Quaternion.identity);
            transform.position = respawnPoint;
            Destroy(effect,2f);
            ScoreManager.Instance.AddScore(score);
            gameObject.SetActive(false);
            respawn.Wait();
        }
    }
}
