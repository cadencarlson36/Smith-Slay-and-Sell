using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MoltenMetalMapping
{
    public OreType oreType;
    public Color color;

    [Range(0, 5)] // slider in the Inspector
    public float emissionIntensity;
}

public class MoltenMetalDict : MonoBehaviour
{
    [SerializeField]
    private MoltenMetalMapping[] visualMappings;

    private Dictionary<OreType, MoltenMetalMapping> _metalDict;

    void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        _metalDict = new Dictionary<OreType, MoltenMetalMapping>();
        foreach (var mapping in visualMappings)
        {
            if (!_metalDict.ContainsKey(mapping.oreType))
            {
                _metalDict.Add(mapping.oreType, mapping);
            }
            else
            {
                Debug.LogWarning($"Duplicate OreType found in MoltenMetalDict: {mapping.oreType}");
            }
        }
    }

    public bool TryGetVisuals(OreType type, out Color color, out float intensity)
    {
        if (_metalDict == null)
            InitializeDictionary();

        if (_metalDict.TryGetValue(type, out MoltenMetalMapping mapping))
        {
            color = mapping.color;
            intensity = mapping.emissionIntensity;
            return true;
        }
        // Default values
        color = Color.gray;
        intensity = 0f;
        return false;
    }
}
