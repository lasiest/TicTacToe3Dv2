using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class isClicked : MonoBehaviour
{   
    public int value;
    public SpawnPlayer spawnPlayer;
    private void OnMouseDown() {
        Debug.Log(value);
        // spawnPlayer.buttonIsClick(value);
        spawnPlayer.RPC_buttonIsClick(value);
        // if(spawnPlayer.turn % 2 == 0){
        //     Instantiate
        // }
    }
}
