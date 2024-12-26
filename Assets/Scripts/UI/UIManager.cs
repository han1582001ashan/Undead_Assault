using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    //MainMenu
    [SerializeField] private GameObject UIMainMenu, UIGamePlay, UIPauseGame, UIGameOver;
    //UIGamePlay
    public FixedJoystick MovementJoystick;
    public FixedJoystick AttackJoystick;
    public FixedTouchField fixedTouchField;
    public TMP_Text displayBattleTimeTxt;
   
    public TMP_Text zombieLeftTxt;
    public TMP_Text coinTxt;
    public Button attackButton;
    public Button reloadButton;
    public bool attackButtonPress;
    public bool reloadButtonPress;
    public UIHealthBar EnemyHealBar;
    public UIHealthBar PlayerHealBar;
    public TMP_Text ammoTxt;
    public TMP_Text achievementTxt;

    void Awake()
    {
        reloadButtonPress = false;
        GameManager.OnGameStateChange += UpdateUIOnGameStateChange;
    }
    private void UpdateUIOnGameStateChange(GameState state){
        UIMainMenu.SetActive(state== GameState.MainMenu);
        UIGamePlay.SetActive(state==GameState.GamePlaying);
        UIPauseGame.SetActive(state==GameState.GamePause);
        UIGameOver.SetActive(state==GameState.GameOver);
       
    }

    void Update()
    {
        displayBattleTimeTxt.text= DisplayTime(GameManager.Instance.battleTimeRemainning);
       
        int zombieLeft= GameManager.Instance.remainingzombies+ GameManager.Instance.zombiesOnMap.Count;
        zombieLeftTxt.text="Zombie Left: "+ zombieLeft +"/"+ GameManager.Instance.levelConfig.totalZombiesNumber;
        coinTxt.text=GameManager.Instance.coin.ToString();
        achievementTxt.text= "Your Score: "+ GameManager.Instance.coin;
        

    }
    public void StartGame(){
        GameManager.Instance.UpdateGameState(GameState.GameStart);
    }
    public void ReturnMainMenu(){
         GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
    public void PauseGame(){
     GameManager.Instance.UpdateGameState(GameState.GamePause);
    }
    public void ContinueGame(){
         GameManager.Instance.UpdateGameState(GameState.GamePlaying);
    }
    private void OnEnable()
    {
        instance = this;
    }
    private void OnDisable()
    {
        instance = null;
    }
    public void AttackButtonPress()
    {
        attackButtonPress = true;
    }
    public void AttackButtonUnPress()
    {
        attackButtonPress = false;
    }
    public void ReloadButtonPress()
    {
        reloadButtonPress = true;
    }
    public string DisplayTime(float timeToDisPlay){
        timeToDisPlay+=1;
        float minutes= Mathf.FloorToInt(timeToDisPlay/60);
        float seconds= Mathf.FloorToInt(timeToDisPlay%60);
       string TimeText= string.Format("{0:00}: {1:00}", minutes, seconds);
       return TimeText;
    }
}
