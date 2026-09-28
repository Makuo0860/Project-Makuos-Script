using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EnemyCanvas : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private float displayTime = 3f;
    [SerializeField] private AudioClip subtitleSE;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(ShowCanvas());
    }

    IEnumerator ShowCanvas()
    {
        canvas.SetActive(true);
        if (subtitleSE != null)
        {
            audioSource.PlayOneShot(subtitleSE);
        }

        yield return new WaitForSeconds(displayTime);
        canvas.SetActive(false);
    }
}