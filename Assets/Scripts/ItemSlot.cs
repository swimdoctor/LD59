using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefab;
    public Image icon;

    private GameObject dragIcon;
    private Camera mainCamera;
    private PlayerController player;
    private float rotation = 0f;
    private GameObject placedObject;

    void Start()
    {
        mainCamera = Camera.main;
        player = FindFirstObjectByType<PlayerController>();

        if (prefab != null)
        {
            icon.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
        }
    }

    void Update()
    {
        if (placedObject == null) return;

        if (Keyboard.current.qKey.isPressed) rotation += 100f * Time.deltaTime;
        if (Keyboard.current.eKey.isPressed) rotation -= 100f * Time.deltaTime;

        placedObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
        if (dragIcon != null)
            dragIcon.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (player == null || player.money < 50)
            return;
        player.ChangeMoney(-50);
        rotation = 0f;

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

        placedObject = null;
    }
}