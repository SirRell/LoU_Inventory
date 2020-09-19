using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAction : MonoBehaviour
{

    /// <summary>
    /// Check around player to see if there are any items to pick up
    /// Display a popup above item to show what it is
    /// when item is picked up, play player animation for pickup
    /// </summary>

    public float radius = 4;
    public Vector3 offset = Vector3.zero;
    public TextMeshProUGUI interactionPopUp;
    Collider[] availableItems;
    GameObject closestItem = null;
    PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        if(closestItem != null && Input.GetKeyDown(KeyCode.X))
        {
            inventory.AddItem(closestItem.GetComponent<Item>());
        }
    }

    void FixedUpdate()
    {
        GetItemsInRadius();
        if (closestItem != null)
        {
            interactionPopUp.text = "Press X to pick up " + closestItem.GetComponent<Item>().itemName;
            interactionPopUp.enabled = true;
        }
        else
            interactionPopUp.enabled = false;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset, radius);
    }

    void GetItemsInRadius()
    {
        closestItem = null;
        availableItems = Physics.OverlapSphere(transform.position, radius);

        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < availableItems.Length; i++)
        {
            if (availableItems[i].gameObject.GetComponent<Item>() != null)
            {
                float distance = (transform.position - availableItems[i].transform.position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestItem = availableItems[i].gameObject;
                }
            }
        }
    }
}
