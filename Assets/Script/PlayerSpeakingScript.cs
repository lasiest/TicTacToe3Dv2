using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpeakingScript : MonoBehaviourPun
{   
    public PhotonVoiceView photonVoiceView;
    public GameObject indicatorSpeaker;
    private void Awake() {
        indicatorSpeaker.SetActive(false);
    }

    void Update()
    {
        if(photonVoiceView.IsSpeaking){
            indicatorSpeaker.SetActive(true);
        }else if(!photonVoiceView.IsSpeaking){
            indicatorSpeaker.SetActive(false);
        }            
    }
}
