using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro ;

public class GameManager : MonoBehaviour
{
    
    #region Singelton 
    private static GameManager _singelton;
    public static GameManager Singelton{
        get => _singelton;
        private set{
            if(_singelton == null){
                _singelton = value ;
            }else if(_singelton != value){
                Debug.Log($"{value.name} destroyed for GameManager singelton !!");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singelton = this;
    }

    #endregion Singelton


    [SerializeField]
    HandBTN[] handBTNs ;

    [SerializeField]
    Animator playerAnium , aiAnim ;

    public int playerScore {get ; private set ;} 
    public int  aiScore {get ; private set ;}
    float timer ;

    delegate void VoidDelegate() ;
    VoidDelegate OnEndTimer ;
    bool tickTack ;
    public bool  gameEnd = false ;
    public HandGesture playerHand = HandGesture.Rock , aiHand = HandGesture.Rock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        TimerTick();
    }
    
    // Calls value of OnEndTimer after passing 1.0 
    void TimerTick(){
        UiManager.Singelton.ShowTimerValue(timer);

        if(!gameEnd){
            if(timer > 0 && tickTack){
                timer -= Time.deltaTime ;
            }
            if(timer < 1f && tickTack){
            
                OnEndTimer?.Invoke();
            }
        }
        
    }
    // Set hand gesture for player (Use Buttons)
    public void SetPlayerHand(HandBTN _value){

        //Debug.Log($"{_value.name} has triggerd .");

        playerHand = _value.gesture ;

        UiManager.Singelton.SetHandFramePos(_value.transform.position);
    }

    // Sets Timer and value of OnEndTimer
    void SetRoundTimer(){
        timer = 5f;
        tickTack = true ;
        OnEndTimer = RoundTimerEnd ;
    }
    void SetRoundSwitchTimer(){
        timer = 6f;
        tickTack = true ;
        OnEndTimer = NextRound ;
    }

    // Prepare Game For Next Round 
    void NextRound(){
        tickTack = false ;

        aiHand = HandGesture.Rock;
        playerHand = HandGesture.Rock;

        SetPlayerHand(handBTNs[0]);

        UiManager.Singelton.NextRound();

        SetRoundTimer();

        playerAnium.Play("HandMove");
        aiAnim.Play("HandMove");
    }

    // Set Ai Hand Gesture And Call UI 
    void RoundTimerEnd(){
        tickTack = false ;
        aiHand = (HandGesture)Random.Range(0,3);
        ShowHands();
        EndRound();
    }
    
    // Call UI 
    void ShowHands(){
        UiManager.Singelton.ShowHands(playerHand,aiHand);

        playerAnium.SetTrigger("Show");
        aiAnim.SetTrigger("Show");
    }

    // Main patr , Finds who won this round 
    void EndRound(){
        int stat = 2;
        switch (playerHand)
        {
            case HandGesture.Rock :
                if(aiHand == HandGesture.Scissors){
                    stat = 1;
                    playerScore++;
                }else if(aiHand == HandGesture.Paper){
                    stat = 0; 
                    aiScore++; 
                }
                break;
            case HandGesture.Paper :
                if(aiHand == HandGesture.Rock){
                    stat = 1;
                    playerScore++;
                }else if(aiHand == HandGesture.Scissors){
                    stat = 0;
                    aiScore++;   
                }
                break;
            case HandGesture.Scissors :
                if(aiHand == HandGesture.Paper){
                    stat = 1;
                    playerScore++;
                }else if(aiHand == HandGesture.Rock){
                    stat = 0;
                    aiScore++;   
                }
                break;
            default:
                break;
        }

        
        //Debug.Log($"Round stat is {stat} : Player>>{playerHand.ToString()} , Ai>>{aiHand.ToString()}");
        
        UiManager.Singelton.RoundResult(stat , playerScore , aiScore);
        SetRoundSwitchTimer();
    }

    // Prepers First Round 
    void StartRound(){

        playerAnium.Play("HandMove");
        aiAnim.Play("HandMove");

        SetRoundTimer();
        SetPlayerHand(handBTNs[0]);

    }

    // First Round Settings 
    public void StartGame(){
        StartRound();
        playerScore = 0 ;
        aiScore = 0 ;
        
    }
    
}
