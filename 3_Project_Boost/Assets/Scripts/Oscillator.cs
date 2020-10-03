using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] private Vector3 movementVector;
    // TODO remove from inspector later
    [SerializeField] [Range(0, 1)] private float movementFactor; // 0 for not moved, 1 for fully moved.

    private Vector3 startingPos;

    private void Start() {

        startingPos = transform.position;
    }

    private void Update() {

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}

