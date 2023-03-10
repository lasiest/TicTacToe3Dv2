using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public SpawnPlayer spawnPlayer;
    void Start()
    {
        Button temp = GetComponent<Button>();
        temp.onClick.AddListener(()=>spawnPlayer.backToMainMenu());
    }
}
