using UnityEngine;
using System.Collections.Generic;


// TODO filter for pickupable/interactables probably using tags.
public class InteractSphere : MonoBehaviour
{
    private List<GameObject> objectsInRange = new List<GameObject>();
    void Update()
    {
        foreach (var obj in objectsInRange)
        {
            //Debug.Log($"In range: {obj.name}");
        }
    }
    //TODO make this actually return the nearest and not just the first in the list
    public GameObject GetNearestInRange()
    {
        return (objectsInRange.Count > 0) ? objectsInRange[0] : null;
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"Enter: {other.name}");
        objectsInRange.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log($"Exit: {other.name}");

        objectsInRange.Remove(other.gameObject);
    }
}
