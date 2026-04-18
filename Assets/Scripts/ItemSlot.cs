using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefab;
    public Image icon;

    private GameObject dragIcon;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (prefab != null)
        {
            icon.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

private GameObject placedObject;

public void OnBeginDrag(PointerEventData eventData)
{
    // create prefab
    Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
    worldPos.z = 0f;
    placedObject = Instantiate(prefab, worldPos, Quaternion.identity);

    dragIcon = new GameObject("DragIcon");
    dragIcon.transform.SetParent(transform.root, false);
    Image img = dragIcon.AddComponent<Image>();
    img.sprite = icon.sprite;
    img.raycastTarget = false;
    RectTransform rt = dragIcon.GetComponent<RectTransform>();
    rt.anchorMin = new Vector2(0.5f, 0.5f);
    rt.anchorMax = new Vector2(0.5f, 0.5f);
    rt.pivot = new Vector2(0.5f, 0.5f);
    rt.sizeDelta = new Vector2(64, 64);
    rt.position = eventData.position;
}

public void OnDrag(PointerEventData eventData)
{
    // move both icon and prefab
    if (dragIcon != null)
        dragIcon.transform.position = eventData.position;

    if (placedObject != null)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0f;
        placedObject.transform.position = worldPos;
    }
}

public void OnEndDrag(PointerEventData eventData)
{
    if (dragIcon != null)
        Destroy(dragIcon);
}
}