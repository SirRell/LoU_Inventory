using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Bandages,
    Alcohol,
    Ammo_Pistol
}

public class Item : MonoBehaviour
{
    public int amount = 1;
    public ItemType type;
    public string itemName;

    public void Pickup()
    {
        Destroy(gameObject);
    }
}
