using UnityEngine;

public class BrosHP : MonoBehaviour
{
    public GameObject effectPrefab;
    [SerializeField] private RespawnBro respawn;
    [SerializeField] private Vector3 respawnPoint;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 respawnOffset = new Vector3(-30f, 10f, -50f);

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
        if (other.gameObject.tag == "EnemyMissle")
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            transform.position = player.position + player.TransformDirection(respawnOffset);
            Destroy(effect, 2f);
            gameObject.SetActive(false);
            respawn.Wait();
        }
    }
}
