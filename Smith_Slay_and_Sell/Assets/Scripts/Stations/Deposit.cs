using UnityEngine;

public class Deposit : MonoBehaviour
{
    [SerializeField]
    private ScoreHandler scoreHandler;

    [SerializeField]
    private RequestManager requestManager;

    //TODO implement layer
    [Header("Item Settings")]
    [Tooltip("The layer of the item this Furnace accepts.")]
    public string validItemLayer = "processed";

    private void OnTriggerEnter(Collider other)
    {
        GameObject parentObject = other.transform.root.gameObject;
        if (parentObject.TryGetComponent(out FinishedItem finished))
        {
            if (requestManager.SubmitFinishedItem(finished))
            {
                scoreHandler.UpdateScore();
                Destroy(parentObject);
            }
            //Object not in requests/orders
            else
            {
                //launch Object
                Rigidbody rb = parentObject.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.linearVelocity = parentObject.transform.forward * 10f;
            }
        }
    }
}
