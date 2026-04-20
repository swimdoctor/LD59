using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefab;
    public Image icon;
    public int cost = 50;

    private Camera mainCamera;
    private PlayerController player;
    private float rotation = 0f;
    private GameObject placedObject;
    public CompositeCollider2D tilemap;

    void Start()
    {
        mainCamera = Camera.main;
		player = FindFirstObjectByType<PlayerController>();
		tilemap = FindFirstObjectByType<CompositeCollider2D>();

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
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (player == null || player.money < cost)
            return;
        player.ChangeMoney(-cost);
        rotation = 0f;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0f;
        placedObject = Instantiate(prefab, worldPos, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (placedObject != null)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0f;
			worldPos.x = Mathf.Round(worldPos.x * 2) / 2;
			worldPos.y = Mathf.Round(worldPos.y * 2) / 2;
			placedObject.transform.position = worldPos;

			if(CheckValidPosition(worldPos)) placedObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            else placedObject.GetComponent<SpriteRenderer>().color = new Color(1, .5f, .5f, 0.5f);
		}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
		if(!CheckValidPosition(placedObject.transform.position))
        {
            Destroy(placedObject);
            player.ChangeMoney(cost);
        }
		placedObject = null;
    }

    public bool CheckValidPosition(Vector3 pos)
    {
        if(tilemap.OverlapPoint(pos)) return false;
        
        foreach(Tower t in Tower.towers)
        {
            if(t == placedObject.GetComponent<Tower>()) continue;
            if(Vector3.Distance(t.transform.position, pos) < .9) return false;
        }

		return true;
    }
}