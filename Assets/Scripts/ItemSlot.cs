using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public GameObject prefab;
    public Image icon;

    void Start()
    {
        if (prefab != null)
        {
            // get sprite from prefab
            icon.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}