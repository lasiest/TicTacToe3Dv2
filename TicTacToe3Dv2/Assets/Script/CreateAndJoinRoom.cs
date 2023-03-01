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

    public void createRoom(){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, TypedLobby.Default);
    }
    public void joinRoom(){
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
