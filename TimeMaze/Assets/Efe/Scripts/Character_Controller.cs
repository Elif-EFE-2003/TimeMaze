using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Character_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSens = 100f;

    public float maxSprintTime = 5f;
    public float sprintSpeed = 7f;
    public float coolDownSpeed = 0.5f;
    private float currentSprintTime;
    private float currentSpeed;

    public Transform GroundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;
    public bool isGrounded;


    public Transform playerCamera;
    public GameObject lantern;

    private CharacterController controller;
    public Slider sprintSlider;

    private float rotationY = 0f;
    private float rotationX = 0f;

    private Vector3 velocity;
    public float gravity = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        currentSprintTime = maxSprintTime;
        currentSpeed = moveSpeed;

        if (sprintSlider != null)
        {
            sprintSlider.minValue = 0f;
            sprintSlider.maxValue = maxSprintTime;
            sprintSlider.value = currentSprintTime;
        }
    }

    private void Update()
    {
        Mouse();
        GroundCheckControl();
        Movement();
        LanternOpenClose();
    }

    private void Mouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void Movement()
    {
        // Yer kontrolü
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // yere yapýþma efekti
        }

        // Input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Kamera yönüne göre hareket
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Sprint sistemi
        if (Input.GetKey(KeyCode.LeftShift) && currentSprintTime > 0)
        {
            currentSpeed = sprintSpeed;
            currentSprintTime -= Time.deltaTime;

            if (currentSprintTime < 0)
                currentSprintTime = 0;
        }
        else
        {
            currentSpeed = moveSpeed;
            currentSprintTime += Time.deltaTime * coolDownSpeed;
            if (currentSprintTime > maxSprintTime)
                currentSprintTime = maxSprintTime;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        // Zýplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Yerçekimi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Slider güncelle
        if (sprintSlider != null)
        {
            sprintSlider.value = currentSprintTime;
        }
    }

    private void GroundCheckControl()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);
        // DEBUG için log
        Debug.DrawRay(GroundCheck.position, Vector3.down * groundDistance, isGrounded ? Color.green : Color.red);
    }


    private void OnDrawGizmosSelected()
    {
        if (GroundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheck.position, groundDistance);
    }

    public void LanternOpenClose()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            lantern.SetActive(!lantern.activeSelf);
        }
    }
}

