using UnityEngine;

public class GameStartState : GameBaseState
{
    

    public override void EnterState()
    {

      GameManager.Instance.Initialize();
      GameManager.Instance.DeleteAllZomBie();
      GameManager.Instance.InitializePlayer();

    }

    public override void UpdateState()
    {
      GameManager.Instance.UpdateGameState(GameState.GamePlaying);
      
    }
}
