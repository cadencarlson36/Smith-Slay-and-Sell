using UnityEngine;
// This class is intended to be used with sprites to have them always face the camera.
// TODO: Add axis constraints

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        //Sets the billboard/sprite to always face directly towards the camera regardless of position.
        //LateUpdate to ensure transformation matches camera motion and avoid jitter.
        transform.forward = Camera.main.transform.forward;
    }
}
