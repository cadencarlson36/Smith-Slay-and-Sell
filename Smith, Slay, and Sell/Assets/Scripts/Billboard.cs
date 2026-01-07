using UnityEngine;
// This class is intended to be used with sprites to have them always face the camera.
// TODO: Add axis constraints

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
