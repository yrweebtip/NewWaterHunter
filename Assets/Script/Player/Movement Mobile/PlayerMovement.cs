using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    [Header("Mobile Inputs")]
    public Joystick movementJoystick;

    [Header("Movement Settings")]
    public float movementSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    [Header("Audio Footsteps")]
    public AudioSource footstepAudioSource;
    public AudioClip[] footstepClips; 
    public float walkStepInterval = 0.5f;   
    public float sprintStepInterval = 0.3f; 
    private float stepTimer;

    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private bool isSprinting = false;

    [Header("UI Elements")]
    public GameObject collectButton;

    public static GameObject tombolAmbilStatic;

    void Awake()
    {
        tombolAmbilStatic = collectButton;
    }

    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        currentSpeed = movementSpeed;

        if (tombolAmbilStatic != null)
        {
            tombolAmbilStatic.SetActive(false);
        }
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        HandleMovement();
        HandleSprint();

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        float horizontal = movementJoystick.Horizontal;
        float vertical = movementJoystick.Vertical;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * currentSpeed * Time.deltaTime);

      
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            stepTimer -= Time.deltaTime; 

            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = isSprinting ? sprintStepInterval : walkStepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void HandleSprint()
    {
        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = movementSpeed;
        }
    }

    private void PlayFootstepSound()
    {
        
        if (footstepAudioSource != null && footstepClips.Length > 0)
        {
            
            int randomIndex = Random.Range(0, footstepClips.Length);
            footstepAudioSource.PlayOneShot(footstepClips[randomIndex]);
        }
    }

    public void OnSprintDown()
    {
        isSprinting = true;
    }

    public void OnSprintUp()
    {
        isSprinting = false;
    }

    public void AmbilItem()
    {
        if (CollectItem.itemTerdekat != null)
        {
            CollectItem.itemTerdekat.Collect();
        }
    }
}