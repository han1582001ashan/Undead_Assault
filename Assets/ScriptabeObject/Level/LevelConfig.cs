using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelConfig", menuName ="LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [Header("Level Config")]
    public int totalZombiesNumber;
    public int maxZombiesOnMapNumber;
    public int spawnSpeed;
    public float timeEachLevel;
    public float waitTime;
    
    [Header("difficulty increases each level")]
    public float increaseZombieValueEachLevel;
    public float increaseMapValueEachLevel;
}
