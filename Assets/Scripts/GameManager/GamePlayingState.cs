using Unity.VisualScripting;
using UnityEngine;

public class GamePlayingState : GameBaseState
{


    public override void EnterState()
    {
InitializeLevel();
    }

    public override void UpdateState()
    {
        GameManager.Instance.PlayerMovement();
        GameManager.Instance.ZombieMovement();
        GameManager.Instance.BattleTimePlay();
        GameManager.Instance.spawnManager.UpdateSpawn();
        if(IsPassLevel()){
            GameManager.Instance.currentLevel++;
           InitializeLevel();
            
        }
        if(GameManager.Instance.battleTimeRemainning<=0){
            GameManager.Instance.UpdateGameState(GameState.GameOver);
        }

    }
    public bool IsPassLevel(){
        if(GameManager.Instance.remainingzombies<=0){
            return true;
        }
        return false;
    }
    public void InitializeLevel(){
           GameManager.Instance.battleTimeRemainning=GameManager.Instance.levelConfig.timeEachLevel;
            GameManager.Instance.remainingzombies= GameManager.Instance.levelConfig.totalZombiesNumber;
    }
   



}