using UnityEngine;

public class Station : MonoBehaviour
{
    public enum StationState
    {
        Idle,
        Processing,
        Finished
    }
    [SerializeField] private GameObject fireSprite;
    [Header("Station Status")]
    public StationState currentState = StationState.Idle;

    [Header("Processing Settings")]
    public float processingTime = 3.0f;

    private float currentTimer = 0f;


    //Will need to switch from a tag system eventually since only
    //one tag can be set at a time in Unity, but we might have multiple processing
    //types that should only work on some entities
    [Header("Item Settings")]
    [Tooltip("The tag of the item this station accepts.")]
    public string validItemTag = "Processable";
    [Tooltip("The prefab to spawn after processing completes")]
    public GameObject outputPrefab;
    [Tooltip("Where the finished item should apper.")]
    public Transform spawnPoint;


    private GameObject itemBeingProcessed;

    void Update()
    {
        if (fireSprite != null)
        {
            fireSprite.SetActive(currentState == StationState.Processing);
        }
        if (currentState == StationState.Processing)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= processingTime)
            {
                CompleteProcessing();
                currentTimer = 0f;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == StationState.Idle && other.CompareTag(validItemTag))
        {
            StartProcessing(other.gameObject);
        }
    }
    private void StartProcessing(GameObject inputItem)
    {
        Debug.Log($"Station started processing: {inputItem.name}");
        currentState = StationState.Processing;
        currentTimer = 0f;

        itemBeingProcessed = inputItem;
        itemBeingProcessed.SetActive(false);
    }

    private void CompleteProcessing()
    {
        Debug.Log("Station finished processing.");
        currentState = StationState.Finished;

        if (itemBeingProcessed != null)
        {
            Destroy(itemBeingProcessed);
        }
        if (outputPrefab != null)
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up;
            GameObject spawnedObject = Instantiate(outputPrefab, spawnPos, Quaternion.identity);
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float upForce = 5f;
                float sideRange = 2f;

                Vector3 force = new Vector3(
                    Random.Range(-sideRange, sideRange),
                    upForce,
                    Random.Range(-sideRange, sideRange)
                );

                rb.AddForce(force, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("No output prefab assigned to station!");
        }

        currentState = StationState.Idle;
    }
}
