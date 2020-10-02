using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    private void Start() {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {

       Thrust();
       Rotate();    
    }

    private void Thrust() {

        // Can thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {

            rigidBody.AddRelativeForce(Vector3.up);
            // So that audio doesn't layer
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            print("Thrusting");
        }
        // Stop audio when it is not thrusting
        else {
            audioSource.Stop();
        }
    }

    private void Rotate() {

        // Take manual control of rotation
        rigidBody.freezeRotation = true;

        // Rotate left and right in z-axis
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back);
            print("Rotating right");
        }

        // Resume physics control of rotation
        rigidBody.freezeRotation = false;
    }
}
