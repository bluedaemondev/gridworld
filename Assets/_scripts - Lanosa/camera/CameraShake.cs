using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;


    Vector3 originalPos;

    void Awake()
    {
        camTransform = GetComponent<Transform>();
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    public void ShakeCameraFor(float time, float scale)
    {
        StopAllCoroutines();
        StartCoroutine(CamShakeRoutine(time, scale));
    }

    IEnumerator CamShakeRoutine(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            camTransform.position = originalPos + Random.insideUnitSphere * amount;

            duration -= Time.deltaTime;

            yield return null;
        }

        camTransform.position = originalPos;
    }

    #region Old
    //private float shakeDuration = 0f;

    //public float shakeAmount = 0.7f;
    //public float decreaseFactor = 1.0f;

    //void Update()
    //{
    //    if (shakeDuration > 0)
    //    {
    //        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

    //        shakeDuration -= Time.deltaTime * decreaseFactor;
    //    }
    //    else
    //    {
    //        shakeDuration = 0f;
    //        camTransform.localPosition = originalPos;
    //    }
    //}
    #endregion
}