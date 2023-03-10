using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using System;

[RequireComponent(typeof(PhotonView))]
public class SpawnPlayer : MonoBehaviourPunCallbacks
{   
    public GameObject DefaultCharacter;
    public string player1name;
    public string player2name;
    public GameObject StartButton;
    public GameObject MenuButton;
    public GameObject lingkaran;
    public GameObject silang;
    public GameObject board;
    public TMP_Text Infotext;
    public TMP_Text InfoRoom;
    public TMP_Text tmp_player1;
    public TMP_Text tmp_player2;
    public bool gameStarted;
    public bool gameEnded;
    public int turn;
    public int[] values;
    public int[] flag;
    public bool xWin;
    public bool OWin;

    private void Awake() {  
        board.SetActive(false);     
        StartButton.SetActive(false);
        turn = 0;
        gameStarted = gameEnded  = false;
        MenuButton.SetActive(false);
        xWin = OWin = false;
        RPC_SetName(PhotonNetwork.LocalPlayer.NickName, photonView.IsMine);
        Spawn();
        RPC_Prepare();
        SetMenuInfo();
    }  
    
    private void SetPlayer1_SO(){
        object[] mycustominitdata = new object[]{CharacterInfo.Instance.player_ScriptableObject.id};
        GameObject newplayer = PhotonNetwork.Instantiate(DefaultCharacter.name, new Vector3(-7f,2f,0), Quaternion.identity, 0, mycustominitdata) as GameObject;
    }

    private void SetPlayer2_SO(){
        object[] mycustominitdata = new object[]{CharacterInfo.Instance.player_ScriptableObject.id};
        GameObject newplayer = PhotonNetwork.Instantiate(DefaultCharacter.name, new Vector3(7f,2f,0), Quaternion.identity, 0, mycustominitdata) as GameObject;
    }

    private void Spawn(){
        if(photonView.IsMine){
            SetPlayer1_SO();
        }else{
            SetPlayer2_SO();
        }
    }

    [PunRPC]
    private void Prepare() {
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1){
            Infotext.text = "Waiting for other player";
        }else if(PhotonNetwork.CurrentRoom.PlayerCount == 2){
            if(photonView.IsMine){
                StartButton.SetActive(true);
                Infotext.text = "Start when you ready!";           
            }else{
                Infotext.text = "Waiting for host";    
            }
        }
    }

    private void RPC_Prepare(){   
        photonView.RPC(nameof(Prepare), RpcTarget.AllBuffered);
        Debug.Log("RPC_SetActivePlayer terpanggil");
    }

    [PunRPC]
    private void SetName(string name, bool isPlayer1){
        if(isPlayer1 && string.IsNullOrEmpty(player1name)){
            player1name = name;
        }else if(!isPlayer1 && string.IsNullOrEmpty(player2name)){
            player2name = name;
        }
        SetMenuInfo();
    }

    private void RPC_SetName(string name, bool isPlayer1){
        photonView.RPC(nameof(SetName), RpcTarget.AllBuffered, name, isPlayer1);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        player1name = player2name = "";
        if(gameStarted){
            Infotext.text = "Other player disconnected";
            MenuButton.SetActive(true);
            SetMenuInfo();
            PhotonNetwork.LeaveRoom();               
        }else{
            GameObject temp;
            temp = GameObject.FindGameObjectWithTag("Player");
            temp.transform.position = new Vector3(-6f,2f,0);
            Prepare();
            RPC_SetName(PhotonNetwork.LocalPlayer.NickName, photonView.IsMine);
            SetMenuInfo();
            StartButton.SetActive(false);
        }
    }

    private void SetMenuInfo(){   
        InfoRoom.text = "Room ID: " + PhotonNetwork.CurrentRoom.Name + "\n" + "Current Player: " + PhotonNetwork.CurrentRoom.PlayerCount + "/2";
        tmp_player1.text = player1name;
        tmp_player2.text = player2name;    
    }

    [PunRPC]
    private void StartFunction(){
        board.SetActive(true);
        StartButton.SetActive(false);
        Infotext.text = "";
        gameStarted  = true;
        Gameplay();
    }

    public void RPC_StartFunction(){
        photonView.RPC(nameof(StartFunction), RpcTarget.All);
    } 

    [PunRPC]
    public void buttonIsClick(int value){
        if(values[value] == 0){
            values[value] = (turn % 2) + 1;
            turn++;
            Gameplay();
            InstantiateObject(); 
        }

    }
    
    public void RPC_buttonIsClick(int value){
        if(photonView.IsMine && turn % 2 == 0 && !gameEnded){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
        }else if(!photonView.IsMine && turn % 2 != 0 && !gameEnded){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
        }
    }   

    public void Gameplay(){
        if(gameStarted && photonView.IsMine && turn % 2 == 0 && !gameEnded){
            Infotext.text = player1name + " Turn";
        }else if(gameStarted && photonView.IsMine && turn % 2 != 0 && !gameEnded){
            Infotext.text =  player2name + " Turn";
        }

        if(gameStarted && !photonView.IsMine && turn % 2 == 0 && !gameEnded){
            Infotext.text = player1name + " Turn";
        }else if(gameStarted && !photonView.IsMine && turn % 2 != 0 && !gameEnded){
            Infotext.text =  player2name + " Turn";
        }
        if(!gameEnded){
            xWinCondition();
            oWinCondition();  
        }

        if(gameEnded){
            if(xWin){
                Infotext.text = player1name + " Win the Game";
            }else if(OWin){
                Infotext.text = player2name + " Win the Game";
            }
            MenuButton.SetActive(true);
        }
        if(turn > 8 && !gameEnded){
            Infotext.text = "Tie";
            MenuButton.SetActive(true);
        }        
    }

    public void backToMainMenu(){
        SceneManager.LoadScene("Lobby");
        if(PhotonNetwork.InRoom){
            PhotonNetwork.LeaveRoom();
        }
    }
    
        public void xWinCondition(){
        if(values[0] == 1 && values[1] == 1 && values[2] == 1){ 
            gameEnded = true;
            xWin = true;
        }else if(values[3] == 1 && values[4] == 1 && values[5] == 1){ 
            gameEnded = true;
            xWin = true;
        }else if(values[6] == 1 && values[7] == 1 && values[8] == 1){
            gameEnded = true;
            xWin = true;
        }else if(values[0] == 1 && values[3] == 1 && values[6] == 1){
            gameEnded = true;
            xWin = true;
        }else if(values[1] == 1 && values[4] == 1 && values[7] == 1){
            gameEnded = true;
            xWin = true;
        }else if(values[2] == 1 && values[5] == 1 && values[8] == 1){
            gameEnded = true;
            xWin = true;
        }else if(values[0] == 1 && values[4] == 1 && values[8] == 1){
            gameEnded = true;
            xWin = true;
        }else if(values[2] == 1 && values[4] == 1 && values[6] == 1){
            gameEnded = true;
            xWin = true;
        }
    }

    public void oWinCondition(){
        if(values[0] == 2 && values[1] == 2 && values[2] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[3] == 2 && values[4] == 2 && values[5] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[6] == 2 && values[7] == 2 && values[8] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[0] == 2 && values[3] == 2 && values[6] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[1] == 2 && values[4] == 2 && values[7] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[2] == 2 && values[5] == 2 && values[8] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[0] == 2 && values[4] == 2 && values[8] == 2){
            gameEnded = true;
            OWin = true;
        }else if(values[2] == 2 && values[4] == 2 && values[6] == 2){
            gameEnded = true;
            OWin = true;
        }        
    }

    private void InstantiateObject(){
        if(values[0] == 1 && flag[0] == 0){
            Instantiate(lingkaran, new Vector3(-4f, 1.3f, 4f), Quaternion.identity); flag[0]++;
        }else if(values[1] == 1 && flag[1] == 0){
            Instantiate(lingkaran, new Vector3(0f, 1.3f, 4f), Quaternion.identity); flag[1]++;
        }else if(values[2] == 1 && flag[2] == 0){
            Instantiate(lingkaran, new Vector3(4f, 1.3f, 4f), Quaternion.identity); flag[2]++;
        }else if(values[3] == 1 && flag[3] == 0){
            Instantiate(lingkaran, new Vector3(-4f, 1.3f, 0f), Quaternion.identity); flag[3]++;
        }else if(values[4] == 1 && flag[4] == 0){
            Instantiate(lingkaran, new Vector3(0f, 1.3f, 0f), Quaternion.identity); flag[4]++;
        }else if(values[5] == 1 && flag[5] == 0){
            Instantiate(lingkaran, new Vector3(4f, 1.3f, 0f), Quaternion.identity); flag[5]++;
        }else if(values[6] == 1 && flag[6] == 0){
            Instantiate(lingkaran, new Vector3(-4f, 1.3f, -4f), Quaternion.identity); flag[6]++;
        }else if(values[7] == 1 && flag[7] == 0){
           Instantiate(lingkaran, new Vector3(0f, 1.3f, -4f), Quaternion.identity); flag[7]++;
        }else if(values[8] == 1 && flag[8] == 0){
            Instantiate(lingkaran, new Vector3(4f, 1.3f, -4f), Quaternion.identity); flag[8]++;
        }

        if(values[0] == 2 && flag[0] == 0){
            Instantiate(silang, new Vector3(-4f, 1.3f, 4f), Quaternion.identity); flag[0]++;
        }else if(values[1] == 2 && flag[1] == 0){
            Instantiate(silang, new Vector3(0f, 1.3f, 4f), Quaternion.identity); flag[1]++;
        }else if(values[2] == 2 && flag[2] == 0){
            Instantiate(silang, new Vector3(4f, 1.3f, 4f), Quaternion.identity); flag[2]++;
        }else if(values[3] == 2 && flag[3] == 0){
            Instantiate(silang, new Vector3(-4f, 1.3f, 0f), Quaternion.identity); flag[3]++;
        }else if(values[4] == 2 && flag[4] == 0){
            Instantiate(silang, new Vector3(0f, 1.3f, 0f), Quaternion.identity); flag[4]++;
        }else if(values[5] == 2 && flag[5] == 0){
            Instantiate(silang, new Vector3(4f, 1.3f, 0f), Quaternion.identity); flag[5]++;
        }else if(values[6] == 2 && flag[6] == 0){
            Instantiate(silang, new Vector3(-4f, 1.3f, -4f), Quaternion.identity); flag[6]++;
        }else if(values[7] == 2 && flag[7] == 0){
            Instantiate(silang, new Vector3(0f, 1.3f, -4f), Quaternion.identity); flag[7]++;
        }else if(values[8] == 2 && flag[8] == 0){
            Instantiate(silang, new Vector3(4f, 1.3f, -4f), Quaternion.identity); flag[8]++;
        }
    }
}
