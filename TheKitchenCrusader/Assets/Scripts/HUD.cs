using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// HUD
/// </summary>
public class HUD : MonoBehaviour
{
    // Table Text Fields
    [SerializeField]
    TextMeshProUGUI TablesLeft;
    string tableText = "Tables Left: ";
    int numTables = 3;

    [SerializeField]
    TextMeshProUGUI recordText;
    string tables = "You have defended: ";
    string results = "tables in the level";
    // Timer fields
    [SerializeField]
    TMP_Text timeText;
    string timerText = "Timer: ";
    public float timeRemaining = 0f;
    public bool timeIsRunning = false;

    // Menu Manager field
    MenuManager mm;
    [SerializeField]
    GameObject WinMenu;
    [SerializeField]
    GameObject LoseMenu;
    // Start is called before the first frame update
    void Start()
    {
        // Set field to component
        mm = GetComponent<MenuManager>();
      // set text property of text field into number of tables
      // in the game   

        TablesLeft.text = tableText + numTables.ToString();
        timeIsRunning = true;
        timeText.SetText("WIP");
    }

    public void RemoveFurniture()
    {
        // Decrement Number of tables
        numTables -= 1;

        // Update text
        TablesLeft.text = tableText + numTables.ToString();

        // Check if All Tables are destroyed
        if (numTables <= 0)
        {
            SpawnLoseMenu();
        }
    }
    // Update is called once per frame
    void Update()
    {
        // If the timer has started
        if (timeIsRunning)
        {
            // if time hasn't run out
            if (timeRemaining >= 0)
            {
                Debug.Log("Tick");
                // count down
                timeRemaining -=  Time.deltaTime;
                updateTimer(timeRemaining);
            }
            else
            {
                // declare that time is up
                Debug.Log("Time is up.");
                timeRemaining = 0;
                timeIsRunning = false;
                // Check to see if the perfered ammount of tables are
                // protected
                if (numTables > 1)
                {
                    // Player wins
                    SpawnWinMenu();
                }
                else
                {
                    // player loses game
                    SpawnLoseMenu();
                }
                
            }
        }
    }
    /// <summary>
    /// Instantiate Win Message
    /// </summary>
    public void SpawnWinMenu()
    {
        WinMenu.SetActive(true);
        Time.timeScale = 0f;
        // set player's results
        recordText.SetText(tables + numTables + "/3 " + results);
    }
    /// <summary>
    /// Instantiate Lose Menu
    /// </summary>
    public void SpawnLoseMenu()
    {
        //if (LoseMenu.activeInHierarchy == false)
        //{
        LoseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    /// <summary>
    /// Update and display timer text every frame
    /// </summary>
    /// <param name="currentTime"></param>
    void updateTimer(float currentTime)
    {
        // increment the current time by one second
        currentTime += 1f;
        // create seconds
        float seconds = Mathf.FloorToInt(currentTime / 60f);
        timeText.SetText("Timer: " + timeRemaining);
    }

}
