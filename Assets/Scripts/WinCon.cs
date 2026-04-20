using UnityEngine;
using TMPro;
public class WinCon : MonoBehaviour
{
    public TMP_Text messageText;
    public string message = "You Win!";

    void OnDestroy()
    {
            messageText.gameObject.SetActive(true);
        if (messageText != null)
            messageText.text = message;
    }
}