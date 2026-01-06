using UnityEngine;
using UnityEngine.InputSystem;

public class interact : MonoBehaviour
{

	private CharacterController controller;

	[Header("Input Actions")]

	public InputActionReference interactAction; 

	private void Awake(){
		//This if statement attemps to get a component if it already exists,
		//otherwise it adds it. This saves editor memory and allows easier modularity.
		if (!TryGetComponent<CharacterController>(out controller)){
			controller = gameObject.AddComponent<CharacterController>();
		}
		//Debug.Log(controller);
	}

	void Update(){
		interactAction.action.started += 
			context => {
				Debug.Log("Player interact");
			};
		interactAction.action.performed += 
			context => {
				Debug.Log("Player interact held");
			};
		interactAction.action.canceled += 
			context => {
				Debug.Log("Player interact canceled");
			};
	}
}
