using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    UIManager Instance;
    public RectTransform selector;

    [Header("Inventory Slots")]
    public GameObject[] left; //Heavy guns
    public GameObject[] up, right, down; //Medkit & Granade, Handguns, Bottle/Brick & Molotov & Smoke
    Vector2 inventoryDirection;
    CanvasGroup inventoryUI;
    bool activeInventory = false;
    float activeTimer = 5f;
    public float fadeSpeed = 1.5f;

    InventorySlot slot;

    void Awake()
    {
        //Make this the only UI Manager, and easily accessible
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryUI = GameObject.Find("Inventory").GetComponent<CanvasGroup>();
        inventoryUI.alpha = 0;
        PlayerInventory.Instance.GainItem += UpdateUI;
    }

    void UpdateUI(Item item)
    {
        switch (item.type)
        {
            case ItemType.Bandages:
                up[0].GetComponent<InventorySlot>().UpdateAmount(true, item.amount);
                break;
            case ItemType.Alcohol:
                down[1].GetComponent<InventorySlot>().UpdateAmount(true, item.amount);
                break;
            case ItemType.Ammo_Pistol:
                right[0].GetComponent<InventorySlot>().UpdateAmount(true, item.amount);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        #region Display inventory & Inventory selection
        if (Input.GetButtonDown("Inventory_Horizontal") || Input.GetButtonDown("Inventory_Vertical"))
        {
            if (!activeInventory)
            {
                activeInventory = true;
                StartCoroutine(ShowInventory());
            }
            activeTimer = 5f;
            bool moved = false;

            inventoryDirection = new Vector2(Input.GetAxisRaw("Inventory_Horizontal"), Input.GetAxisRaw("Inventory_Vertical"));

            if(inventoryDirection == Vector2.left)
            {
                for (int i = 0; i < left.Length; i++)
                {
                    if (selector.position == left[i].transform.position)
                    {
                        if (++i < left.Length)
                        {
                            selector.position = left[i].transform.position;
                        }
                        else
                        {
                            selector.position = left[0].transform.position;
                        }
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                    selector.position = left[0].transform.position;

            }
            else if(inventoryDirection == Vector2.up)
            {
                for (int i = 0; i < up.Length; i++)
                {
                    if (selector.position == up[i].transform.position)
                    {
                        if (++i < up.Length)
                        {
                            selector.position = up[i].transform.position;
                        }
                        else
                        {
                            selector.position = up[0].transform.position;
                        }
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                    selector.position = up[0].transform.position;
            }
            else if (inventoryDirection == Vector2.right)
            {
                for (int i = 0; i < right.Length; i++)
                {
                    if (selector.position == right[i].transform.position)
                    {
                        if (++i < right.Length)
                        {
                            selector.position = right[i].transform.position;
                        }
                        else
                        {
                            selector.position = right[0].transform.position;
                        }
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                    selector.position = right[0].transform.position;
            }
            else if (inventoryDirection == Vector2.down)
            {
                for (int i = 0; i < down.Length; i++)
                {
                    if (selector.position == down[i].transform.position)
                    {
                        if (++i < down.Length)
                        {
                            selector.position = down[i].transform.position;
                        }
                        else
                        {
                            selector.position = down[0].transform.position;
                        }
                        moved = true;
                        break;
                    }
                }
                if (!moved)
                    selector.position = down[0].transform.position;
            }
        }

        if (activeInventory)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0f)
            {
                StartCoroutine(HideInventory());
            }
        }
        #endregion
    }

    IEnumerator ShowInventory()
    {
        while(inventoryUI.alpha < 1)
        {
            inventoryUI.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        yield return null;
    }
    IEnumerator HideInventory()
    {
        while (inventoryUI.alpha > 0)
        {
            inventoryUI.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        activeInventory = false;
        yield return null;

    }
}
