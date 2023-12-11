using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Animation fields
    public Animator animator;
    public bool isMoving = false;
    float movementAnim;
    float shootingAnimK;
    float shootingAnimM;
    bool isFiringK;
    bool isFiringM;

    // Player movement setup
    public Rigidbody2D rb2d;
    public float playerSpeed = 8f;
    Vector2 movement;

    // Ammunition fields
    public bool outOfAmmo = false;
    public int mustardAmmo = 25;

    // Shooting fields
    public GameObject projectiltePrefab;
    public GameObject projectilePrefab2;
    public float projectileSpeed = 15f;
    public bool ketchupEquipped = true;
    public bool mustardEquipped = false;

    // hud support
    HUD hud = new HUD();

    // Start is called before the first frame update
    void Start()
    {
        // Save reference to HUD Script
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        // Set player to idle
        animator = gameObject.GetComponent<Animator>();
        //animator.SetFloat("MovementSpeed", 0f);
        //isMoving = false
        //
        // get the animator's movement and shooting floats
        movementAnim = animator.GetFloat("MovementSpeed");
        shootingAnimK = animator.GetFloat("KetchupSpeed");
        shootingAnimM = animator.GetFloat("MustardSpeed");
        isFiringK = animator.GetBool("IsFiringKetchup");
        isFiringM = animator.GetBool("IsFiringMustard");
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

            if (ketchupEquipped == true)
            {
                animator.SetBool("IsFiringKetchup", true);
                // check if walk animation is playing
                if (movementAnim == 1f)
                {
                    // stop walking animation 
                    animator.SetFloat("MovementSpeed", 0f);
                }
            }
            
            if (mustardEquipped == true)
            {
                animator.SetBool("IsFiringMustard", true);
                // check if walk animation is playing
                if (movementAnim == 1f)
                {
                    // stop walking animation 
                    animator.SetFloat("MovementSpeed", 0f);
                }
            }
            
        }
        //check when player releases space
        else if (Input.GetKeyUp(KeyCode.Space) && ketchupEquipped == true)
        {
            // set animation back to idle
            animator.SetFloat("KetchupSpeed", 0f);
            animator.SetBool("IsFiringKetchup", false);
           
        }
        else if (Input.GetKeyUp(KeyCode.Space) && mustardEquipped == true)
        {
            // set animation back to idle
            animator.SetFloat("MustardSpeed", 0f);
            animator.SetBool("IsFiringMustard", false);
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
            ketchupEquipped = false;
        }
        // check for animation
        if (movement.x > 0f && isFiringK == false || movement.x < 0f && isFiringK == false)
        {
            // Update animation
            animator.SetFloat("MovementSpeed", 1f);
        }
        else if (movement.y > 0f && isFiringK == false || movement.y < 0f && isFiringK == false)
        {
            // Update animation
            animator.SetFloat("MovementSpeed", 1f);
        }
        else
        {
            // keep player animation in idle
            animator.SetFloat("MovementSpeed", 0f);
        }

        if (movement.x > 0f && isFiringM == false || movement.x < 0f && isFiringM == false)
        {
            // Update animation
            animator.SetFloat("MovementSpeed", 1f);
        }
        else if (movement.y > 0f && isFiringM == false || movement.y < 0f && isFiringM == false)
        {
            // Update animation
            animator.SetFloat("MovementSpeed", 1f);
        }
        else
        {
            // keep player animation in idle
            animator.SetFloat("MovementSpeed", 0f);
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
            // Update player animation
            animator.SetFloat("KetchupSpeed", 1f);

        }
        else if (mustardEquipped == true && outOfAmmo == false) 
        {
            GameObject mustard = Instantiate<GameObject>(projectilePrefab2);
            mustard.transform.position = transform.position;
            Rigidbody2D mustardRigidbody = mustard.GetComponent<Rigidbody2D>();
            mustardRigidbody.velocity = Vector2.up * projectileSpeed;
            // Update player animation
            animator.SetFloat("MustardSpeed", 1f);
            mustardAmmo -= 1;
            // update text
            hud.RemoveMustardAmmo();
            
        }
        // check if mustard is out of ammo
        if (mustardAmmo <= 0)
        {
            outOfAmmo = true;
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
            // Spawn the lose screen
            hud.SpawnLoseMenu();
        }
        // if player collides with ammo
        if (collision.gameObject.CompareTag("Ammo"))
        {
           
            Destroy(collision.gameObject);

            // refill ammo
            mustardAmmo += 5;
            outOfAmmo = false;

            if (mustardAmmo >= 25)
            {
                mustardAmmo = 25;
            }
            // update UI
            hud.AddMustardAmmo();
        }
    }
    /// <summary>
    /// Decreases player's mustard ammo by 5
    /// </summary>
    public void ReduceMustardAmmo()
    {
        mustardAmmo -= 5;
        // check if the ammo count is at or below 0
        if (mustardAmmo <= 0)
        {
            // keep it 0
            mustardAmmo = 0;
            outOfAmmo = true;
        }
    }
}
