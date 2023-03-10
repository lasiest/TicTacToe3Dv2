using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PlayerInformationScript : MonoBehaviour, IPunInstantiateMagicCallback
{
    Renderer rend;
    public PhotonVoiceView photonVoiceView;
    public GameObject indicatorSpeaker;

    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiateData = info.photonView.InstantiationData;
        int id = (int)instantiateData[0];

        Player_ScriptableObject player_ScriptableObject = Array.Find(CharacterInfo.Instance.arrayOfCharacterSelection, x => x.id  == id);
        rend = GetComponent<MeshRenderer>();
        rend.enabled = true;
        rend.sharedMaterial = player_ScriptableObject.color;
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
