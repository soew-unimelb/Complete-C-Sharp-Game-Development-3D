using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mainThrust = 800f;

    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip success;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ParticleSystem successParticles;

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
       // Can control only when player is alive
       if (state == State.Alive) {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }
    private void RespondToThrustInput() {

        // Can thrust while rotating
        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        }
        // Stop audio when it is not thrusting
        else {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }
    private void RespondToRotateInput() {

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
    private void ApplyThrust() {

        float thrustingThisFrame = mainThrust * Time.deltaTime;

        rigidBody.AddRelativeForce(Vector3.up * thrustingThisFrame);
        // So that audio doesn't layer
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void OnCollisionEnter(Collision collision) {

        // Ignore collisions when not alive.
        if (state != State.Alive) { return; }

        switch (collision.gameObject.tag) {

            case "Friendly":
                // Do nothing
                break;

            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }
    private void StartSuccessSequence() {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", 1f); // TODO parameterise time
    }
    private void StartDeathSequence() {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", 1f);
    }
    private void LoadNextLevel() {
        SceneManager.LoadScene(1); // TODO allow for more than 2 levels
    }
    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }
}
