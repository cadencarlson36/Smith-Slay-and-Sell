using System;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

[Serializable]
public struct Request
{
    public float timeLeft;

    // This is the requested object type
    public FinishedType finishedType;
    public MetalType metalType;
}

public class RequestService : MonoBehaviour
{
    private readonly int MAX_REQUEST = 10;
    private readonly float DEFAULT_TIME = 120f; //Each request/order is 2 minutes long by default
    private List<Request> requests;

    void Awake()
    {
        requests = new List<Request>();
    }

    Request CreateAndGetRequest(float time, FinishedType finishedType, MetalType metalType)
    {
        Request newRequest = new()
        {
            timeLeft = time,
            finishedType = finishedType,
            metalType = metalType,
        };
        return newRequest;
    }

    void AddRequest(Request newRequest)
    {
        if (requests.Count < MAX_REQUEST)
        {
            requests.Add(newRequest);
        }
    }

    //Are we correctly managing memory here or is this depending on C# GC?
    public List<Request> GetRequests()
    {
        return new List<Request>(requests);
    }

    //TODO pull enums from a manager in the scene so we can ensure only items available to being made can be requested. (complicated recipes should be saved for later levels)
    public void CreateAndAddRandomRequest()
    {
        if (requests.Count < MAX_REQUEST)
        {
            //Cool hack for getting array of type enum
            FinishedType[] finishedTypes = (FinishedType[])Enum.GetValues(typeof(FinishedType));
            MetalType[] metalTypes = (MetalType[])Enum.GetValues(typeof(MetalType));
            System.Random rand = new();
            Request newRequest = new()
            {
                finishedType = finishedTypes[rand.Next(finishedTypes.Length)],
                metalType = metalTypes[rand.Next(metalTypes.Length)],
                timeLeft = DEFAULT_TIME,
            };
            AddRequest(newRequest);
        }
        else
        {
            Debug.LogWarning("Requests are full");
        }
    }

    //Return type to determine if item should be destroyed/turned in
    public bool SubmitFinishedItem(FinishedType finishedType)
    {
        for (int i = 0; i < requests.Count; i++)
        {
            if (requests[i].finishedType == finishedType)
            {
                //TODO add reward processing here?
                requests.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public void UpdateTimeLeft()
    {
        for (int i = 0; i < requests.Count; i++)
        {
            Request temp = requests[i];
            temp.timeLeft -= Time.deltaTime;
            requests[i] = temp;
        }
    }
}
