using UnityEngine;



[RequireComponent(typeof(Rigidbody))]

public class TitleAirplaneController : MonoBehaviour

{

    [Header("Rotation")]

    [SerializeField] private float pitchSpeed = 70f;

    [SerializeField] private float climbSpeed = 15f;



    private Rigidbody rb;



    private void Start()

    {

        rb = GetComponent<Rigidbody>();

    }



    private void Update()

    {



        // 上を向けば上昇、下を向けば下降

        transform.position += Vector3.up * transform.forward.y * climbSpeed * Time.deltaTime;



        // ピッチ

        float pitch = 0f;

        if (Input.GetKey(KeyCode.W))

            pitch = 1f;

        else if (Input.GetKey(KeyCode.S))

            pitch = -1f;



        transform.Rotate(

            pitch * pitchSpeed * Time.deltaTime, 0f, 0f, Space.Self);

    }

}