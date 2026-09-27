using UnityEngine;

public class RespawnBro : MonoBehaviour
{
    public GameObject respawnItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Wait()
    {
        Invoke("RespawnEnemy", 15f);
    }

    public void RespawnEnemy()
    {
        respawnItem.SetActive(true);
    }
}
