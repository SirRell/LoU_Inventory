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
    CanvasGroup inventoryUI;
    bool activeInventory = false;
    float activeTimer = 5f;
    public float fadeSpeed = 1.5f;
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
        inventoryUI = GameObject.Find("Inventory").GetComponent<CanvasGroup>();
        inventoryUI.alpha = 0;
    }

    void UpdateUI()
    {
        selector.position = selection.position;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory_Horizontal") || Input.GetButtonDown("Inventory_Vertical"))
        {
            if (!activeInventory)
            {
                activeInventory = true;
                StartCoroutine(ShowInventory());
            }
            activeTimer = 5f;

            inventoryDirection = new Vector2(Input.GetAxisRaw("Inventory_Horizontal"), Input.GetAxisRaw("Inventory_Vertical"));

            if(inventoryDirection == Vector2.left)
            {
                if(selector.position == left[0].transform.position)
                {
                    selector.position = left[1].transform.position;
                }
                else
                {
                    selector.position = left[0].transform.position;
                }
            }
            else if(inventoryDirection == Vector2.up)
            {
                if (selector.position == up[0].transform.position)
                {
                    selector.position = up[1].transform.position;
                }
                else
                {
                    selector.position = up[0].transform.position;
                }
            }
            else if (inventoryDirection == Vector2.right)
            {
                if (selector.position == right[0].transform.position)
                {
                    selector.position = right[1].transform.position;
                }
                else
                {
                    selector.position = right[0].transform.position;
                }
            }
            else if (inventoryDirection == Vector2.down)
            {
                if (selector.position == down[0].transform.position)
                {
                    selector.position = down[1].transform.position;
                }
                else if(selector.position == down[1].transform.position)
                {
                    selector.position = down[2].transform.position;
                }
                else
                {
                    selector.position = down[0].transform.position;
                }
            }
        }
        else
            inventoryDirection = Vector2.zero;

        if (activeInventory)
        {
            activeTimer -= Time.deltaTime;
            if (activeTimer <= 0f)
            {
                StartCoroutine(HideInventory());
            }
        }
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
