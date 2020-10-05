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
    public int amount = 1;  //Amount to be picked up
    public ItemType type;   //Type of the item
    public string itemName; //Name of the item
    public Sprite itemImage;    //Inventory slot image

    public void Pickup()
    {
        Destroy(gameObject);
    }
}
