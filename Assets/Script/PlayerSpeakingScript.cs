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
    // public GameObject indicatorRecording;
    private void Awake() {
        indicatorSpeaker.SetActive(false);
        // indicatorRecording.SetActive(false);
    }

    void Update()
    {
        // if(photonVoiceView.IsRecording){
        //     indicatorRecording.SetActive(true);
        // }else if(!photonVoiceView.IsRecording){
        //     indicatorRecording.SetActive(false);
        // }

        if(photonVoiceView.IsSpeaking){
            indicatorSpeaker.SetActive(true);
        }else if(!photonVoiceView.IsSpeaking){
            indicatorSpeaker.SetActive(false);
        }            
    }

    // public void OnPlayerLeftRoom(Player otherPlayer){
    //     Debug.Log("Destroy this game object");
    //     PhotonNetwork.Destroy(gameObject);
    // }
}
