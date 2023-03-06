using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo Instance {get; private set;}
    public int character;

    private void Awake() {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void SetCharacter1(){
        character = 1;
    }
    public void SetCharacter2(){
        character = 2;
    }
    public void SetCharacter3(){
        character = 3;
    }
}
