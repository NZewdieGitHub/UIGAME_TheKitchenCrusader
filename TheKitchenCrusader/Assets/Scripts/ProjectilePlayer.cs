using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Holds projectile functionality
/// </summary>
[RequireComponent(typeof(BoundsCheck))]
public class ProjectilePlayer : MonoBehaviour
{
    private BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if projectile is offscreen
        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {          
            Destroy(gameObject);
        }
    }
}
