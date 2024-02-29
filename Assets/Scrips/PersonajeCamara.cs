using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeCamara : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    //[SerializeField] private Animator anim;
    private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 8.0f;
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float gravityValue = -9.81f;
    public Transform camara;


    float turnVelocitySmooth;
    float turnSmoothTime = 0.1f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Verticcal");

        //move = move.normalized;

        //anim.SetFloat("SpeedX", move.x);
        //anim.SetFloat("SpeedZ", move.z);
        //anim.SetFloat("SpeedMag", move.magnitude);

        Vector3 move;
        float targetAngle;
   
        targetAngle = camara.eulerAngles.y;

        move = transform.forward * vertical + transform.right * horizontal;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocitySmooth, turnSmoothTime);

        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        controller.Move(move * Time.deltaTime * playerSpeed);


        //if (move != Vector3.zero)
        //{
        //gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -.3f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
