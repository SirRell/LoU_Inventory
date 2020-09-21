using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    UIManager Instance;
    [SerializeField]
    RectTransform selector;
    [SerializeField]
    RectTransform selection;

    [Header("Inventory Slots")]
    public GameObject[] left;
    public GameObject[] up, right, down;
    Vector2 inventoryDirection;

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
        UpdateUI();
    }

    void UpdateUI()
    {
        selector.position = selection.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory_Horizontal") || Input.GetButtonDown("Inventory_Vertical"))
        {
            inventoryDirection = new Vector2(Input.GetAxisRaw("Inventory_Horizontal"), Input.GetAxisRaw("Inventory_Vertical"));

            if(inventoryDirection == Vector2.left)
            {
                print("Left arrow pressed");
            }
            else if(inventoryDirection == Vector2.up)
            {
                print("Up arrow pressed");
            }
            else if (inventoryDirection == Vector2.right)
            {
                print("Right arrow pressed");
            }
            else if (inventoryDirection == Vector2.down)
            {
                print("Down arrow pressed");
            }
                print("Inventory button pushed");
        }
        else
            inventoryDirection = Vector2.zero;
        
    }
}
