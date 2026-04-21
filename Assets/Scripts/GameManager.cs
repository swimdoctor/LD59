using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{
    public TMP_Text towerText;
    public TMP_Text upgradeTowerButtonText;
    public GameObject updateMenu;
    private GameObject selectedTower = null; // null if no tower selected
    public PlayerController player;
    void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame) Application.Quit();

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
            foreach (RaycastResult result in results)
            {
                GameObject obj = result.gameObject;
                if (obj.CompareTag("UpgradeUI"))
                {
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
                int upgradeCost = selectedTower.GetComponent<Tower>().getUpgradeCost();
                UpdateButtonText(upgradeCost);
            }
        }
    }

    private void UpdateButtonText(int upgradeCost)
    {
        if(upgradeCost >= 0)
        {
            upgradeTowerButtonText.text = "Upgrade: $" + upgradeCost;
        } else
        {
            upgradeTowerButtonText.text = "Max Upgrade";
        }
    }

    public void UpgradeTower()
    {
        if(selectedTower != null && selectedTower.GetComponent<Tower>().getUpgradeCost() >= 0 && player.money >= selectedTower.GetComponent<Tower>().getUpgradeCost())
        {
            player.ChangeMoney(-selectedTower.GetComponent<Tower>().getUpgradeCost());
            selectedTower.GetComponent<Tower>().UpgradeTower();
            int upgradeCost = selectedTower.GetComponent<Tower>().getUpgradeCost();
            UpdateButtonText(upgradeCost);
        }
    }
}
