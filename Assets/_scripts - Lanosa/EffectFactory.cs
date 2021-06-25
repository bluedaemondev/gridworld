using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : MonoBehaviour
{
    public static EffectFactory instance { get; private set; }

    public CameraShake shaker;
    public Camera mainCam;

    float originalFov;


    //public struct CameraShakeInfo
    //{
    //    public float amplitude { get; set; }
    //    public float frequency { get; set; }
    //    public float time { get; set; }

    //    public CameraShakeInfo(float amp, float freq, float timeEffect)
    //    {
    //        this.amplitude = amp;
    //        this.frequency = freq;
    //        this.time = timeEffect;
    //    }
    //}


    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);

        if (mainCam == null)
            mainCam = Camera.main;
        if (shaker == null)
            shaker = mainCam.GetComponent<CameraShake>();

        originalFov = mainCam.fieldOfView;
    }

    public void InstantiateEffectAt(GameObject prefabEffect, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject spwn = Instantiate(prefabEffect, position, rotation, parent);
        if (spwn.GetComponent<ParticleSystem>())
        {
            var pSys = spwn.GetComponent<ParticleSystem>();

            pSys.Play();
            //pSys.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Destroy(pSys.gameObject, pSys.main.duration);

        }
    }

    public void ShakeCamera(float time, float scale)
    {
        this.shaker.ShakeCameraFor(time, scale);
    }
    /// <summary>
    /// Recibe +10, -10, o el numero que sea para sumar al FOV
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="time"></param>
    public void ZoomCamera(float amount, float time)
    {
        StartCoroutine(SetCameraFov(amount, time));
    }
    IEnumerator SetCameraFov(float amount, float time)
    {
        this.mainCam.fieldOfView = originalFov - amount;
        yield return new WaitForSecondsRealtime(time);
        this.mainCam.fieldOfView = originalFov;
    }

}
