using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "NewPlayerSelection", menuName ="PlayerSelection")]

public class Player_ScriptableObject : ScriptableObject
{
    public Material color;
    public int id;
    public new string name;
}
