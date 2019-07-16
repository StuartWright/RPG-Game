using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

    public class MercItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Items> OnRightClickEvent;
        public Image Image;
        private Items item;
        [SerializeField] ItemToolTip toolTip;
        public MercInventory inventory;
        public Inventory PlayerInventory;
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

    public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
            {
                if (item is Items)
                {
                if(SkillManager.Instance.Money >= item.ItemPrice)
                {
                    PlayerInventory.AddItem(item);
                    SkillManager.Instance.Money -= item.ItemPrice;
                    inventory.RemoveItem(item);
                    UIManager.instance.UpdateMoney();
                } 
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

    }


