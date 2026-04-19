using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public TMP_Text towerText;
    public GameObject updateMenu;
    private GameObject selectedTower = null; // null if no tower selected
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Check for colliders
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            bool hitTower = false;
            foreach (Collider2D col in hits)
            {
                GameObject obj = col.gameObject;
                if (obj.layer == LayerMask.NameToLayer("Towers"))
                {
                    Debug.Log("Tower clicked: " + obj.name);
                    hitTower = true;
                    selectedTower = obj;
                }
            }

            // Check for UI items
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = mouseScreenPos;
            bool hitUpgradeUI = false;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            print("Length of results: " + results.Count);
            foreach (RaycastResult result in results)
            {
                Debug.Log("UI clicked: " + result.gameObject.name);
                GameObject obj = result.gameObject;
                if (obj.CompareTag("UpgradeUI"))
                {
                    Debug.Log("Clicked UI with tag UpgradeUI: " + obj.name);
                    hitUpgradeUI = true;
                }
            }

            if(!hitTower)
            {
                if(!hitUpgradeUI)
                {
                    selectedTower = null;
                    updateMenu.SetActive(false);
                }
            } 
            else
            {
                updateMenu.SetActive(true);
                towerText.text = selectedTower.name;
            }

            print("Tower Selected: " + selectedTower);
        }
    }

    public void UpgradeTower()
    {
        if(selectedTower != null)
        {
            // FIXME: Call the upgrade to tower here
            // selectedTower.GetComponent<Tower>.UpgradeTower();
        }
    }
}
