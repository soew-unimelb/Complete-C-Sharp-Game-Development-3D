using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] private float rcsThrust = 100f;
    [SerializeField] private float mainThrust = 800f;
    [SerializeField] private float levelLoadDelay = 1f;

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
    private bool collisionsDisabled = false;

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

        if (Debug.isDebugBuild) { RespondToDebugKeys(); }
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
    private void RespondToDebugKeys() {

        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }
        // Toggle collision
        else if (Input.GetKeyDown(KeyCode.C)) {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private void OnCollisionEnter(Collision collision) {

        // Ignore collisions when not alive or when it is diabled
        if (state != State.Alive || collisionsDisabled) { return; } // TODO to check

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
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    private void StartDeathSequence() {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }
    private void LoadNextLevel() {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        int nextSceneIndex = (currentSceneIndex + 1) % totalScenes;
        
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }
}
