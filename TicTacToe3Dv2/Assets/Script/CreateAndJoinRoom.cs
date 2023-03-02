using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_Text Infotext;

    public void createRoom(){
        if(createInput.text == ""){
            Debug.Log("Room name must not empty");
            Infotext.text = "Room name must not empty";
        }else{
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);            
        }

    }
    public void joinRoom(){
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        // base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Room not found");
        Infotext.text = "Room not found";
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

}
