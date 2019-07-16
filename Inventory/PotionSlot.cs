using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PotionSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Items> OnRightClickEvent;

    public Image Image;
    private Items item;
    [SerializeField] ItemToolTip toolTip;
    public Inventory inventory;
    private bool AtShop = false;
    private Player PlayerRef;
    [SerializeField] Text AmountText;
    public Items Item
    {
        get
        { return item; }

        set
        {
            item = value;
            if (item == null)
            {
                Image.enabled = false;
            }
            else
            {
                Image.sprite = item.ItemIcon;
                Image.enabled = true;
            }
        }
    }

    private void Update()
    {
        CheckItemStack();
    }

    public void CheckItemStack()
    {
        if (Item != null)
        {
            if (Item.StackAmount > 0)
                AmountText.text = Item.StackAmount.ToString();
            else
                AmountText.text = "";

        }
        inventory.CheckStackNum -= CheckItemStack;
    }

    private void Start()
    {
        PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        PlayerRef.PlayerCanBuy += InShop;
        PlayerRef.PlayerCantBuy += OutOfShop;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            EquippableItems equippableItem = Item as EquippableItems;
            if (!AtShop)
            {
                if (equippableItem != null && OnRightClickEvent != null)
                {
                    OnRightClickEvent(equippableItem);
                }
                else
                {
                    Item.Use(Item);
                }
            }
            else if (AtShop && item.Equipped == false)
            {
                SkillManager.Instance.Money += item.ItemPrice;
                inventory.RemoveItem(item);
                UIManager.instance.UpdateMoney();
            }
        }
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (item is Items)
            {
                inventory.RemoveItem(item);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if (Image == null)
        {
            Image = GetComponent<Image>();
        }
        if (toolTip == null)
        {
            toolTip = FindObjectOfType<ItemToolTip>();
        }
        AmountText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Items aItem = Item as Items;

        if (aItem != null)
        {
            toolTip.ShowToolTip(aItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.HideToolTip();
    }

    public void InShop()
    {
        AtShop = true;
    }
    public void OutOfShop()
    {
        AtShop = false;
    }
}
