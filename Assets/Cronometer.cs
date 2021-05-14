using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cronometer : MonoBehaviour
{
    private float _currentTimer;
    private float _timeBetweenTicks;

    public event Action<float> timedEvents;

    public float CurrentTimer { get => _currentTimer; }

    private void Update()
    {
        OnTimePass();
    }

    public void Init(float timeTicker)
    {
        this._currentTimer = 0;
        this._timeBetweenTicks = timeTicker;

        timedEvents += EventTickerTest;
    }

    public void OnTimePass()
    {
        _currentTimer += Time.unscaledDeltaTime;

        if (timedEvents != null && Mathf.Approximately(_currentTimer % _timeBetweenTicks, 0))
            timedEvents(_currentTimer);
    }

    private void EventTickerTest(float currenttimer)
    {
        Debug.Log("call ticker , curr = " + currenttimer);
    }
}
