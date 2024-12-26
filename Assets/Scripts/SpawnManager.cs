using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] zombiesList;
    [SerializeField]
    private float spawnRateTimer;
    public void UpdateSpawn()
    {
        if(CanSpawn()){
            SpawnZombie();
        }
    }
    public void Initialize(){
        spawnRateTimer=GameManager.Instance.levelConfig.spawnSpeed;
        
    }
   
    bool CanSpawn()
    {
        spawnRateTimer += Time.deltaTime;
        if (spawnRateTimer < GameManager.Instance.levelConfig.spawnSpeed)
        {
            return false;
        }
        if (GameManager.Instance.remainingzombies <= 0)
        {
            return false;
        }
        if (GameManager.Instance.zombiesOnMap.Count >= GameManager.Instance.levelConfig.maxZombiesOnMapNumber)
        {
            return false;
        }
        
        if (GameManager.Instance.remainingzombies > 0)
        {
            return true;
        }
        return false;
    }
    void SpawnZombie()
    {
        spawnRateTimer = 0;
        System.Random r = new System.Random();
        int spawnPoint = r.Next(0, spawnPoints.Length - 1);
        int enemyType = r.Next(0, zombiesList.Length - 1);
        GameObject newZombie = Instantiate(zombiesList[enemyType], spawnPoints[spawnPoint].position, Quaternion.identity);
       
        GameManager.Instance.zombiesOnMap.Add(newZombie);
        GameManager.Instance.remainingzombies--;
    }
}
