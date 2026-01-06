using UnityEngine; 
using UnityEngine.InputSystem; 

public class Interact : MonoBehaviour { 
	private CharacterController controller; 
	[Header("Input Actions")] 
		public InputActionReference interactAction; 
	[Header("Debug Options")] 
		public bool showInteractSphere = false; 

	private Transform interactSphereTransform; 

	private void Awake() { 
		if (!TryGetComponent<CharacterController>(out controller)) 
			controller = gameObject.AddComponent<CharacterController>(); 

		interactSphereTransform = transform.Find("InteractSphere"); 
		if (!interactSphereTransform) { 
			var interactSphereObj = new GameObject("InteractSphere"); 
			interactSphereObj.transform.SetParent(transform); 
			interactSphereObj.transform.localPosition = new Vector3(0, 0, 1); 
			interactSphereObj.AddComponent<SphereCollider>().isTrigger = true; 
			interactSphereTransform = interactSphereObj.transform; 
			var meshFilter = interactSphereObj.GetComponent<MeshFilter>(); 
			var meshRenderer = interactSphereObj.GetComponent<MeshRenderer>(); 
			meshFilter = interactSphereObj.AddComponent<MeshFilter>(); 
			meshFilter.mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx"); 
			meshRenderer = interactSphereObj.AddComponent<MeshRenderer>(); 
			meshRenderer.material.color = Color.green; 
		} 
	} 

	private void OnValidate() { 
		if (!interactSphereTransform) return; 
		var meshRenderer = interactSphereTransform.GetComponent<MeshRenderer>();
		if (meshRenderer){
			meshRenderer.enabled = showInteractSphere;
		}

	} 
	void Update() { 
		// Not best practice to subscribe every frame, put this in Start or Awake for real code! 
		interactAction.action.started += ctx => 
			Debug.Log("Interact started"); 
		interactAction.action.performed += ctx => 
			Debug.Log("Interact held"); 
		interactAction.action.canceled += ctx => 
			Debug.Log("Interact canceled"); 
	} 
}
