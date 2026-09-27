using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class AirplaneController : MonoBehaviour
{
    [Header("Engine")]
    [SerializeField] private float maxSpeed = 250f;
    [SerializeField] private float minSpeed = 60f;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float boostMultiplier = 1.5f;

    [Header("Rotation")]
    [SerializeField] private float pitchSpeed = 70f;
    [SerializeField] private float rollSpeed = 120f;
    [SerializeField] private float yawSpeed = 35f;

    [Header("Flight")]
    [SerializeField] private float liftPower = 12f;
    [SerializeField] private float drag = 0.002f;

    [SerializeField] private TMP_Text speedText;

    private Rigidbody rb;

    private float throttle = 0.5f;
    private float currentSpeed;

    private void Start()
    {
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        currentSpeed = 120f;
    }

    private void Update()
    {
        //------------------------
        // ブースト
        //------------------------

        float targetSpeed = minSpeed;

        // Shiftを押している間だけブースト
        if (Input.GetKey(KeyCode.LeftShift))
        {
            targetSpeed = maxSpeed;
        }

        // 徐々に目標速度へ近づく
        currentSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            acceleration * Time.deltaTime);

        //------------------------
        // ピッチ
        //------------------------

        float pitch = 0f;

        if (Input.GetKey(KeyCode.W))
            pitch = 1f;
        else if (Input.GetKey(KeyCode.S))
            pitch = -1f;

        //------------------------
        // ロール
        //------------------------

        float roll = 0f;

        if (Input.GetKey(KeyCode.A))
            roll = 1f;
        else if (Input.GetKey(KeyCode.D))
            roll = -1f;

        //------------------------
        // ヨー
        //------------------------

        float yaw = 0f;

        if (Input.GetKey(KeyCode.Q))
            yaw = -1f;
        else if (Input.GetKey(KeyCode.E))
            yaw = 1f;

        //------------------------
        // 機体回転
        //------------------------

        transform.Rotate(
            pitch * pitchSpeed * Time.deltaTime,
            yaw * yawSpeed * Time.deltaTime,
            roll * rollSpeed * Time.deltaTime,
            Space.Self);

        if (speedText != null)
        {
            speedText.text = $"SPEED : {Mathf.RoundToInt(currentSpeed)} km/h";
        }
    }

    private void FixedUpdate()
    {
        //------------------------
        // 前進
        //------------------------

        rb.linearVelocity = transform.forward * currentSpeed;

        //------------------------
        // 揚力
        //------------------------

        rb.AddForce(
            transform.up * currentSpeed * liftPower,
            ForceMode.Force);

        //------------------------
        // 抗力
        //------------------------

        rb.AddForce(
            -rb.linearVelocity * drag,
            ForceMode.Force);
    }
}