using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChange;
    public GameBaseState currentState;
    public GameStartState startState = new GameStartState();
    public MainMenuState mainMenuState = new MainMenuState();
    public GamePlayingState gamePlayingState=new GamePlayingState();
    public GamePauseState gamePauseState=new GamePauseState();

public GameOverState gameOverState=new GameOverState();

    public Transform playerStartTransform;

    // LoadConfig
    public PlayerConfig playerConfig;
    public ZombieConfig[] zombieConfigList;
    public Dictionary<ZombieType, ZombieConfig> zombieConfigDictionnary; 
    public LevelConfig levelConfig;

    public List<GameObject> zombiesOnMap = new List<GameObject>();
    public int currentLevel;
    public int maxZombieOnMap;
    public int maxZombieInLevel;
    public int remainingzombies;
    public bool isPause;
    public bool timeIsRunning=true;
    public float battleTimeRemainning;
    public float WaitTimeRemaining;
    public bool isBattleTime;
     public SpawnManager spawnManager;

     public int coin;

    void Awake()
    {
        Instance = this;
        LoadZombieConfig();
        maxZombieOnMap= levelConfig.maxZombiesOnMapNumber;
        maxZombieInLevel= levelConfig.totalZombiesNumber;  
        Initialize();

        
    }
    public void Initialize(){
        coin=0;
        currentLevel=1;
        remainingzombies=maxZombieInLevel;

    }

    void Start()
    {
        UpdateGameState(GameState.MainMenu);
    }
    void LoadZombieConfig(){
        zombieConfigDictionnary= new Dictionary<ZombieType, ZombieConfig>();
        for(int i=0;i<zombieConfigList.Length ;i++){
            zombieConfigDictionnary.Add(zombieConfigList[i].zombieType,zombieConfigList[i]);
        }
    }
    void Update()
    {
        currentState.UpdateState();
       
    }
    public void UpdateGameState(GameState state)
    {
        State = state;
        switch (state)
        {
            case GameState.MainMenu:
              SwitchState(mainMenuState);
                break;
            case GameState.GameStart:
            SwitchState(startState);
                break;
                case GameState.GamePause:
            SwitchState(gamePauseState);
                break;
            case GameState.GamePlaying:
            SwitchState(gamePlayingState);
                break;
            case GameState.GameOver:
            SwitchState(gameOverState);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
        OnGameStateChange?.Invoke(state);
    }
    public void InitializePlayer(){
        PlayerManager.instance.transform.position=playerStartTransform.position;
        PlayerManager.instance.Initialize();
         
    }
    
    public void BattleTimePlay(){
        
          if(battleTimeRemainning>=0){
             battleTimeRemainning -= Time.deltaTime;
        }
    }
    public void WaitTimePlay(){
        if(WaitTimeRemaining>=0){
            WaitTimeRemaining-=Time.deltaTime;
        }
    }
    public void ZombieMovement(){

            foreach(GameObject zombie in zombiesOnMap.ToList()){
            ZombieManager zombieManager= zombie.GetComponent<ZombieManager>();
            zombieManager.ZombieUpdate();
        
        }
    }
    public void DeleteAllZomBie(){

            foreach(GameObject zombie in zombiesOnMap.ToList()){
                zombiesOnMap.Remove(zombie);
            Destroy(zombie);
        
        }
       
    }
    public void PlayerMovement(){
        PlayerManager.instance.PlayerUpdate();
    }
    public void SendDamageToZombie(ZombieManager zombieManager, int damage)
    {
        zombieManager.TakeDamage(damage);
    }
    public void SendDamageToPlayer(PlayerManager playerManager, int damage)
    {
        playerManager.TakeDamage(damage);
    }
    public void SwitchState(GameBaseState state)
    {
        currentState = state;
        currentState.EnterState();
    }
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }
}
public enum GameState
{
    MainMenu,
    GameStart,
    GamePlaying,
    GamePause,
    GameOver
}
