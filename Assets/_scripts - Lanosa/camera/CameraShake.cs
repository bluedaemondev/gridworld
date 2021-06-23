using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;
    private float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        camTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void ShakeCameraFor(float time)
    {
        this.shakeDuration = time;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}