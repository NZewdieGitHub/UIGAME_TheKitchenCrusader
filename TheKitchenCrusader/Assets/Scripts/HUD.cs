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
        
    }
    /// <summary>
    /// Instantiate Win Message
    /// </summary>
    public void SpawnWinMenu()
    {
        WinMenu.SetActive(true);
        Time.timeScale = 0f;
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
}
