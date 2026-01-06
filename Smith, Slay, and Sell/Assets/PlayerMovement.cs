using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
	private float playerSpeed = 5.0f;

	private CharacterController controller;
	private Vector3 playerVelocity;
	private float deadzone = 0.2f;

	[Header("Input Actions")]
	// In the editor select the Input Action Reference for movement
	public InputActionReference moveAction; 
	private void Awake(){
		//This if statement attemps to get a component if it already exists,
		//otherwise it adds it. This saves editor memory and allows easier modularity.
		if (!TryGetComponent<CharacterController>(out controller)){
			controller = gameObject.AddComponent<CharacterController>();
		}
		controller.minMoveDistance = 0f;
		//Debug.Log(controller);
	}

	void Update(){

		Vector2 input = moveAction.action.ReadValue<Vector2>();
		if (input.magnitude < deadzone){
			return;
		}
		Vector3 move = new Vector3(input.x, 0, input.y);
		move = move.normalized;

		if (move != Vector3.zero){
		    transform.forward = move;
		}
		Vector3 finalMove = (move * playerSpeed) ;
		//Debug.Log(finalMove);
		controller.Move(finalMove * Time.deltaTime);
	}
}
