using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    public Animator animatorPublico;
    [SerializeField] public Animator animatorPrivadoSerializado;
    private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] public Transform camara;

    float turnVelocitySmooth;
    public float turnSmoothTime = 0.1f;

    private void Start()
    {
        
        controller = gameObject.GetComponent<CharacterController>();
        animatorPrivadoSerializado = gameObject.GetComponent <Animator>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;

        animatorPublico.SetFloat("SpeedX", move.x);
        animatorPublico.SetFloat("SpeedZ", move.z);
        animatorPublico.SetFloat("SpeedMag", move.magnitude);

        if (move.magnitude >= 0.1)
        {
            float targetAngle;
            Vector3 moveDir;

            targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + camara.eulerAngles.y;

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocitySmooth, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f,angle, 0f);

            controller.Move(moveDir.normalized * Time.deltaTime * playerSpeed);
        }
        
       

        //if (move != Vector3.zero)
        //{
          //  gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
