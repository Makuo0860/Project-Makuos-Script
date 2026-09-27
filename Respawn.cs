using UnityEngine;

public class Respawn : MonoBehaviour
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
        Invoke("RespawnEnemy", 10f);
    }

    public void RespawnEnemy()
    {
        respawnItem.SetActive(true);
    }
}
