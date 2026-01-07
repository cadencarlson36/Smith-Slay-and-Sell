using UnityEngine;


public class InteractSphere : MonoBehaviour
{
    private List<GameObject> objectsInRange;
    void Update()
    {
        foreach (var obj in objectsInRange)
        {
            Debug.Log($"In range: {obj.name}");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        objectsInRange.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        objectsInRange.Remove(other.gameObject);
    }
}
