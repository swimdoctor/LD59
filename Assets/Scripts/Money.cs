using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    public TMP_Text infoText;

    public void SetText(string text)
    {
        infoText.text = text;
    }
}