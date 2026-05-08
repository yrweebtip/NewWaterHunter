using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    [Header("Mobile Inputs")]
    // Hubungkan On-Screen Joystick dari UI ke variabel ini
    public Joystick movementJoystick;

    [Header("Movement Settings")]
    public float movementSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float gravity = -9.81f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;

    private Vector3 velocity;
    private bool isGrounded;
    private float currentSpeed;
    private bool isSprinting = false;

    [Header("UI Elements")]
    public GameObject collectButton;

    public static GameObject tombolAmbilStatic;

    void Awake()
    {
        // Menyalin referensi UI ke variabel statis saat game dimulai
        tombolAmbilStatic = collectButton;
    }

    void Start()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        currentSpeed = movementSpeed;

        // Nonaktifkan tombol saat game dimulai
        if (tombolAmbilStatic != null)
        {
            tombolAmbilStatic.SetActive(false);
        }
    }

    void Update()
    {
        // Mengecek apakah pemain menyentuh tanah agar gravitasi stabil
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        HandleMovement();
        HandleSprint();

        // Menjalankan gravitasi
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void HandleMovement()
    {
        // Input dari Joystick
        float horizontal = movementJoystick.Horizontal;
        float vertical = movementJoystick.Vertical;

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * currentSpeed * Time.deltaTime);
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

    // ==========================================
    // FUNGSI PUBLIC UNTUK TOMBOL SPRINT DI UI
    // ==========================================

    // Gunakan Event Trigger (PointerDown) pada Button Sprint
    public void OnSprintDown()
    {
        isSprinting = true;
    }

    // Gunakan Event Trigger (PointerUp) pada Button Sprint
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