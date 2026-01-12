using UnityEngine;
using System.Collections.Generic;


// TODO filter for pickupable/interactables probably using tags.
// This script is used in the InteractSphere object created in PlayerInteract.cs
// It currently provides public functions to get objects with colliders that the sphere
// intersects with.
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
    //This function returns the first object in the list.
    //DISCLAIMER: This is NOT the nearest item in range as of yet.
    //This function will need to be updated with logic to get the closest object relative to the player.
    //TODO make this actually return the nearest and not just the first in the list
    public GameObject GetNearestInRange()
    {
        return (objectsInRange.Count > 0) ? objectsInRange[0] : null;
    }
    void OnTriggerEnter(Collider other)
    {
        //When a collider enters the sphere, we add the object to the objectsInRange list.
        objectsInRange.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {

        //When a collider exits the sphere, we remove the object from the objectsInRange list.
        objectsInRange.Remove(other.gameObject);
    }
}
