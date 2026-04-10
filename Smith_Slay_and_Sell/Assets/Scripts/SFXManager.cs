using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] popSounds;

    public void PopSound(Vector3 position)
    {
        SFX.Play(popSounds[Random.Range(0, popSounds.Length)], position, Random.Range(1f, 1.5f));
    }
}
