using UnityEngine;

public class RequestManager : MonoBehaviour
{
    [SerializeField]
    private RequestService requestService;

    [SerializeField]
    private float spawnInterval = 30f;

    [SerializeField]
    private bool debuglog = false;

    private float spawnTimer = 0f;

    void Update()
    {
        requestService.UpdateTimeLeft();
        spawnTimer += Time.deltaTime;
        if (ShouldSpawn())
        {
            SpawnRequest();
            spawnTimer = 0f;
            if (debuglog)
            {
                Debug.Log("Attempting to spawn a new request.");

                Debug.Log("Current List of Requests: ");
                foreach (var x in requestService.GetRequests())
                {
                    Debug.Log(x.finishedType);
                    Debug.Log(x.metalType);
                    Debug.Log(x.timeLeft);
                }
            }
        }
    }

    public bool SubmitFinishedItem(FinishedType finishedType)
    {
        if (debuglog)
        {
            Debug.Log("Attempting to submit finished item.");
        }
        return requestService.SubmitFinishedItem(finishedType);
    }

    private void SpawnRequest()
    {
        //Have RandomRequest from a scene manager instead of enum
        requestService.CreateAndAddRandomRequest();
    }

    private bool ShouldSpawn()
    {
        return spawnTimer >= spawnInterval;
    }
}
