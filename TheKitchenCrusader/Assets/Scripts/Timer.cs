using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    // set up time fields
    [SerializeField]
    TMP_Text timeText;
    string timerText = "Timer: ";
    public float timeRemaining = 0;
    public bool timeIsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        timeText.SetText("Working");
    }

    // Update is called once per frame
    void Update()
    {
        // If the timer has started
         if(timeIsRunning)
         {
            // if time hasn't run out
            if (timeRemaining >= 0)
            {
                Debug.Log("Tick");
                // count down
                timeRemaining -= Time.deltaTime;
                updateTimer(timeRemaining);
            }
            else
            {
                // declare that time is up
                Debug.Log("Time is up.");
                timeRemaining = 0;
                timeIsRunning = false;
            }
         }
    }
    /// <summary>
    /// Update and display timer text every frame
    /// </summary>
    /// <param name="currentTime"></param>
    void updateTimer(float currentTime)
    {
        // increment the current time by one second
        currentTime += 1;
        // create seconds
        float seconds = Mathf.FloorToInt(currentTime / 60);

        timeText.text = string.Format("Timer:", seconds);
    }
}
