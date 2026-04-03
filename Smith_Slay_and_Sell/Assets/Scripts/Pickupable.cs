using UnityEngine;

public class Pickupable : MonoBehaviour, IInteract
{
    //Interface function for player interact
    public void Interact(GameObject player)
    {
        PlayerInteract playerInteract = player.GetComponent<PlayerInteract>();
        if (playerInteract != null)
        {
            playerInteract.heldRb = this.GetComponent<Rigidbody>();
            playerInteract.heldObject = this.transform;//TODO don't name transform as object; jebaited
        }
        else
        {
            //TODO make this make more sense
            Debug.Log("Unreachable code reached");
        }
    }
}
