using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField user_Nickname;
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_Text Infotext;

    // public int character;
    
    public void InputNickname(){
        if(user_Nickname.text == ""){
            Infotext.text = "Name must not empty";
        }else{
            PhotonNetwork.NickName = user_Nickname.text;
            Debug.Log(user_Nickname.text);
            Infotext.text = "Name has been set as " + PhotonNetwork.NickName;
            // PhotonNetwork.NickName = user_Nickname.text;         
        }

    }

    public void createRoom(){
        if(PhotonNetwork.NickName == ""){
            Infotext.text = "Name must not empty";
        }else{
            if(createInput.text == ""){
                Debug.Log("Room name must not empty");
                Infotext.text = "Room name must not empty";
            }else if(CharacterInfo.Instance.character == 0){
                Debug.Log("You must pick color");
            }
            else{
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);            
            }            
        }


    }
    public void joinRoom(){
        if(PhotonNetwork.NickName == ""){
           Infotext.text = "Name must not empty";     
        }else{
            if(CharacterInfo.Instance.character == 0){
                Debug.Log("You must pick color");
            }else{
                PhotonNetwork.JoinRoom(joinInput.text);
            } 
        }
    }

    public void RandomRoom(){
        if(PhotonNetwork.NickName == ""){
            Infotext.text = "Name must not empty";    
        }else{
            if(CharacterInfo.Instance.character == 0){
                Debug.Log("You must pick color");
            }else{
                PhotonNetwork.JoinRandomRoom(); 
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        Infotext.text = message;
    }

    public override void OnJoinRandomFailed(short returnCode, string message){
        Debug.Log(message);
        Infotext.text = message;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }


}
