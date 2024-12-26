using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;

    float timer;
    public Rifle rifle;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToDestroy)
        {
            DeleteBullet();
        }
    }
    void DeleteBullet()
    {
        Destroy(this.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<ZombieManager>())
        {
            ZombieManager zombie = collision.gameObject.GetComponentInParent<ZombieManager>();
            GameManager.Instance.SendDamageToZombie(zombie, rifle.damage);
        }
        DeleteBullet();

    }

}
