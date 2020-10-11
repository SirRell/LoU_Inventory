using System;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public int molotovCount,
        bandageCount,
        pistolAmmoCount,
        shotgunAmmoCount,
        grenadeCount,
        smokeGrenadeCount,
        throwable;
    public float craftSpeed = 1f;   //Time to create item

    public event Action<ItemType, int> GainItem;

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

    public void PickUpItem(Item item)
    {
        AddItem(item.type, item.amount);
        item.Pickup();
        if(item.type == ItemType.Throwable)
        {   //Change throwable icon to either a brick or bottle
            UIManager.Instance.down[0].GetComponent<InventorySlot>().SetItemImage(item.itemImage);
            //This should be in the UIManager, but that code only has the ItemType received... may refactor later.
        }
    }

    public void AddItem(ItemType type, int amount = 1)
    {
        GainItem(type, amount);
        switch (type)
        {
            case ItemType.Bandages:
                bandageCount += amount;
                break;
            case ItemType.Alcohol:
                molotovCount += amount;
                break;
            case ItemType.Ammo_Pistol:
                pistolAmmoCount += amount;
                break;
            case ItemType.Grenade:
                grenadeCount += amount;
                break;
            case ItemType.SmokeGrenade:
                smokeGrenadeCount += amount;
                break;
            case ItemType.Throwable:
                throwable += amount;
                break;
            case ItemType.Ammo_Shotgun:
                shotgunAmmoCount += amount;
                break;
            default:
                break;
        }
    }
}
