using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private void Start() {
        
    }

    private void Update() {
        ProcessInput();    
    }

    private void ProcessInput() {

        // Can thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {
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
