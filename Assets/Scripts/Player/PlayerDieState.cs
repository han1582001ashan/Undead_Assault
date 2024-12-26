

public class PlayerDieState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
           playerManager.animator.SetBool("Death", true);
    
      
    }

    public override void UpdateState(PlayerManager playerManager)
    {
         if(playerManager.isDie==false){
        ExitState(playerManager);
      }
    }
    public void ExitState(PlayerManager playerManager){
      playerManager.animator.SetBool("Death", false);
playerManager.SwitchState(playerManager.defaultState);
    }
}
