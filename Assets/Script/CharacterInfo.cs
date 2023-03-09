using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo Instance {get; private set;}
    public Player_ScriptableObject player_ScriptableObject;
    public Player_ScriptableObject[] arrayOfCharacterSelection;

    private void Awake() {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void SetPlayer_ScriptableObject(Player_ScriptableObject scriptableObject){
        player_ScriptableObject = scriptableObject;
    }
}
