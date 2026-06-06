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
    public AudioClip[] footstepClips; // Menggunakan array agar bisa menaruh lebih dari 1 suara (biar tidak monoton)
    public float walkStepInterval = 0.5f;   // Jarak waktu antar langkah saat jalan santai
    public float sprintStepInterval = 0.3f; // Jarak waktu antar langkah saat lari
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

        // ==========================================
        // LOGIKA FOOTSTEP AUDIO (VERSI JOYSTICK)
        // ==========================================

        // Kita HAPUS syarat isGrounded.
        // Sekarang hanya mengecek: "Apakah joystick sedang digeser lebih dari 10%?"
        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            stepTimer -= Time.deltaTime; // Kurangi timer

            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                // Atur ulang timer tergantung sedang lari atau jalan
                stepTimer = isSprinting ? sprintStepInterval : walkStepInterval;
            }
        }
        else
        {
            // Reset timer ke 0 jika joystick dilepas
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

    // ==========================================
    // FUNGSI MEMUTAR SUARA LANGKAH
    // ==========================================
    private void PlayFootstepSound()
    {
        // Pastikan AudioSource dan Clip sudah diisi di Inspector agar tidak error
        if (footstepAudioSource != null && footstepClips.Length > 0)
        {
            // Pilih satu suara langkah secara acak dari kumpulan array
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