using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mainThrust = 800f;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

    // Game States
    private enum State { Alive, Dying, Transcending };
    private State state = State.Alive;

    private void Start() {

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
       // TODO somewhere stop sound on death
       // Can control only when player is alive
       if (state == State.Alive) {
            Thrust();
            Rotate();
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
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

        // Resume physics control of rotation
        rigidBody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision) {

        // Do nothing when player is not alive
        if (state != State.Alive) { return; }

        switch (collision.gameObject.tag) {

            case "Friendly":
                // Do nothing
                break;

            case "Finish":
                state = State.Transcending;
                Invoke("LoadNextLevel", 1f); // TODO parameterise time
                break;

            default:
                print("Hit something deadly");
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }
    private void LoadNextLevel() {
        SceneManager.LoadScene(1); // TODO allow for more than 2 levels
    }
    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }
}
