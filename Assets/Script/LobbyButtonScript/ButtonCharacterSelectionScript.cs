using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonCharacterSelectionScript : MonoBehaviour
{
    public CharacterInfo characterInfo;
    public TMP_Text Infotext;
    public int id;
    public Player_ScriptableObject player_ScriptableObject;

    void Start()
    {
        Button temp = GetComponent<Button>();
        temp.onClick.AddListener(()=>characterInfo.SetPlayer_ScriptableObject(player_ScriptableObject));
    }

}
