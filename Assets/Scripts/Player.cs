using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private Animator animator;
    private static readonly int Velocity = Animator.StringToHash("velocity");
    [SerializeField] private Rigidbody rb;
    public float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float sprintMultiplier = 1.4f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Camera cam;
    [SerializeField] private bool isFrozen = false;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private float currentStamina = 100;
    [SerializeField] private float maxStamina = 100;
    
    private bool isSprinting = false;
    private Vector3 velocity;
    private float rotationY = 0f;
    
    
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Physics.gravity = Vector3.down * gravity;
        //animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isFrozen) return;
    
        // Sprint handling
        if (isSprinting && currentStamina > 0)
        {
            currentStamina -= Time.fixedDeltaTime * 10; // Reduce stamina while sprinting
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isSprinting = false;
            }
        }
        else if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += Time.fixedDeltaTime * 5; // Regenerate stamina when not sprinting
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }

        staminaSlider.value = currentStamina / maxStamina;
    
        // Apply movement velocity to the Rigidbody
        //Vector3 moveDirection = cam.transform.forward * velocity.z + cam.transform.right * velocity.x;
        Vector3 moveDirection = transform.forward * velocity.z + transform.right * velocity.x;
        moveDirection.y = 0; // Keep movement on the horizontal plane
        rb.linearVelocity = moveDirection * speed + new Vector3(0, rb.linearVelocity.y, 0); // Maintain existing Y velocity (gravity)
    
        
        // Update animator parameters with current speed
        float currentSpeed = rb.linearVelocity.magnitude;
        animator.SetFloat(Velocity, currentSpeed);
        animator.SetFloat("VelocityX",velocity.x);
        animator.SetFloat("VelocityZ",velocity.z);
        animator.SetBool("Run",isSprinting);
        animator.SetBool("IsGrounded",CheckForGround());

        /*// Movement Sounds
        if (currentSpeed > 0.1f && CheckForGround()) // Si el jugador se está moviendo
        {
            if (isSprinting)
            {
                if (!runSound.isPlaying)
                {
                    runSound.Play();
                    walkSound.Stop();
                }
            }
            else
            {
                if (!walkSound.isPlaying)
                {
                    walkSound.Play();
                    runSound.Stop();
                }
            }
        }
        else // Si el jugador no se mueve, detener ambos sonidos
        {
            walkSound.Stop();
            runSound.Stop();
        }*/
    }


    #region Movement

    // Handle movement input
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);
    }
    
    // Handle jumping
    public void OnJump()
    {
        if (!CheckForGround() || isFrozen) return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    // Handle looking/turning
    public void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // Adjust player rotation (Y-axis)
        transform.Rotate(Vector3.up * input.x * sensitivity);

        // Adjust camera rotation (X-axis, clamped)
        rotationY -= input.y * sensitivity;
        rotationY = Mathf.Clamp(rotationY, -80f, 75f);
        cam.transform.localEulerAngles = new Vector3(rotationY, 0f, 0f);
    }
    
    // Handle sprinting
    public void OnSprint(InputValue value)
    {
        if (value.isPressed && currentStamina > 0)
        {
            isSprinting = true;
            speed *= sprintMultiplier;
        }
        else
        {
            isSprinting = false;
            speed /= sprintMultiplier;
        }
    }

    public void OnAttack(InputValue value)
    {
        animator.SetTrigger("Attack");
    }
    

    
    // Check if the player is on the ground
    public bool CheckForGround()
    {
        RaycastHit hit;
        return Physics.Raycast(groundCheck.position, Vector3.down, out hit, 0.3f);
    }
    #endregion

}