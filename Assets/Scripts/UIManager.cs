using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public RectTransform selector;

    [Header("Inventory Slots")]
    public GameObject[] left; //Heavy guns
    public GameObject[] up, right, down; //Medkit & Granade, Handguns, Bottle/Brick & Molotov & Smoke
    Vector2 inventoryDirection;
    CanvasGroup inventoryUI;
    bool activeInventory = false;
    float activeTimer = 5f; //How long the inventory UI is shown
    public float fadeSpeed = 1.5f;
    InventorySlot selection;

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

    void UpdateUI(ItemType item, int qty)
    {
        switch (item)
        {
            case ItemType.Bandages:
                up[0].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Alcohol:
                down[1].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Ammo_Pistol:
                right[0].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Ammo_Shotgun:
                left[0].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Ammo_Rifle:
                left[1].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Ammo_Revolver:
                right[1].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Grenade:
                up[1].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.Throwable:
                down[0].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                down[0].GetComponent<InventorySlot>().itemType = item;
                break;
            case ItemType.Molotov:
                down[1].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            case ItemType.SmokeGrenade:
                down[2].GetComponent<InventorySlot>().UpdateAmount(true, qty);
                break;
            default:
                break;
        }

    }

    void Update()
    {
        #region Display inventory & Inventory selection
        #region Display UI Selections
        if (Input.GetButtonDown("Inventory_Horizontal") || Input.GetButtonDown("Inventory_Vertical"))
        {
            //Set the direction pressed
            inventoryDirection = new Vector2(Input.GetAxisRaw("Inventory_Horizontal"), Input.GetAxisRaw("Inventory_Vertical"));

            if (!activeInventory) //If inventory is not already showing
            {
                activeInventory = true;
                StartCoroutine(ShowInventory());
            }
            activeTimer = 5f;   //Reset the timer to 5 seconds
            bool moved = false;//Selector came from a different direction

            GameObject[] array = InventoryDirection();  //Get a reference to the approperiate direction array

            for (int i = 0; i < array.Length; i++)  //Loop through all inventory items in that specific array
            {
                if (selector.position == array[i].transform.position)   //Find the item that is already selected, if the selection is already in this group
                {
                    if (++i == array.Length)    //If the selected item is at the end, meaning there is not a next item to select
                    {
                        i = 0;  //Select the first item in the array
                    }
                    selector.position = array[i].transform.position;    //Move the selector
                    selection = array[i].GetComponent<InventorySlot>(); //Get a reference to the slot script
                    moved = true;   //Set moved to true because the selector has moved to a new item in the array
                    break;  //Break out of the loop, so if there is more than 2 items, you won't select only the first and last items
                }
            }
            if (!moved) //If the selection wasn't already in this group, put it at the first item
            {
                selector.position = array[0].transform.position;
                selection = array[0].GetComponent<InventorySlot>();
            }
        }
        #endregion

        if (activeInventory) //Displaying Inventory / Searching Bag
        {
            
            if (Input.GetKeyDown(KeyCode.Return)) //Key has been pressed - start/reset timers
            {

            }

            if (Input.GetKey(KeyCode.Return))
            {
                activeTimer = 5f;
                if (selection.craftable)
                { //Start crafting item
                    selection.progress.GetComponent<Image>().fillAmount += Time.deltaTime * PlayerInventory.Instance.craftSpeed;
                    if(selection.progress.GetComponent<Image>().fillAmount >= 1)
                    {
                        selection.progress.GetComponent<Image>().fillAmount = 0;
                        if(selection.itemType != ItemType.None)
                            PlayerInventory.Instance.AddItem(selection.itemType);
                        return;
                    }
                }
                else
                { //Draw Item/Weapon
                    selection.DrawItem();
                }
            }
            if (Input.GetKeyUp(KeyCode.Return))  // Draw Item / Weapon
            {
                selection.DrawItem();
                selection.progress.GetComponent<Image>().fillAmount = 0;    //Reset craft progress
            }

            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0f)
            {
                StartCoroutine(HideInventory());
            }
        }
#endregion
    }

    ref GameObject[] InventoryDirection()
    {
        if (inventoryDirection == Vector2.left)
            return ref left;
        else if (inventoryDirection == Vector2.up)
            return ref up;
        else if (inventoryDirection == Vector2.right)
            return ref right;
        else return ref down;
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
