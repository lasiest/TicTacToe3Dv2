using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

[RequireComponent(typeof(PhotonView))]
public class SpawnPlayer : MonoBehaviourPun
{
    public GameObject player1;
    public GameObject player2;
    public GameObject StartButton;
    public TMP_Text Infotext;
    // public GameObject player1InfoText;
    // public GameObject player2InfoText;

    private void Awake() {
        player1.SetActive(false);
        player2.SetActive(false);
        // player1InfoText.SetActive(false);
        // player2InfoText.SetActive(false);     
        StartButton.SetActive(false);
        RPC_SetActivePlayer();

    }

    [PunRPC]
    private void SetActivePlayers() {
        if(!player1.activeInHierarchy){
            player1.SetActive(true);
            Debug.Log("set active player 1");
            Infotext.text = "Waiting for other player";
            // player1InfoText.SetActive(true);
        }else if(player1.activeInHierarchy && !player2.activeInHierarchy){
            player2.SetActive(true);
            Debug.Log("set active player 2");
            Infotext.text = "Waiting for host";
            // player1InfoText.SetActive(false);
            // player2InfoText.SetActive(true);
        }
    }

    private void RPC_SetActivePlayer(){
        photonView.RPC(nameof(SetActivePlayers), RpcTarget.AllBuffered);
        Debug.Log("RPC_SetActivePlayer terpanggil");
    }

    // [PunRPC]
    // private void SetActiveStartButton(){
    //     if(player1.activeInHierarchy && player2.activeInHierarchy){
    //         StartButton.SetActive(true);
    //     }
    // }

    private void RPC_SetActiveStartButton(){
        photonView.RPC(nameof(SetActivePlayers), RpcTarget.Others);
    }

    private void Update() {
        if(player1.activeInHierarchy && player2.activeInHierarchy && photonView.IsMine){
            Infotext.text = "Start when you ready!";
            StartButton.SetActive(true);
        }
    }
}
