using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceHolderForButtonScript : MonoBehaviour
{
    public Player_ScriptableObject[] character;
    public CharacterInfo characterInfo;
    public GameObject placeholder;
    public GameObject button;

    private void Awake() {
        foreach (Player_ScriptableObject info in character){
            Debug.Log(info.id);
            GameObject newButton = Instantiate(button) as GameObject;
            newButton.SetActive(true);
            newButton.transform.SetParent(placeholder.transform, false);
            ButtonScript buttonScript;
            buttonScript = newButton.GetComponent<ButtonScript>();
            buttonScript.id = info.id;
            buttonScript.Infotext.SetText(info.name);
        }
    }

    
}
