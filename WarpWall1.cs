using UnityEngine;

public class WarpWall1 : MonoBehaviour
{
    public Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}
