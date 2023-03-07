using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using TMPro;

public class muteUnmuteScript : MonoBehaviour
{

    public Recorder recorder;
    public TMP_Text buttonText; 

    // Update is called once per frame
    private void Awake() {
        buttonText.text = "Click to mute";
    }
    public void Mute_Unmute(){
        if(recorder.TransmitEnabled){
            buttonText.text = "Click to unmute";
            recorder.TransmitEnabled = false;
        }else{
            buttonText.text = "Click to mute";
            recorder.TransmitEnabled = true;
        }
    }
}
