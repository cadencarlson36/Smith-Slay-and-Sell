using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    private float playerSpeed = 5.0f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private float deadzone = 0.2f;
    private float turnSmoothSpeed = 60f;

    private PlayerInput playerInput;

    private void Start()
    {
        //This if statement attemps to get a component if it already exists,
        //otherwise it adds it. This saves editor memory and allows easier modularity.
        if (!TryGetComponent<CharacterController>(out controller))
        {
            controller = gameObject.AddComponent<CharacterController>();
        }
        controller.minMoveDistance = 0f;
        playerInput = GetComponent<PlayerInput>();
        //Debug.Log(controller);
    }

    void Update()
    {
        //Read the "Move" action from the player input system.
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        //This ensures that the input vector for move is of a certain magnitude, acting like a deadzone
        //This is VERY important for controller inputs that are almost never reading true (0,0)
        if (input.magnitude < deadzone)
        {
            return;
        }

        Vector3 move = new Vector3(input.x, 0, input.y);
        move = move.normalized;

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSmoothSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogError("Zero-vector move detected (impossible)");
        }
        //Scale the final movement by speed and time
        controller.Move(move * playerSpeed * Time.deltaTime);
    }
}
