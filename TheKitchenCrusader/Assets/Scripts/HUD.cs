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

    // Ammo Field
    [SerializeField]
    TextMeshProUGUI ammoText;
    public int ammoCount = 25;

    // Menu Manager field
    MenuManager mm;
    [SerializeField]
    GameObject WinMenu;
    [SerializeField]
    GameObject LoseMenu;

    // bool to make sure mustard text never goes negative
    private bool atLimit = false;

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
        ammoText.text = "Mustard Ammo: " + ammoCount.ToString();
    }
    /// <summary>
    /// Decrease table count
    /// </summary>
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
    /// <summary>
    /// Remove Mustard Ammo count
    /// </summary>
    public void RemoveMustardAmmo()
    {
        if (ammoCount > 0)
        {
            ammoCount -= 1;

            // Update text
            ammoText.text = "Mustard Ammo: " + ammoCount.ToString(); 
        }
    }
    /// <summary>
    /// Add ammo to text UI
    /// </summary>
    public void AddMustardAmmo()
    {
             
            ammoCount += 5;

            // Update text
            ammoText.text = "Mustard Ammo: " + ammoCount.ToString();
        
        if (ammoCount >= 25)
        {
            ammoCount = 25;
            // Update text
            ammoText.text = "Mustard Ammo: " + ammoCount.ToString();
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
        timeText.SetText("Timer: " + timeRemaining.ToString("0"));
    }
    /// <summary>
    /// Decreases ammo UI by 5
    /// </summary>
    public void ReduceAmmoCount()
    {
        ammoCount -= 5;
        // Update text
        ammoText.text = "Mustard Ammo: " + ammoCount.ToString();
    }
}
