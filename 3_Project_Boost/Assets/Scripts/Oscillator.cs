using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

    [SerializeField] private Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] private float period = 2f;

    // TODO remove from inspector later
    [SerializeField] [Range(0, 1)] private float movementFactor; // 0 for not moved, 1 for fully moved.

    private Vector3 startingPos;

    private void Start() {

        startingPos = transform.position;
    }

    private void Update() {

        if (period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period; // Grows continually from 0

        const float tau = 2f * Mathf.PI;
        float rawSineWave = Mathf.Sin(cycles * tau); // from -1 to +1

        movementFactor = rawSineWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}

