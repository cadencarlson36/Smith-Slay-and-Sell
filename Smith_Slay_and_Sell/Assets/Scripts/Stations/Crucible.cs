using System.Collections.Generic;
using UnityEngine;

public class Crucible : MonoBehaviour
{
    public enum CrucibleState
    {
        EmptyCold,
        EmptyHot,
        PartiallyFilledCold,
        PartiallyFilledHot,
        FullCold,
        FullHot,
    }

    private const int maxCapacity = 10;
    private int currentMetal = 0;
    List<OreType> currentMetalList = new List<OreType>(capacity: maxCapacity);

    [SerializeField]
    private MoltenMetalDict moltenMetalManager;

    [SerializeField]
    private MoltenMetal moltenMetal;

    [Header("Crucible Status")]
    public CrucibleState currentState = CrucibleState.EmptyCold;

    private const float maxTemperature = 100f;
    private const float minPourTemp = 15f;
    public float temperature = 0f;

    [Header("Processing Settings")]
    public float processingTime = 3.0f;

    [SerializeField]
    private float coolingRate = 1f;

    void Update()
    {
        if (temperature > 0)
        {
            temperature -= coolingRate * Time.deltaTime;
            temperature = Mathf.Max(temperature, 0); // clamp to 0
        }

        bool isHot = temperature > minPourTemp;
        int currentFill = currentMetalList.Count;

        if (currentFill == 0)
        {
            currentState = isHot ? CrucibleState.EmptyHot : CrucibleState.EmptyCold;
        }
        else if (currentFill >= maxCapacity)
        {
            currentState = isHot ? CrucibleState.FullHot : CrucibleState.FullCold;
        }
        else
        {
            currentState = isHot
                ? CrucibleState.PartiallyFilledHot
                : CrucibleState.PartiallyFilledCold;
        }

        RefreshVisuals();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState != CrucibleState.FullCold && currentState != CrucibleState.FullHot)
        {
            GameObject parentObject = other.transform.root.gameObject;

            if (parentObject.TryGetComponent(out OreItem oreItem))
            {
                AddOreToCrucible(oreItem.type);
                Destroy(parentObject);
            }
            else if (parentObject.TryGetComponent(out CoalItem coalItem))
            {
                AddCoalToCrucible();
                Destroy(parentObject);
            }
            else if (parentObject.TryGetComponent(out WorkableItem workableItem))
            {
                AddMetalToCrucible(workableItem.metalType);
                Destroy(parentObject);
            }
            else if (parentObject.TryGetComponent(out FinishedItem finishedItem))
            {
                AddMetalToCrucible(finishedItem.metalType);
                Destroy(parentObject);
            }
        }
    }

    public void AddHeat(float amountToAdd)
    {
        temperature += amountToAdd;
        temperature = Mathf.Min(temperature, maxTemperature); // clamp to max
    }

    public OreType PourMetal()
    {
        if (temperature > minPourTemp && currentMetalList.Count > 0)
        {
            OreType pouredMetal = currentMetalList[0];
            currentMetalList.RemoveAt(0);
            return pouredMetal;
        }
        return OreType.None;
    }

    private void AddCoalToCrucible()
    {
        Debug.Log("TODO: Add steel logic if coal added to iron layer");
    }

    private void AddMetalToCrucible(OreType itemType)
    {
        // Placeholder for future logic
    }

    private void AddOreToCrucible(OreType itemType)
    {
        if (currentMetalList.Count + 2 <= maxCapacity)
        {
            currentMetalList.Add(itemType);
            currentMetalList.Add(OreType.Slag);
            SortMetals();
        }
        else
        {
            Debug.Log("Crucible is full");
        }
    }

    private void SortMetals()
    {
        if (currentMetalList.Count < 1)
            return;

        List<OreType> metals = new List<OreType>();
        int slagCount = 0;

        foreach (OreType type in currentMetalList)
        {
            if (type == OreType.Slag)
                slagCount++;
            else
                metals.Add(type);
        }

        currentMetalList.Clear();
        currentMetalList.AddRange(metals);
        for (int i = 0; i < slagCount; i++)
            currentMetalList.Add(OreType.Slag);

        currentMetal = metals.Count;
    }

    private void RefreshVisuals()
    {
        if (moltenMetal == null || moltenMetalManager == null)
            return;

        moltenMetal.ClearAll();

        float heatFactor = Mathf.InverseLerp(minPourTemp, maxTemperature, temperature);

        for (int i = 0; i < currentMetalList.Count; i++)
        {
            OreType currentType = currentMetalList[i];

            if (
                moltenMetalManager.TryGetVisuals(
                    currentType,
                    out Color metalColor,
                    out float baseIntensity
                )
            )
            {
                float dynamicIntensity = baseIntensity * heatFactor;
                moltenMetal.SetLayer(i, metalColor, dynamicIntensity, true);
            }
            else
            {
                moltenMetal.SetLayer(i, Color.gray, 0f, true);
            }
        }
    }
}
