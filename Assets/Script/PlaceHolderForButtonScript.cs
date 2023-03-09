using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceHolderForButtonScript : MonoBehaviour
{
    public GameObject placeholder;
    public GameObject button;

    private void Awake() {
        foreach (Player_ScriptableObject info in CharacterInfo.Instance.arrayOfCharacterSelection){
            GameObject newButton = Instantiate(button) as GameObject;
            newButton.SetActive(true);
            newButton.transform.SetParent(placeholder.transform, false);
            ButtonCharacterSelectionScript buttonScript;
            buttonScript = newButton.GetComponent<ButtonCharacterSelectionScript>();
            buttonScript.id = info.id;
            buttonScript.Infotext.SetText(info.name);
            buttonScript.player_ScriptableObject = info;
        }
    }

    
}
