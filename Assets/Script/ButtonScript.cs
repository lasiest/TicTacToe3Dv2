using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public CharacterInfo characterInfo;
    public TMP_Text Infotext;
    public int id;

    void Start()
    {
        Button temp = GetComponent<Button>();
        temp.onClick.AddListener(()=>characterInfo.SetCharacter(id));
    }

}
