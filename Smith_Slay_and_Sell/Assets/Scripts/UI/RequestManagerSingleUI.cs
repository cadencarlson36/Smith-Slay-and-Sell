using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RequestManagerSingleUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI requestNameText;

    [SerializeField]
    private Image requestIcon;

    [SerializeField]
    private TextMeshProUGUI timeRemaning;

    public void SetRequest(Request request)
    {
        requestNameText.text = request.finishedItemSO.requestName;
        requestIcon.sprite = request.finishedItemSO.sprite;
        timeRemaning.text = request.timeLeft.ToString("F0");
    }
}
