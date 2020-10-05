using System;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int bottleCount,
        bandageCount,
        pistolAmmoCount;

    public event Action<Item> GainItem;

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        
    }

    public void AddItem(Item item)
    {
        GainItem(item);
        switch (item.type)
        {
            case ItemType.Bandages:
                bandageCount += item.amount;
                break;
            case ItemType.Alcohol:
                bottleCount += item.amount;
                break;
            case ItemType.Ammo_Pistol:
                pistolAmmoCount += item.amount;
                break;
            default:
                break;
        }
        item.Pickup();
    }
}
