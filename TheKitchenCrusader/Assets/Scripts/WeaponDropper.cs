using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWeaponType
{
    none, // no weapon
    mustard // slows enemies down
}
[System.Serializable]                                                         // a
 public class WeaponDefinition
{                                               // b
     public eWeaponType type = eWeaponType.none;
     [Tooltip("Letter to show on the PowerUp Cube")]                           // c
     public string letter;
     [Tooltip("Color of Mustard Powerup")]
     public Color powerUpColor = Color.white;                           // d
 }
public class WeaponDropper : MonoBehaviour
{
   
}
