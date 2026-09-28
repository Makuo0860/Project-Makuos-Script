using UnityEngine;

public class 揺れる : MonoBehaviour
{
    [Header("Float")]
    [SerializeField] private float height = 2.5f;       // 上下の揺れ幅
    [SerializeField] private float floatSpeed = 0.25f;  // 揺れる速さ

    [Header("Pitch")]
    [SerializeField] private float pitchAngle = 2f;     // 機首の上下角度

    private Vector3 startPos;
    private Quaternion startRot;

    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    void Update()
    {
        float wave = Mathf.Sin(Time.time * 0.25f) * 2f + Mathf.Sin(Time.time * 0.57f) * 0.5f;

        transform.position = startPos + Vector3.up * wave;

        transform.rotation = startRot * Quaternion.Euler(wave * pitchAngle, 0f, 0f);
    }
}