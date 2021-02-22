using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    private float SECONDS_IN_A_DAY = 86400;

    [SerializeField]
    private Transform rotation;

    [SerializeField]
    private Light sun;
    private float sunIntensity;

    [SerializeField]
    private float sunVariation = 1.5f;

    [SerializeField]
    private float sunBaseIntensity = 1f;

    [SerializeField]
    private float _targetDayLength = 0.5f; //Length of day in minutes

    [SerializeField]
    private Gradient sunGradient;
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay; //Percentage of the day passed
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }

    [SerializeField]
    private int _dayNumber = 0; //Days passed
    public int dayNumber
    {
        get
        {
            return _dayNumber;
        }
    }
    [SerializeField]
    private int _yearNumber = 0;
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }

    private float _timeScale = 100f;
    [SerializeField]
    private int _yearLength = 100;
    public float yearLength
    {
        get
        {
            return _yearLength;
        }
    }

    public bool pause = false;

    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60); 
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / SECONDS_IN_A_DAY; //Second of the day

        if (_timeOfDay > 1) // Day completed
        {
            _dayNumber++;
            _timeOfDay -= 1;
        } 

        if( _dayNumber > _yearLength) //New Year
        {
            _yearNumber++;
            _dayNumber = 0;
        }
    }

    private void SunRotation() 
    {
        float sun = timeOfDay * 360f;
        rotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sun));
    }

    private void SunIntensity()
    {
        sunIntensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        sunIntensity = Mathf.Clamp01(sunIntensity);

        sun.intensity = sunIntensity * sunVariation + sunBaseIntensity;
    }

    private void SunColor()
    {
        sun.color = sunGradient.Evaluate(sunIntensity);
    }

    private void Update()
    {
        if(!pause)
        {
            UpdateTimeScale();
            UpdateTime();
        }
        SunRotation();
        SunIntensity();
        SunColor();
    }
}