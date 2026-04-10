using UnityEngine;

public class RequestManagerUI : MonoBehaviour
{
    [SerializeField]
    private Transform container;

    [SerializeField]
    private Transform requestTemplate;

    [SerializeField]
    private RequestManager requestManager;

    private void Awake()
    {
        requestTemplate.gameObject.SetActive(false);
    }

    // private void Start()
    // {
    //     requestManager.OnRequestSpawned += OnRequestSpawned;
    //     requestManager.OnRequestCompleted += OnRequestCompleted;
    // }
    //
    // private void OnRequestSpawned(object sender, System.EventArgs e)
    // {
    //     UpdateVisual();
    // }
    //
    // private void OnRequestCompleted(object sender, System.EventArgs e)
    // {
    //     UpdateVisual();
    // }
    private void Update()
    {
        UpdateVisual(); //Sadly have to for time
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == requestTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach (Request request in requestManager.GetRequests())
        {
            Transform requestTransform = Instantiate(requestTemplate, container);
            requestTransform.gameObject.SetActive(true);
            requestTransform.GetComponent<RequestManagerSingleUI>().SetRequest(request);
        }
    }
}
