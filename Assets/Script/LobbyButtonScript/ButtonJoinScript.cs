using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonJoinScript : MonoBehaviour
{
    public CreateAndJoinRoom createAndJoinRoom;
    void Start()
    {
        Button temp = GetComponent<Button>();
        temp.onClick.AddListener(()=>createAndJoinRoom.joinRoom());
    }
}
