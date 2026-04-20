using UnityEngine;
using TMPro;

public class HealthText : MonoBehaviour
{
    public TMP_Text infoText;

    public void SetText(string text)
    {
        infoText.text = text;
    }
}