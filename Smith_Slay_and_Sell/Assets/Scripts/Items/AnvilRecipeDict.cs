using System.Collections.Generic;
using Unity;
using UnityEngine;

public class AnvilRecipeDict : MonoBehaviour
{
    [Header("Assign Recipes Here")]
    [SerializeField]
    private AnvilRecipeMapping[] recipeMappings;

    private Dictionary<(OreType, WorkableType), GameObject> recipeDictionary;

    void Awake()
    {
        InitializeRecipes();
    }

    private void InitializeRecipes()
    {
        recipeDictionary = new Dictionary<(OreType, WorkableType), GameObject>();

        foreach (AnvilRecipeMapping mapping in recipeMappings)
        {
            (OreType ore, WorkableType workable) recipeKey = (
                mapping.metalType,
                mapping.workableType
            );
            var recipeValue = mapping.prefab;

            if (!recipeDictionary.ContainsKey(recipeKey))
            {
                recipeDictionary.Add(recipeKey, recipeValue);
            }
            else
            {
                Debug.LogWarning($"Duplicate recipe for workable + ore type: {recipeKey}");
            }
        }
    }

    public GameObject GetRecipeForWorkableType(OreType ore, WorkableType workable)
    {
        if (recipeDictionary.TryGetValue((ore, workable), out GameObject recipe))
        {
            return recipe;
        }
        else
        {
            Debug.LogError($"No recipe found for workable type: {(workable, ore)}");
            return null;
        }
    }
}
