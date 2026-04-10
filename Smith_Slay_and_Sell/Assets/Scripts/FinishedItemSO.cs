using UnityEngine;

[CreateAssetMenu(fileName = "FinishedItemSO", menuName = "ScriptableObjects/FinishedItemSO")]
public class FinishedItemSO : ScriptableObject
{
    public string requestName;
    public GameObject finishedObject;
    public Sprite sprite;
}
