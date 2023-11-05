using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used for clamping the player and destroying projectiles offscreen
/// </summary>
public class BoundsCheck : MonoBehaviour
{
    // Enumuration to determine screen bounds
    [System.Flags]
    public enum eScreenLocs
    {                                        // a
        onScreen = 0,  // 0000 in binary (zero)
        offRight = 1,  // 0001 in binary
        offLeft = 2,  // 0010 in binary
        offUp = 4,  // 0100 in binary
        offDown = 8   // 1000 in binary
    }


    // fields for camera width and height
    [Header("Dynamic")]
    public float camWidth;
    public float camHeight;

    // position sides of the camera
    public enum eType { center, inset, outset };

    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen = true;

    // bool to check if object is on screen
    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    //public bool isOnScreen = true;

    /// <summary>
    /// Play on awake
    /// </summary>
    private void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
    /// <summary>
    /// Happens every frame AFTER Update method
    /// </summary>
    private void LateUpdate()
    {
        // find the checkRadius that will enable center
        float checkRadius = 0;
        if (boundsType == eType.inset)
        {
            checkRadius = -radius;
        }
        if (boundsType == eType.outset)
        {
            checkRadius = radius;
        }
        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;
        // isOnScreen = false;
        // Restrict the x position to CamWidth
        // right of x axis
        if(pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            //isOnScreen = false;
            screenLocs |= eScreenLocs.offRight;
        }
        // left of x axis
        if (pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            screenLocs |= eScreenLocs.offLeft;
            //isOnScreen = false;
        }
        // top of y axis
        if (pos.y > camHeight + checkRadius) 
        {
            pos.y = camHeight + checkRadius;
            screenLocs |= eScreenLocs.offUp;
            //isOnScreen = false;
        }
        // bottom of y axis
        if (pos.y < -camHeight - checkRadius) 
        {
            pos.y = -camHeight - checkRadius;
            screenLocs |= eScreenLocs.offDown;
            //isOnScreen = false;
        }
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            //isOnScreen = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Checks if player is on screen
    /// </summary>
    /// <returns></returns>
    public bool isOnScreen
    {
        get { return screenLocs == eScreenLocs.onScreen; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="checkLoc"></param>
    /// <returns></returns>
    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;          // a
        return ((screenLocs & checkLoc) == checkLoc);                     // b
    }
}
