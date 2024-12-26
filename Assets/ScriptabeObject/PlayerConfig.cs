using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerConfig", menuName ="Scriptable/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    public static PlayerConfig instance;
    public int health;
    public float moveSpeed;
    public float jumpHeight;
    public float rotationSpeed = 10f;
    public int ammo;
    void Awake()
    {
        instance = this;
    }
}
