using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int bottleCount;
    public int bandageCount;
    public TextMeshProUGUI tempInventoryText;

    private void Start()
    {
        UpdateUI();
    }

    public void AddItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.Bandages:
                bandageCount += item.amount;
                break;
            case ItemType.Alcohol:
                bottleCount += item.amount;
                break;
            default:
                break;
        }
        item.Pickup();
        UpdateUI();
    }

    void UpdateUI()
    {
        tempInventoryText.text = "Bandages: " + bandageCount + "\n Alcohol: " + bottleCount;
    }
}
