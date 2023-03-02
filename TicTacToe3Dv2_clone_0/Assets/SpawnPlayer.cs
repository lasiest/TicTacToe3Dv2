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
    public GameObject _lingkaran;
    public GameObject _kotak;
    public GameObject board;
    public TMP_Text Infotext;
    public bool gameStarted;
    public bool gameEnded;
    public int turn;
    public int[] values;

    private void Awake() {
        player1.SetActive(false);
        player2.SetActive(false);
        board.SetActive(false);     
        StartButton.SetActive(false);
        RPC_SetActivePlayer();
        turn = 0;
        gameStarted = false;
        gameEnded = false;

    }

    [PunRPC]
    private void SetActivePlayers() {
        if(!player1.activeInHierarchy){
            player1.SetActive(true);
            Debug.Log("set active player 1");
            Infotext.text = "Waiting for other player";
        }else if(player1.activeInHierarchy && !player2.activeInHierarchy){
            player2.SetActive(true);
            Debug.Log("set active player 2");
            Infotext.text = "Waiting for host";
        }
    }

    private void RPC_SetActivePlayer(){
        photonView.RPC(nameof(SetActivePlayers), RpcTarget.AllBuffered);
        Debug.Log("RPC_SetActivePlayer terpanggil");
    }

    private void RPC_SetActiveStartButton(){
        photonView.RPC(nameof(SetActivePlayers), RpcTarget.Others);
    }

    private void Update() {
        if(player1.activeInHierarchy && player2.activeInHierarchy && photonView.IsMine && !gameStarted){
            Infotext.text = "Start when you ready!";
            StartButton.SetActive(true);
        }
        if(gameStarted && photonView.IsMine && turn % 2 == 0){
            Infotext.text = "YourTurn";
        }else if(gameStarted && photonView.IsMine && turn % 2 != 0){
            Infotext.text = "Wait for your turn";
        }

        if(gameStarted && !photonView.IsMine && turn % 2 == 0){
            Infotext.text = "Wait for your turn";
        }else if(gameStarted && !photonView.IsMine && turn % 2 != 0){
            Infotext.text = "YourTurn";
        }
    }

    [PunRPC]
    private void StartFunction(){
        board.SetActive(true);
        StartButton.SetActive(false);
        Infotext.text = "";
        gameStarted  = true;
    }

    public void RPC_StartFunction(){
        photonView.RPC(nameof(StartFunction), RpcTarget.All);
    } 

    [PunRPC]
    public void buttonIsClick(int value){
        if(values[value] == 0){
            values[value] = (turn % 2) + 1;
            turn++;
        }           
    }

    public void RPC_buttonIsClick(int value){
        if(photonView.IsMine && turn % 2 == 0){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
        }else if(!photonView.IsMine && turn % 2 != 0){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
        }else{
            Debug.Log("Not your turn");
        }
    }
}
