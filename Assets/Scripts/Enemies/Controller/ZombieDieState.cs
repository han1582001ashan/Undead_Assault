

using UnityEngine;

public class ZombieDieState : ZombieBaseState
{
    private float deadTime=3f;
    public override void EnterState(ZombieManager zombieManager)
    {
       zombieManager.animator.SetBool("Die", true);
       zombieManager.isDie=true;
       zombieManager.agent.isStopped=true;
    }

    public override void UpdateState(ZombieManager zombieManager)
    {
     
        
    }
}