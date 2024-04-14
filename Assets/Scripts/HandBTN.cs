using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HandGesture : int{
    Rock = 0 ,
    Scissors,
    Paper,

}
public class HandBTN : MonoBehaviour
{
    public HandGesture gesture = HandGesture.Rock ;

    // On Button Trigger
    public void BTN(){
        GameManager.Singelton.SetPlayerHand(this);
    }
}
