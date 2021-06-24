using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : MonoBehaviour
{
    public static EffectFactory instance { get; private set; }

    public CameraShake shaker;
    public Camera mainCam;


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
    
}
