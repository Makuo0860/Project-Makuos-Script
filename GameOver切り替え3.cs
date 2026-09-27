using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver切り替え3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Change", 5f);
    }

    void Change()
    {
        SceneManager.LoadScene("Title");
    }
}
