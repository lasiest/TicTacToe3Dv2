using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IsClicked : MonoBehaviour
{   
    public int value;
    public SpawnPlayer spawnPlayer;
    private void OnMouseDown() {
        Debug.Log(value);
        spawnPlayer.RPC_buttonIsClick(value);
    }
}
