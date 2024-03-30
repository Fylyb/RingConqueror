using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotorSplit1 : MonoBehaviour
{
    public bool disabled = false;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;

    public PlayerHealthSplit1 playerHealth; // bude nastaveny playerHealthSplit1
    public PlayerHealthSplit2 aiHealth; // bude nastavený playerHealthSplit2
    //public float gravity = -9.8f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealthSplit1>();
        //aiHealth = GetComponent<AIhealth>();
    }

    // Update is called once per frame
    void Update()
    {
        //isGrounded = controller.isGrounded;

        if (!disabled)
        {
            //ProcessMove();
            isGrounded = controller.isGrounded;
        }
    }
    //receive the inputs for our Inputmanager.cs and apply them to our character controller
    public void ProcessMove(Vector2 vector2)
    {
        if (playerHealth.kO == false && aiHealth.kO == false)
        {
            float moveX = 0f;
            float moveZ = 0f;

            // Kontrola pohybu smìrem dopøedu a dozadu (klávesy W a S)
            if (Input.GetKey(KeyCode.W))
            {
                moveZ = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveZ = -1f;
            }

            // Kontrola pohybu smìrem doleva a doprava (klávesy A a D)
            if (Input.GetKey(KeyCode.A))
            {
                moveX = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                moveX = 1f;
            }

            Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }

        //Vector3 moveDirection = Vector3.zero;
        //moveDirection.x = input.x;
        //moveDirection.z = input.y;
        //controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        ////playerVelocity.y += gravity * Time.deltaTime;
        ////if (isGrounded && playerVelocity.y < 0)
        ////    playerVelocity.y = -2f;
        ////controller.Move(playerVelocity * Time.deltaTime);
        ////Debug.Log(playerVelocity.y);
    }
}
