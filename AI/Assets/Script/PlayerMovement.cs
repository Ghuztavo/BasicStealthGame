using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References:
    public Transform orientation;
    Rigidbody rb;
    [SerializeField] Transform feet;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveSpeedWalking;
    [SerializeField] private float moveSpeedRunning;
    [SerializeField] private float moveSpeedCrouching;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundDrag;
    [SerializeField] private float rotateSpeed;
    public bool isMoving = false;
    public bool isRunning = false;
    public bool isCrouching = false;

    float horizontalInput;
    float verticalInput;
    float rotationInput;
    float yRotation;
    Vector3 moveDirection;
    bool isGrounded;

    public GameObject player;
    Animator animator;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (player != null)
        {
            animator = player.GetComponent<Animator>();
        }
    }

    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotationInput = Input.GetAxis("Mouse X");

        

        // rotate character 
        yRotation += rotationInput * Time.deltaTime * rotateSpeed;
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        // Handle movement speeds
        if (verticalInput > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            isCrouching = false;
            moveSpeed = moveSpeedRunning;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            isRunning = false;
            if (verticalInput > 0)
            {
                moveSpeed = moveSpeedCrouching;
            }
        }
        else
        {
            isRunning = false;
            isCrouching = false;
            moveSpeed = moveSpeedWalking;
        }

        //Process jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isCrouching = false;
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        // Check Rigidbody Velocity Instead of Only Input
        isMoving = rb.linearVelocity.magnitude > 0.1f;

        // Only set Running if player is actually moving
        if (!isMoving) isRunning = false;

        //Animation
        animator.SetFloat("Walk", verticalInput);
        animator.SetFloat("Strafe", horizontalInput);
        animator.SetBool("Running", isRunning);
        animator.SetBool("Crouching", isCrouching);
        animator.SetBool("IsMoving", isMoving);

        if (rb.linearVelocity.y > 0.1 && !isGrounded)
        {
            animator.SetBool("Jumping", true);
            animator.SetBool("Falling", false);
        }
        else if (rb.linearVelocity.y < -0.1)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
        }
    }

    private void FixedUpdate()
    {
        //Physics based movement
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);


        //Speed limit
        Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitVelocity = flatVelocity.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitVelocity.x, rb.linearVelocity.y, limitVelocity.z);
        }

        CheckGrounded();
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(feet.transform.position, Vector3.down, out hit, 0.1f))
        {
            isGrounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
        }
    }

}
