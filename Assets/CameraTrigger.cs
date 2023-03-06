using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{   
    public GameObject player;
    public GameObject camera1;
    public GameObject camera2;

    private void Awake() {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Camera2")){
            Debug.Log("Trigger Camera 2");
            camera2.SetActive(true);
            camera1.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Camera2")){
            Debug.Log("Trigger Camera 2");
            camera1.SetActive(true);
            camera2.SetActive(false);
        }        
    }
}
