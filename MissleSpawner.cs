using UnityEngine;
using System.Collections;
using TMPro;

public class MissleSpawner : MonoBehaviour
{
    [SerializeField] private LockOn lockOn;
    [SerializeField] private GameObject prefab;
    [SerializeField] private float interval = 0.1f;
    [SerializeField] private float coolTime = 1f;
    [SerializeField] private TMP_Text missileText;
    [SerializeField] private Transform leftMuzzle;
    [SerializeField] private Transform rightMuzzle;

    private bool useLeft = true;

    private bool canFire = true;

    private float currentCoolTime = 0f;

    private bool isSpawning = false;
    private Transform thisTransform;
    private WaitForSeconds intervalWait;

    public AudioClip shotSound;

    void Start()
    {
        thisTransform = transform;
        intervalWait = new WaitForSeconds(interval);

       // lockOn = GetComponent<LockOn>();

    }

    void Update()
    {
        if (!canFire)
        {
            currentCoolTime -= Time.deltaTime;

            if (missileText != null)
            {
                missileText.text = currentCoolTime.ToString("F1");
            }
        }
        else
        {
            if (missileText != null)
            {
                if (lockOn != null && lockOn.IsLocked)
                {
                    missileText.text = "READY";
                }
                else
                {
                    missileText.text = "";
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) &&
            canFire &&
            !isSpawning &&
            lockOn != null &&
            lockOn.currentTarget != null)
        {
            StartCoroutine(SpawnMissle());
            StartCoroutine(CoolTime());

            if (shotSound != null)
            {
                AudioSource.PlayClipAtPoint(
                    shotSound,
                    transform.position
                );
            }
        }
    }

    IEnumerator SpawnMissle()
    {
        isSpawning = true;

        HomingMissile leftMissile =
            Instantiate(prefab, leftMuzzle.position, leftMuzzle.rotation)
            .GetComponent<HomingMissile>();

        leftMissile.Target = lockOn.currentTarget;

        HomingMissile rightMissile =
            Instantiate(prefab, rightMuzzle.position, rightMuzzle.rotation)
            .GetComponent<HomingMissile>();

        rightMissile.Target = lockOn.currentTarget;

        yield return null;

        isSpawning = false;
    }

    IEnumerator CoolTime()
    {
        canFire = false;

        currentCoolTime = coolTime;

        yield return new WaitForSeconds(coolTime);

        canFire = true;
    }
}