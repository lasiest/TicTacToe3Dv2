using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject player1;
    public GameObject player2;
    public GameObject StartButton;
    public GameObject MenuButton;
    public GameObject lingkaran;
    public GameObject silang;
    public GameObject board;
    public TMP_Text Infotext;
    public bool gameStarted;
    public bool gameEnded;
    public int turn;
    public int[] values;
    public int[] flag;
    public bool xWin;
    public bool OWin;

    private void Awake() {
        player1.SetActive(false);
        player2.SetActive(false);
        board.SetActive(false);     
        StartButton.SetActive(false);
        RPC_SetActivePlayer();
        turn = 0;
        gameStarted = false;
        gameEnded = false;
        MenuButton.SetActive(false);
        xWin = false;
        OWin = false;
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

    // private void RPC_SetActiveStartButton(){
    //     photonView.RPC(nameof(SetActivePlayers), RpcTarget.Others);
    // }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Someone Disconnected - On PLayer left room");
        Infotext.text = "Other player disconnected";
        MenuButton.SetActive(true);
    }

    private void Update() {
        //Ini Buat Cek berapa orang yang lagi didalem room
        // Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        // if(PhotonNetwork.CurrentRoom.PlayerCount == 1 && gameStarted){
        //     Infotext.text = "Other player disconnected";
        //     MenuButton.SetActive(true);
        // }


        if(player1.activeInHierarchy && player2.activeInHierarchy && photonView.IsMine && !gameStarted && PhotonNetwork.CurrentRoom.PlayerCount == 2){
            Infotext.text = "Start when you ready!";
            StartButton.SetActive(true);
        }
        if(gameStarted && photonView.IsMine && turn % 2 == 0 && !gameEnded && PhotonNetwork.CurrentRoom.PlayerCount == 2){
            Infotext.text = "YourTurn";
        }else if(gameStarted && photonView.IsMine && turn % 2 != 0){
            Infotext.text = "Wait for your turn";
        }

        if(gameStarted && !photonView.IsMine && turn % 2 == 0 && !gameEnded && PhotonNetwork.CurrentRoom.PlayerCount == 2){
            Infotext.text = "Wait for your turn";
        }else if(gameStarted && !photonView.IsMine && turn % 2 != 0){
            Infotext.text = "YourTurn";
        }
        xWinCondition();
        oWinCondition();            

        if(gameEnded){
            if(xWin){
                Infotext.text = "Player 1 Win the Game";
            }else if(OWin){
                Infotext.text = "Player 2 Win the Game";
            }
            MenuButton.SetActive(true);
        }
        if(turn > 8 && !gameEnded){
            Infotext.text = "Tie";
            MenuButton.SetActive(true);
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
        // xWinCondition();
        // oWinCondition();     
    }
    
    public void RPC_buttonIsClick(int value){
        if(photonView.IsMine && turn % 2 == 0 && !gameEnded && PhotonNetwork.CurrentRoom.PlayerCount == 2){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
            InstantiateObject();
        }else if(!photonView.IsMine && turn % 2 != 0 && !gameEnded && PhotonNetwork.CurrentRoom.PlayerCount == 2){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
            InstantiateObject();
        }else{
            Debug.Log("Not your turn");
        }
    }   

    public void backToMainMenu(){
        SceneManager.LoadScene("Lobby");
        PhotonNetwork.LeaveRoom();
    }

    // void OnPlayerLeftRoom(){
    //     Debug.Log("Someone Disconnected - On PLayer left room");
    // }

        public void xWinCondition(){
        if(values[0] == 1 && values[1] == 1 && values[2] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[3] == 1 && values[4] == 1 && values[5] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[6] == 1 && values[7] == 1 && values[8] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[0] == 1 && values[3] == 1 && values[6] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[1] == 1 && values[4] == 1 && values[7] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[2] == 1 && values[5] == 1 && values[8] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[0] == 1 && values[4] == 1 && values[8] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }else if(values[2] == 1 && values[4] == 1 && values[6] == 1){
            Debug.Log("X win"); 
            gameEnded = true;
            xWin = true;
        }
    }

    public void oWinCondition(){
        if(values[0] == 2 && values[1] == 2 && values[2] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[3] == 2 && values[4] == 2 && values[5] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[6] == 2 && values[7] == 2 && values[8] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[0] == 2 && values[3] == 2 && values[6] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[1] == 2 && values[4] == 2 && values[7] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[2] == 2 && values[5] == 2 && values[8] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[0] == 2 && values[4] == 2 && values[8] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }else if(values[2] == 2 && values[4] == 2 && values[6] == 2){
            Debug.Log("O win"); 
            gameEnded = true;
            OWin = true;
        }        
    }

    private void InstantiateObject(){
        if(values[0] == 1 && flag[0] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(-4f, 1.3f, 4f), Quaternion.identity); flag[0] = 1;
        }else if(values[1] == 1 && flag[1] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(0f, 1.3f, 4f), Quaternion.identity); flag[1] = 1;
        }else if(values[2] == 1 && flag[2] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(4f, 1.3f, 4f), Quaternion.identity); flag[2] = 1;
        }else if(values[3] == 1 && flag[3] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(-4f, 1.3f, 0f), Quaternion.identity); flag[3] = 1;
        }else if(values[4] == 1 && flag[4] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(0f, 1.3f, 0f), Quaternion.identity); flag[4] = 1;
        }else if(values[5] == 1 && flag[5] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(4f, 1.3f, 0f), Quaternion.identity); flag[5] = 1;
        }else if(values[6] == 1 && flag[6] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(-4f, 1.3f, -4f), Quaternion.identity); flag[6] = 1;
        }else if(values[7] == 1 && flag[7] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(0f, 1.3f, -4f), Quaternion.identity); flag[7] = 1;
        }else if(values[8] == 1 && flag[8] != 1){
            PhotonNetwork.Instantiate(lingkaran.name, new Vector3(4f, 1.3f, -4f), Quaternion.identity); flag[8] = 1;
        }

        if(values[0] == 2 && flag[0] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(-4f, 1.3f, 4f), Quaternion.identity); flag[0] = 1;
        }else if(values[1] == 2 && flag[1] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(0f, 1.3f, 4f), Quaternion.identity); flag[1] = 1;
        }else if(values[2] == 2 && flag[2] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(4f, 1.3f, 4f), Quaternion.identity); flag[2] = 1;
        }else if(values[3] == 2 && flag[3] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(-4f, 1.3f, 0f), Quaternion.identity); flag[3] = 1;
        }else if(values[4] == 2 && flag[4] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(0f, 1.3f, 0f), Quaternion.identity); flag[4] = 1;
        }else if(values[5] == 2 && flag[5] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(4f, 1.3f, 0f), Quaternion.identity); flag[5] = 1;
        }else if(values[6] == 2 && flag[6] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(-4f, 1.3f, -4f), Quaternion.identity); flag[6] = 1;
        }else if(values[7] == 2 && flag[7] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(0f, 1.3f, -4f), Quaternion.identity); flag[7] = 1;
        }else if(values[8] == 2 && flag[8] != 1){
            PhotonNetwork.Instantiate(silang.name, new Vector3(4f, 1.3f, -4f), Quaternion.identity); flag[8] = 1;
        }
    }
}
