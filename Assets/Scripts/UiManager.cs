using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    #region Singelton 
    private static UiManager _singelton;
    public static UiManager Singelton{
        get => _singelton;
        private set{
            if(_singelton == null){
                _singelton = value ;
            }else if(_singelton != value){
                Debug.Log($"{value.name} destroyed for UiManager singelton !!");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singelton = this;
    }

    #endregion Singelton

    [SerializeField]
    GameObject startCover , selectframe ,winerIcon , loseWindow , winWindow;
    [SerializeField]
    Transform playerShinePos , aiShinePos;

    [SerializeField]
    Image playerHandUi , aiHandUi ;

    public Sprite[] handsUi ;
    public int  winScore {get ; private set ;}

    [SerializeField]
    TextMeshProUGUI timerUi , playerScore , aiScore ;
    [SerializeField]
    Text winScoreUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(winerIcon.activeInHierarchy){
            winerIcon.transform.Rotate(winerIcon.transform.forward * 0.05f);
        }
    }
    public void NextRound(){

        playerHandUi.sprite = UiManager.Singelton.handsUi[0];
        aiHandUi.sprite = UiManager.Singelton.handsUi[0];

        winerIcon.SetActive(false);
    }
    public void ShowHands(HandGesture _playerHand , HandGesture _aiHand){
        playerHandUi.sprite = UiManager.Singelton.handsUi[(int)_playerHand];
        aiHandUi.sprite = UiManager.Singelton.handsUi[(int)_aiHand];
    }
    public void SetHandFramePos(Vector3 _pos){
        selectframe.transform.position = _pos ;
    }
    public void ShowTimerValue(float _value){
        timerUi.text = ((int)_value).ToString();
    }
    public void RoundResult(int _stat , int _playerPoint , int _aiPoint){
        SoundManager.Singelton.PlayEndRoundSound();
        if(_stat == 0){
            winerIcon.transform.position = aiShinePos.position;
            winerIcon.SetActive(true);
        }else if(_stat == 1){
            winerIcon.transform.position = playerShinePos.position;
            winerIcon.SetActive(true);
        }
        
        playerScore.text = _playerPoint.ToString();
        aiScore.text = _aiPoint.ToString();
        if(GameManager.Singelton.playerScore == winScore)
            PlayerWin();
        if(GameManager.Singelton.aiScore == winScore)
            AiWin();
    }

    // Player Or Ai Win
    void AiWin(){
        GameManager.Singelton.gameEnd = true ;
        Debug.Log($"Ai won !!");
        loseWindow.SetActive(true);
        SoundManager.Singelton.PlayPlayerLoseSound();
    }
    void PlayerWin(){
        GameManager.Singelton.gameEnd = true ;
        Debug.Log($"Player won !!");
        winWindow.SetActive(true);
        SoundManager.Singelton.PlayPlayerWinSound();
    }

    // Start & Exit & Rest Buttons
    public void StartBTN(){

        if(!string.IsNullOrEmpty(winScoreUi.text)){
            winScore = int.Parse(winScoreUi.text);
        }else{
            winScore = 3 ;
        }

        GameManager.Singelton.StartGame();

        startCover.SetActive(false);
    }

    public void ExitBTN(){

        Application.Quit();
    }

    public void RestBTN(){

        SceneManager.LoadScene(0);
    }
}
