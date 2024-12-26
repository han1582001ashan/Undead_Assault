using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
public int cardID;
public string cardName;
public string cardDescription;
public int cost;
    
}
public enum CardType{
    Heal,
    Damage,
    Ammo,
    ClipSize,
    Speed,
}