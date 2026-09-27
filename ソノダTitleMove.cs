using UnityEngine;

public class ソノダTitleMove : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 250f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (transform.position.z >= 2000f)
        {
            transform.position = new Vector3(521f, 521f, 627f);
            gameObject.SetActive(false);
        }
    }
}
