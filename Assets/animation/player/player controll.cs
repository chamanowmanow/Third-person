using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroll : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController characterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationspeed = 10f;

    private Vector3 playerVelocity;
    [SerializeField] private float gravityValue = -13f;

    public bool groundedPlayer;
    [SerializeField] private float jumpHeight = 2.5f;

    public Animator animator;

    public static playercontroll instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CheckWinner.instance.isWinner)
        {
            case true:
                animator.SetBool("Victory", CheckWinner.instance.isWinner);
                break;
           case false:
                Movement();
                break;
        }
        
    }

    void Movement()
    {

        groundedPlayer = characterController.isGrounded;
        if(characterController.isGrounded && playerVelocity.y < -2f)
        {
            playerVelocity.y = -1f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0)
            * new Vector3 (horizontalInput, 0, verticalInput);
        Vector3 movementDirecion = movementInput.normalized;

        characterController.Move(movementDirecion * playerSpeed * Time.deltaTime);

        

        if(movementDirecion != Vector3.zero) 
        {
            Quaternion desireRotation = Quaternion.LookRotation(movementDirecion, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desireRotation, rotationspeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && groundedPlayer) 
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
            animator.SetTrigger("jumping");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("speed", Mathf.Abs(movementDirecion.x) + Mathf.Abs(movementDirecion.z));
        animator.SetBool("Ground", characterController.isGrounded);
    }
}
