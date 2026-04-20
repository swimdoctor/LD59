using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    public TMP_Text infoText;

    public void SetText(string text)
    {
        infoText.text = text;
    }
}