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
    public bool player1;
    public bool player2;
    public GameObject Character1;
    public GameObject Character2;
    public GameObject Character3;
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
    public GameObject[] playerObject;
    private void Start() {  
        player1 = player2 = false;
        board.SetActive(false);     
        StartButton.SetActive(false);
        turn = 0;
        gameStarted = gameEnded  = false;
        MenuButton.SetActive(false);
        xWin = OWin = false;
        RPC_SetName(PhotonNetwork.LocalPlayer.NickName, photonView.IsMine);
        RPC_SetActivePlayer();
        SetMenuInfo();
        
    }

    private void Update() {
        playerObject = GameObject.FindGameObjectsWithTag("Player");
    }

    private void SetPlayer1(){
       if(CharacterInfo.Instance.character == 1){
            PhotonNetwork.Instantiate(Character1.name, new Vector3(-6f,2f,0), Quaternion.identity);
            Debug.Log("1");
        }else if(CharacterInfo.Instance.character == 2){
            PhotonNetwork.Instantiate(Character2.name, new Vector3(-6f,2f,0), Quaternion.identity);
            Debug.Log("2");
        }else if(CharacterInfo.Instance.character == 3){
            PhotonNetwork.Instantiate(Character3.name, new Vector3(-6f,2f,0), Quaternion.identity);
            Debug.Log("3");
        }
    }

    private void SetPlayer2(){
        if(CharacterInfo.Instance.character == 1){
            PhotonNetwork.Instantiate(Character1.name, new Vector3(6f,2f,0), Quaternion.identity);
            Debug.Log("4");
        }else if(CharacterInfo.Instance.character == 2){
            PhotonNetwork.Instantiate(Character2.name, new Vector3(6f,2f,0), Quaternion.identity);
            Debug.Log("5");
        }else if(CharacterInfo.Instance.character == 3){
            PhotonNetwork.Instantiate(Character3.name, new Vector3(6f,2f,0), Quaternion.identity);
            Debug.Log("6");
        }
    }

    [PunRPC]
    private void SetActivePlayers() {
        // DestroyWithTag("Player");
        if(PhotonNetwork.CurrentRoom.PlayerCount == 1 && photonView){
            if(photonView.IsMine && !player1){
                SetPlayer1();
                player1 = true;
                player2 = false;                
            }
            Debug.Log("set active player 1");
            Infotext.text = "Waiting for other player";
        }else if(PhotonNetwork.CurrentRoom.PlayerCount == 2){
            if(!photonView.IsMine && !player2){
                // DestroyWithTag("Player");
                SetPlayer2();
                player1 = true;
                player2 = true;
                Debug.Log("set active player 2");
            }if(photonView.IsMine){
                player1 = true;
                StartButton.SetActive(true);
                Infotext.text = "Start when you ready!";                
            }else{
                Infotext.text = "Waiting for host";
            }
        }
    }

    private void RPC_SetActivePlayer(){
        photonView.RPC(nameof(SetActivePlayers), RpcTarget.AllBuffered);
        Debug.Log("RPC_SetActivePlayer terpanggil");
    }

    [PunRPC]
    private void SetName(string name, bool isPlayer1){
        if(isPlayer1 && string.IsNullOrEmpty(player1name)){
            Debug.Log("Set player 1 name" + name);
            player1name = name;
        }else if(!isPlayer1 && string.IsNullOrEmpty(player2name)){
            Debug.Log("Set player 2 name" + name);
            player2name = name;
        }
        SetMenuInfo();
    }

    private void RPC_SetName(string name, bool isPlayer1){
        photonView.RPC(nameof(SetName), RpcTarget.AllBuffered, name, isPlayer1);
    }

    // public override void OnPlayerEnteredRoom(Player newPlayer)
    // {
    //     DestroyWithTag("Player");
    // }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        player1name = player2name = "";
        DestroyWithTag("Player");
        if(gameStarted){
            Debug.Log("Someone Disconnected - On PLayer left room");
            Infotext.text = "Other player disconnected";
            MenuButton.SetActive(true);
            SetMenuInfo();
            PhotonNetwork.LeaveRoom();               
        }else{
            Debug.Log(otherPlayer.NickName + " left");
            DestroyWithTag("Player");
            player1 = player2 = false;
            SetActivePlayers();
            RPC_SetName(PhotonNetwork.LocalPlayer.NickName, photonView.IsMine);
            SetMenuInfo();
            StartButton.SetActive(false);
        }
    }

    void DestroyWithTag (string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject){
            PhotonNetwork.Destroy(oneObject);
            Debug.Log("Something Destroyed");            
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
        }  
    }
    
    public void RPC_buttonIsClick(int value){
        if(photonView.IsMine && turn % 2 == 0 && !gameEnded){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
            InstantiateObject();
        }else if(!photonView.IsMine && turn % 2 != 0 && !gameEnded){
            photonView.RPC(nameof(buttonIsClick), RpcTarget.All, value);
            InstantiateObject();
        }else{
            Debug.Log("Not your turn");
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
