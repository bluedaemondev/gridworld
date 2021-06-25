using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cronometer : MonoBehaviour
{
    //public float currentTimeScale = 1;
    private float _currentTimer;
    private float _timeBetweenTicks;

    //public event Action<float> onTimeScaleChanged;

    public float CurrentTimer { get => _currentTimer; }

    private void Update()
    {
        OnTimePass();
    }

    public void Init(float timeTicker)
    {
        Time.timeScale = 1;

        this._currentTimer = 0;
        this._timeBetweenTicks = timeTicker;

        //onTimeScaleChanged += (timeScale) => { this.currentTimeScale = timeScale; };
    }

    public void OnTimePass()
    {
        _currentTimer += Time.unscaledDeltaTime;

        //if (onTimeScaleChanged != null && Mathf.Approximately(_currentTimer % _timeBetweenTicks, 0))
        //    onTimeScaleChanged(_currentTimer);
    }
}
