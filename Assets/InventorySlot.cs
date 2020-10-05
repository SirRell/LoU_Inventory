using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Sprite iconImage;
    Image icon;
    TextMeshProUGUI qtyText;
    int qty;

    void Start()
    {
        icon = GetComponentInChildren<Image>();
        SetItemImage(icon.sprite);
        qtyText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateAmount(bool add, int amount)
    {
        if (add)
            qty += amount;
        else
            qty -= amount;
        qtyText.text = qty.ToString();

        if(amount > 0)
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 255);
        else
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 60);
    }

    void SetItemImage(Sprite itemImage)
    {
        icon.sprite = itemImage;
    }

    
}
