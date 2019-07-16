using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


    public class MercInventory : MonoBehaviour
    {
        [SerializeField] List<Items> Items;
        [SerializeField] Transform ItemsParent;
        [SerializeField] MercItemSlot[] ItemSlots;

        public event Action<Items> OnItemRightClickEvent;

        private void Start()
        {
            RefreshUI();
            for (int i = 0; i < ItemSlots.Length; i++)
            {
                ItemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
            }
        }


        private void OnValidate()
        {
            if (ItemsParent != null)
            {
                ItemSlots = ItemsParent.GetComponentsInChildren<MercItemSlot>();
            }

            RefreshUI();
        }

        public void RefreshUI()
        {
            int i = 0;
            for (; i < Items.Count && i < ItemSlots.Length; i++)
            {
                ItemSlots[i].Item = Items[i];
            }
            for (; i < ItemSlots.Length; i++)
            {
                ItemSlots[i].Item = null;
            }
        }

        public bool AddItem(Items item)
        {
            if (IsFull())
            {
                return false;
            }

            Items.Add(item);
            RefreshUI();
            return true;
        }

        public bool IsFull()
        {
            return Items.Count >= ItemSlots.Length;
        }

        public bool RemoveItem(Items item)
        {
            if (Items.Remove(item))
            {
                RefreshUI();
                return true;
            }
            return false;
        }
    }


