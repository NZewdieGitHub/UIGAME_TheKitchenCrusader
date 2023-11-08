using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Enemy Functionality
/// </summary>
[RequireComponent(typeof(BoundsCheck))]
public class Enemy : MonoBehaviour
{
    // enemy setup
    [Header("Inscribed")]
    public float speed = 10;
    public float health = 10;

    //sprite color setup
    SpriteRenderer spriteRenderer;

    // Enemy Clamp Setup
    private BoundsCheck bndCheck;

    // Vector 3 property
    public Vector3 pos
    {
        get { return this.transform.position; }
        set { this.transform.position = value; }
    }

    private void Awake()
    {
        bndCheck= GetComponent<BoundsCheck>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // get access to the sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        // check if enemy is bleow the bottom of the screen
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            // Game Object is below the screen, so destroy it
            Destroy(gameObject);
        }
       
    }
    /// <summary>
    /// Moves the enemy down
    /// </summary>
    public virtual void Move()
    { 
        // Gets current position
        Vector3 tempPos = pos;
        // Moves it downward
        tempPos.y -= speed * Time.deltaTime;
        // sets the position
        pos = tempPos;
    }
    /// <summary>
    /// Colliding with projectiles
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for ketchup
        if (collision.gameObject.CompareTag("Ketchup"))
        {
            // Decrease health
            health -= 1;
            // Destroy ketchup shot
            Destroy(collision.gameObject);
            
            // check if health is below 
            if (health <= 0)
            {
                // Destroy enemy
                Destroy(gameObject);

            }
            // reduce polarity
            Color color = spriteRenderer.color;
            color.a -= 0.1f;
            spriteRenderer.color = color;
        }
        // else if it's mustard
        else if (collision.gameObject.CompareTag("Mustard"))
        {
            // Destroy Mustard Shot
            Destroy(collision.gameObject);

            // Slow enemy down
            speed -= 0.5f;
            // If speed is at its limit
            if (speed <= 0)
            {
                // keep enemy speed at 0
                speed = 0;

            }
        }

    }
}
