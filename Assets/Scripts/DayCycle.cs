// File: DayCycle.cs
// Contributors: Brendan Robinson
// Date Created: 11/12/2019
// Date Last Modified: 11/12/2019

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    #region Constants

    private const float dayStart = 6f;
    private const float dayEnd = 2f;

    #endregion

    #region Fields

    public float timeSpeed;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI timeText;

    private DateTime time;
    private DateTime nextDay;

    private int day = 1;

    #endregion

    #region Methods

    private void Start() {
        time = new DateTime(1, 1, day, 6, 0, 0);
        nextDay = time.AddDays(1);
    }

    private void Update() {
        time = time.AddSeconds(Time.deltaTime * timeSpeed);

        dayText.text = time.ToString("dddd, dd");
        timeText.text = time.ToString("hh: ") + ((time.Minute / 10) * 10).ToString("00") + time.ToString(" tt");

        if (time.Day >= nextDay.Day && time.Hour >= 2f) {
            DayStart();
        }
    }

    private void DayStart() {
        day++;
        time = nextDay;
        nextDay = time.AddDays(1);
    }

    #endregion

}

public struct Crop {

    public List<Sprite> cropStages;
    public List<int> daysBetweenStages;

}