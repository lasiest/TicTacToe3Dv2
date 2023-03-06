using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void TicTacToeScene(){
        SceneManager.LoadScene("LoadingScene");
    }
    public void CinemachineScene(){
        SceneManager.LoadScene("CinemachineScene");
    }
}
