using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Inventory : MonoBehaviour
{
    public delegate void UpdateStackNumber();
    public event UpdateStackNumber CheckStackNum;

    public List<Items> Items;
    [SerializeField] Transform ItemsParent;
    public ItemSlot[] ItemSlots;

    //[SerializeField] List<Items> Potions;
   // [SerializeField] PotionSlot[] PotionSlots;
    [SerializeField] bool PotionInventory;
    public event Action<Items> OnItemRightClickEvent;
    private int i = 0;
 
    private void Start()
    {
        Player PlayerRef = GameObject.Find("Player").GetComponent<Player>();
 
        RefreshUI();
        for(int i = 0; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
            if(ItemSlots[i].Item != null)
                if(this.PotionInventory)
            ItemSlots[i].Item.StackAmount = 2;
        }
        if(PotionInventory)
        {
            for(int i = 0; i < ItemSlots.Length; i++)
            {
                ItemSlots[i].Item.player = PlayerRef;
                ItemSlots[i].Item.PotionBar = this;
            }
        }
        RefreshUI();
    }


    private void OnValidate()
    {
        if(ItemsParent != null)
        {
            ItemSlots = ItemsParent.GetComponentsInChildren<ItemSlot>();
        }
        RefreshUI();
    }

    public void RefreshUI()
    {
        int i = 0;
        for(; i < Items.Count && i < ItemSlots.Length; i++)
        {
            ItemSlots[i].Item = Items[i];
            CheckStackNum += ItemSlots[i].CheckItemStack;
            CheckStackNum();
        }
        for (; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].Item = null;
        }
    }

    public bool AddItem(Items item)
    {
        
       // if (Items.Count != 0 || item.CanStack)
        if (item.CanStack)
        {
            for (i = 0; i < Items.Count; i++)
            {
                if (item.ItemName == Items[i].ItemName)
                {
                    Items[i].StackAmount++;
                    return true;
                }
                else
                    Items.Add(item);

            }
                
            }
      else
        {
            Items.Add(item);
        }

        if (IsFull())
        {
            return false;
        } 
        RefreshUI();
        return true;
    }

    public bool IsFull()
    {
        return Items.Count >= ItemSlots.Length;
    }

    public bool RemoveItem(Items item)
    {
        item.StackAmount--;
        RefreshUI();
        if (item.StackAmount < 0)
        {
            Items.Remove(item);
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool RemovePotion(Items item)
    {
        item.StackAmount--;
        RefreshUI();
        
        return true;
    }

    public bool AddPotion(Items item)
    {
        for (i = 0; i < Items.Count; i++)
        {
            if (item.ItemName == Items[i].ItemName)
            {
                Items[i].StackAmount++;
                RefreshUI();
                return true;
            }

        }
        RefreshUI();
        return true;
    }
}
