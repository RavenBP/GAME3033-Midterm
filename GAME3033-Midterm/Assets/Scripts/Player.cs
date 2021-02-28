using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private InputActionReference pauseControl;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject pausePanel;

    [Header("Player Values")]
    [SerializeField]
    public int playerHealth = 100;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float rotationSpeed = 4.0f;

    private CharacterController controller;
    private AudioSource audioSource;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Transform cameraMainTransform;

    private readonly int MovementSpeedHash = Animator.StringToHash("MovementSpeed");
    private readonly int IsFallingHash = Animator.StringToHash("IsFalling");

    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        pauseControl.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        pauseControl.action.Disable();
    }

    private void Awake()
    {
        PlayerHealth.playerHealth = 100;
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        audioSource = gameObject.GetComponent<AudioSource>();
        cameraMainTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CheckHealth();

        groundedPlayer = controller.isGrounded;

        // Player is grounded / not falling
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            animator.SetBool(IsFallingHash, false);
        }

        // Check Pause Input
        if (pauseControl.action.triggered)
        {
            PauseGame();
        }

        // Get Player Movement Input
        Vector2 movement = movementControl.action.ReadValue<Vector2>();

        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0.0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        animator.SetFloat(MovementSpeedHash, (Mathf.Abs(movement.x) + Mathf.Abs(movement.y)));

        // Changes the height position of the player..
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            audioSource.Play();
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (Mathf.Abs(playerVelocity.y) > 0.4)
        {
            animator.SetBool(IsFallingHash, true);
        }

        if (movement != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    void PauseGame()
    {
        Debug.Log("Player pressed pause");
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pausePanel.SetActive(true);
    }

    void CheckHealth()
    {
        if (PlayerHealth.playerHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("GameOverScene");
        }
    }
}
