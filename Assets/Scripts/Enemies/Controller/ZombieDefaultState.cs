


using UnityEngine;

public class ZombieDefaultState : ZombieBaseState
{
    public override void EnterState(ZombieManager zombieManager)
    {
      zombieManager.Initialize();
    }

    public override void UpdateState(ZombieManager zombieManager)
    {
        zombieManager.SetSpeed();
        zombieManager.ChasePlayer(PlayerManager.instance.transform);
        
         if (zombieManager.CanReset())
        {
            zombieManager.Reset();
        }
        if (zombieManager.CanAttack())
        {
            zombieManager.Attack();
        }
       
    }
}
