using System;
using UnityEngine;

public class ClockBehavior : MonoBehaviour
{
    [SerializeField]
    Transform hoursPivot, minutesPivot, secondsPivot;
    const float hoursToDegrees = 30;
    const float minutesToDegrees = 6;

    void Update()
    {
        var now = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hoursToDegrees * (float)now.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegrees * (float)now.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegrees * (float)now.TotalSeconds);

    }

    void Start()
    {
        Application.targetFrameRate = 30;
    }

}