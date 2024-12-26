using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType{
    Gun,
    Melee

}
public class WeaponConfig : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;
    public int damage;
    public float atkSpeed;
}
