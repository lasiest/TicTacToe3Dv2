using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteUnmuteScript : MonoBehaviour
{
    public VoiceManagerScript voiceManagerScript;
    void Start()
    {
        Button temp = GetComponent<Button>();
        temp.onClick.AddListener(()=>voiceManagerScript.Mute_Unmute());
    }
}
