using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
 HealthPotion,
 ManaPotioin,
 StaminaPotion,
 Equipment
 
}

[CreateAssetMenu]
public class Items : ScriptableObject
{
    public bool CanStack;
    public bool Equipped = false;
    public bool IsPotion = false;

    public Player player;
    public Sprite ItemIcon;
    public Inventory PotionBar;
    public ItemType type;
    public EquipmentType EquipmentType;

    public int AmountToAdd;
    public int Strength;
    public int Intelligance;
    public int Stamina;
    public int Agility;
    public int PhysicalDamage;
    public int MagicDamage;
    //[Range(0, 999)]
    public int StackAmount = 1;
    public int ItemPrice;

    public string ItemName;
    public string ItemDescription;

    public void Use(Items item)
    {
        
        switch (type)
        {
            case ItemType.HealthPotion:
                if(StackAmount >0 && player.Health < player.MaxHealth)
                {
                    player.Health += AmountToAdd;
                    UIManager.instance.UpdatePlayerBars();
                    PotionBar.RemovePotion(this);
                }
                break;
            case ItemType.ManaPotioin:
                if (StackAmount > 0 && player.Mana < player.MaxMana)
                {
                    player.Mana += AmountToAdd;
                    UIManager.instance.UpdatePlayerBars();
                    PotionBar.RemovePotion(this);
                }
                break;
            case ItemType.StaminaPotion:
                if (StackAmount > 0 && player.Stamina < player.MaxStamina)
                {
                    player.Stamina += AmountToAdd;
                    UIManager.instance.UpdatePlayerBars();
                    PotionBar.RemovePotion(this);
                }
                break;
            default:
                
                break;
        }
       
    }
}
