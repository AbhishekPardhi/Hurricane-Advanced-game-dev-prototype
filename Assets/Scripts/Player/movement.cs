using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterController controller;
    public float speed = 9f;
    public float speedslow = 4f;
    public float turnTime = 0.1f;
    public float turnSmoothVelocity;
    public Transform cam;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool walk = false;
    public bool running = false;
    public Transform groundCheck;
    Vector3 velocity;
    public  bool jump = false;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            walk = true;
        }
        else walk = false;
        {
            float targetAngle = /*Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+*/cam.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 movedir = Quaternion.Euler(0f, targetAngle, 0f) * direction;
            if (walk) controller.Move(movedir * speedslow * Time.deltaTime);
            else controller.Move(movedir * speed * Time.deltaTime);

            if (movedir.magnitude > 0)
            {
                running = true;
            }
            else running = false;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jump = true;
        }
       

      
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
