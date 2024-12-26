using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

public enum ZombieType
{
    Roaming,
    Speed,
    Tanker
}
[CreateAssetMenu(fileName ="ZombieConfig", menuName ="ZombieConfig/New Zombie")]
public class ZombieConfig : ScriptableObject
{

   
    [SerializeField]
    public string zombieName;
    [SerializeField]
    public ZombieType zombieType;

    [SerializeField]
    public int HP;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public int atkDamage;
    [SerializeField]
    public float atkSpeed;
    [SerializeField]
    public float atkRange=0.7f;
  
    [SerializeField]
    public int coin;
  
}


