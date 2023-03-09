using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRandomScript : MonoBehaviour
{
    public CreateAndJoinRoom createAndJoinRoom;
    void Start()
    {
        Button temp = GetComponent<Button>();
        temp.onClick.AddListener(()=>createAndJoinRoom.RandomRoom());
    }
}
