using UnityEngine;

public class マクシミリミリアンTitleMove : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 500f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (transform.position.z >= 2000f)
        {
            transform.position = new Vector3(496.9f, 538.13f, 627f);
            gameObject.SetActive(false);
        }
    }
}
