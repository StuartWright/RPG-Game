using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool fixedStats;
    public Items item;
    private Inventory InventoryRef;
    private Inventory PotionInventoryRef;
    /*
    private void Start()
    {
        InventoryRef = GameObject.Find("ItemSlotGrid").GetComponent<Inventory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Pickup pickup = other.GetComponent<Pickup>();

            Items newItem = ItemManager.instance.createItem(other.GetComponent<Player>(), pickup);
            if (newItem.IsPotion)
                PotionInventoryRef.AddPotion(newItem);
            else
                InventoryRef.AddItem(newItem);
            // InventoryRef.AddItem(other.gameObject, other.GetComponent<Items>());
            Destroy(other.gameObject);
        }
       
    }
    */
}
