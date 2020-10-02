using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update() {
        ProcessInput();    
    }

    private void ProcessInput() {

        // Can thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            print("Thrusting");
        }

        if (Input.GetKey(KeyCode.A)) {
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D)) {
            print("Rotating right");
        }
    }
}
