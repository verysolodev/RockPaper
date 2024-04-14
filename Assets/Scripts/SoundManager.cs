using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singelton 
    private static SoundManager _singelton;
    public static SoundManager Singelton{
        get => _singelton;
        private set{
            if(_singelton == null){
                _singelton = value ;
            }else if(_singelton != value){
                Debug.Log($"{value.name} destroyed for SoundManager singelton !!");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singelton = this;
    }

    #endregion Singelton

    [SerializeField]
    AudioSource source ;
    [SerializeField]
    AudioClip endRound , playerWin , playerLose ;

    public void PlayPlayerWinSound(){
        source.Stop();
        source.clip = playerWin ;
        source.Play();
    }

    public void PlayPlayerLoseSound(){
        source.Stop();
        source.clip = playerLose ;
        source.Play();
    }

    public void PlayEndRoundSound(){
        source.Stop();
        source.clip = endRound ;
        source.Play();
    }
    

}
