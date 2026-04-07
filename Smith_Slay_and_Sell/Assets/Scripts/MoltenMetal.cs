using UnityEngine;

public class MoltenMetal : MonoBehaviour
{
    private MaterialPropertyBlock _propBlock;
    private Renderer[] _layerRenderers;

    [SerializeField]
    private GameObject[] layers;

    // URP Property IDs, apparently this messes up SRP ? idk
    private static readonly int ColorID = Shader.PropertyToID("_BaseColor");
    private static readonly int EmissionID = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _layerRenderers = new Renderer[layers.Length];

        for (int i = 0; i < layers.Length; i++)
        {
            if (layers[i] != null)
            {
                _layerRenderers[i] = layers[i].GetComponent<Renderer>();
                layers[i].SetActive(false);
            }
        }
    }

    public void SetLayer(int index, Color color, float glow, bool active)
    {
        if (index < 0 || index >= layers.Length)
        {
            Debug.LogWarning($"Layer index {index} is out of bounds for {gameObject.name}");
            return;
        }

        GameObject layerObj = layers[index];
        Renderer renderer = _layerRenderers[index];

        layerObj.SetActive(active);

        if (active && renderer != null)
        {
            renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor(ColorID, color);
            Color finalEmission = color * glow;
            _propBlock.SetColor(EmissionID, finalEmission);

            renderer.SetPropertyBlock(_propBlock);
        }
    }

    public void ClearAll()
    {
        foreach (var layer in layers)
        {
            if (layer != null)
                layer.SetActive(false);
        }
    }
}
