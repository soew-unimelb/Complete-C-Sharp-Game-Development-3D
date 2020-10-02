using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mainThrust = 800f;

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

    private void OnCollisionEnter(Collision collision) {

        print("Collided");

        switch (collision.gameObject.tag) {

            case "Friendly":
                print("OK"); // TODO remove
                break;
            case "Fuel":
                print("Fuel"); // TODO remove
                break;
            default:
                print("Dead"); // TODO remove
                // TODO kill player
                break;
        }
    }

    private void Thrust() {

        float thrustingThisFrame = mainThrust * Time.deltaTime;

        // Can thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {

            rigidBody.AddRelativeForce(Vector3.up * thrustingThisFrame);
            // So that audio doesn't layer
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }
            //print("Thrusting");
        }
        // Stop audio when it is not thrusting
        else {
            audioSource.Stop();
        }
    }

    private void Rotate() {

        // Take manual control of rotation
        rigidBody.freezeRotation = true;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        // Rotate left and right in z-axis
        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
            //print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
            //print("Rotating right");
        }

        // Resume physics control of rotation
        rigidBody.freezeRotation = false;
    }
}
