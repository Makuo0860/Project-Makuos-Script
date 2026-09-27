using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera mainCamera;
    public Camera subCamera;

    private bool mainCameraOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       mainCamera.enabled = true;
        subCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)  && mainCameraOn == true)
        {
            mainCamera.enabled =false;
            subCamera.enabled = true;

            mainCameraOn = false;
        }
        else if(Input.GetKeyDown(KeyCode.Z) && mainCameraOn == false)
        {
            mainCamera.enabled = true;
            subCamera.enabled = false;

            mainCameraOn = true;
        }
    }
}
