using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour {

    [SerializeField] Transform EquipmentSlotsParent;
    [SerializeField] EquipmentSlots[] EquipmentSlots;

    public event Action<Items> OnItemRightClickEvent;

    private void Start()
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i].OnRightClickEvent += OnItemRightClickEvent;
        }
    }

    private void OnValidate()
    {
        EquipmentSlots = EquipmentSlotsParent.GetComponentsInChildren<EquipmentSlots>();
    }

    public bool AddItem(EquippableItems item, out EquippableItems PreviousItem)
    {
        for(int i = 0; i < EquipmentSlots.Length; i++)
        {
            if(EquipmentSlots[i].EquipmentType == item.EquipmentType)
            {
                PreviousItem = (EquippableItems)EquipmentSlots[i].Item;
                EquipmentSlots[i].Item = item;
                return true;
            }
        }
        PreviousItem = null;
        return false;
    }

    public bool RemoveItem(EquippableItems item)
    {
        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            if (EquipmentSlots[i].Item == item)
            {
                EquipmentSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
}
