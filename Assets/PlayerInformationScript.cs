using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInformationScript : MonoBehaviour
{
    public Player_ScriptableObject player_ScriptableObject;
    public Material color;
    public PhotonVoiceView photonVoiceView;
    public GameObject indicatorSpeaker;
    private void Awake() {
        color = player_ScriptableObject.color;
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
