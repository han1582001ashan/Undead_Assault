using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieClaw : MonoBehaviour
{
    [SerializeField] int damage;
    ZombieManager zombieManager;


    void Start()
    {
        zombieManager = GetComponentInParent<ZombieManager>();

    }

    // Update is called once per frame
    void Update()
    {


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && zombieManager.isAttack == true)
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
            GameManager.Instance.SendDamageToPlayer(playerManager, damage);
           
        }
    }

}
