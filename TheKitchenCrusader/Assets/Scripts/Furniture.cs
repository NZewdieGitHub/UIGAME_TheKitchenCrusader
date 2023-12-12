using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Furniture Objects
/// </summary>
public class Furniture : MonoBehaviour
{
    // Health fields
    public float health = 3f;
    HUD hud = new HUD();

    // position fields
    int ranPosX = Random.Range(5, 8);
    int ranPosY = Random.Range(2, 3);
    // Start is called before the first frame update
    void Start()
    {
        // Save reference to HUD Script
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        // Give table a random position
        Vector2 pos = transform.position;
        pos = new Vector2(ranPosX, ranPosY);
        Instantiate(gameObject,pos,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Check for enemy Collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy Enemy
            Destroy(collision.gameObject);
            // Reduce Furniture Health
            health -= 1f;

            // If health is past its limit
            if (health <= 0)
            {
                // Destroy Furniture gameobject
                Destroy(gameObject);
                // Update UI
                hud.RemoveFurniture();
                
            }
        }
    }
}
