using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelUI : MonoBehaviour
{
    

    private static PanelUI instance;
    public static PanelUI Instance
    {
        get
        {
            return instance;
        }
    }

  //  public Text EquipmentStrText;
   // public Text EquipmentIntText;
   // public Text EquipmentStamText;
   // public Text EquipmentAgiText;
   // public Text EquipmentDamageText;

    public int EquipmentStr;
    public int EquipmentInt;
    public int EquipmentStam;
    public int EquipmentAgi;
    public int EquipmentDamage;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }


    public void UpdateStats()
    {
      //  EquipmentStrText.text = "Strength " + EquipmentStr;
      //  EquipmentIntText.text = "Intelligance " + EquipmentInt;
      //  EquipmentStamText.text = "Stamina " + EquipmentStam;
      //  EquipmentAgiText.text = "Agility " + EquipmentAgi;
       // EquipmentDamageText.text = "Damage " + EquipmentDamage;
    }
    

}
