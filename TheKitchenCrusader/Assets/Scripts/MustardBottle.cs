using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class MustardBottle : MonoBehaviour
{
    public eWeaponType type; // The type of power up
    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    public float birthTime; // The time.time this was instantiated
    public float lifeTime = 10; // PowerUp will exist for # seconds
    public float fadeTime = 4;  // Then it fades over # seconds
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
       // Don;t rotate object
       transform.rotation = Quaternion.identity;
        birthTime = Time.time;
    }
    // Start is called before the first frame update
    void Start()
    {
        // get access to sprite renderer
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      // fade power up over time
      float u = (Time.time - (birthTime - lifeTime)) / fadeTime;
        // if u >= 1, destroy this power up
        if (u >= 1)
        {
            Destroy(this.gameObject);
        }
        // if u > 0 decrese the opacity of this game object
        if (u > 0)
        {
            Color c = spriteRenderer.color; 
            // Fade the game object
            c.a = 1f - u;
            spriteRenderer.color = c;
        }
        if (!bndCheck.isOnScreen)
        {
             // If the PowerUp has drifted entirely off screen, destroy it
             Destroy(gameObject);
        }
    }

    // called by player when colliding with ammo
    public void AbsorbedBy (GameObject target)
    {
        Destroy(this.gameObject);
    }
}
