using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    // Player movement setup
    public Rigidbody2D rb2d;
    public float playerSpeed = 8f;
    Vector2 movement;



    // Shooting fields
    public GameObject projectiltePrefab;
    public GameObject projectilePrefab2;
    public float projectileSpeed = 15f;
    public bool ketchupEquipped = true;
    public bool mustardEquipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Used for inputs
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical"); 

        // when player presses space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // fire the bullet
            TempFire();
        }

        // when player presses escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Quit Application
            Application.Quit();
        }

        // used for switching between condiments
        if (Input.GetKeyDown(KeyCode.K))
        {
            // message which condimint is equipped
            Debug.Log("Ketchup is equipped");
            ketchupEquipped = true;
            mustardEquipped = false;
        }
        else if (Input.GetKeyDown(KeyCode.M)) 
        {
            Debug.Log("Mustard is equipped");
            mustardEquipped = true;
            ketchupEquipped= false;
        }
        
    }
    /// <summary>
    /// Executed 50 times a second
    /// </summary>
    private void FixedUpdate()
    {
        // Used for actual movement
        rb2d.MovePosition(rb2d.position + movement * playerSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Fires a temporary bullet
    /// </summary>
    public void TempFire()
    {
        // check which condiment is enabled
        if (ketchupEquipped == true)
        {
            GameObject ketchup = Instantiate<GameObject>(projectiltePrefab);
            ketchup.transform.position = transform.position;
            Rigidbody2D ketchupRigidbody = ketchup.GetComponent<Rigidbody2D>();
            ketchupRigidbody.velocity = Vector2.up * projectileSpeed;
        }
        else if (mustardEquipped == true) 
        {
            GameObject mustard = Instantiate<GameObject>(projectilePrefab2);
            mustard.transform.position = transform.position;
            Rigidbody2D mustardRigidbody = mustard.GetComponent<Rigidbody2D>();
            mustardRigidbody.velocity = Vector2.up * projectileSpeed;
        }
    }
    /// <summary>
    /// Handle Collision with Enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collision");
            // Destroy Player and Enemy
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
