using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 5f;
    
    private void Start() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        characterController.Move(move * Time.deltaTime * speed);
    }
}
