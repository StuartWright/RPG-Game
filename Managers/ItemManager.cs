using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public GameObject[] Items = new GameObject[12];
    private GameObject Loot;
    private Player PlayerRef;
    private SkillManager ManagerRef;
    private int Strength;
    private int Intelligance;
    private int Stamina;
    private int Agility;
    public int WeapPhysicalDamage;
    public int WeapMagicDamage;
    private int RandomItemStats;
    public int minValue, maxValue;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        PlayerRef = GameObject.Find("Player").GetComponent<Player>();
        ManagerRef = GameObject.Find("Canvas").GetComponent<SkillManager>();
    }


    public GameObject RandomItem()
    {
        int RandomNum = Random.Range(0, Items.Length);
        Loot = Items[RandomNum];

        return Loot;
    }
    /*
    private void GenerateStats(int MinValue, int MaxValue)
    {
       
        int RandomNum = Random.Range(0, 100);
        if(RandomNum <= 10)
        {
           // MaxValue = 4;
        }
        Strength = Random.Range(PlayerRef.PlayerLevel - MinValue, PlayerRef.PlayerLevel + MaxValue);
        Intelligance = Random.Range(PlayerRef.PlayerLevel - MinValue, PlayerRef.PlayerLevel + MaxValue);
        Stamina = Random.Range(PlayerRef.PlayerLevel - MinValue, PlayerRef.PlayerLevel + MaxValue);
        Agility = Random.Range(PlayerRef.PlayerLevel - MinValue, PlayerRef.PlayerLevel + MaxValue);
        WeapPhysicalDamage = Random.Range(PlayerRef.PlayerLevel - 5, PlayerRef.PlayerLevel + 15);
        WeapMagicDamage = Random.Range(PlayerRef.PlayerLevel - 5, PlayerRef.PlayerLevel + 15);
        if (Strength <= 0)
            Strength = 0;
        if (Intelligance <= 0)
            Intelligance = 0;
        if (Stamina <= 0)
            Stamina = 0;
        if (Agility <= 0)
            Agility = 0;
        if (WeapPhysicalDamage <= 0)
            WeapPhysicalDamage = 0;
        if (WeapMagicDamage <= 0)
            WeapMagicDamage = 0;
    }
    */
    private void GenerateStats(int MinValue, int MaxValue)
    {

        int RandomNum = Random.Range(0, 100);
        if (RandomNum <= 10)
        {
            // MaxValue = 4;
        }
        Strength = Random.Range(MinValue, MaxValue);
        Intelligance = Random.Range(MinValue, MaxValue);
        Stamina = Random.Range(MinValue, MaxValue);
        Agility = Random.Range(MinValue, MaxValue);
        WeapPhysicalDamage = Random.Range(PlayerRef.PlayerLevel - 5, PlayerRef.PlayerLevel + 15);
        WeapMagicDamage = Random.Range(PlayerRef.PlayerLevel - 5, PlayerRef.PlayerLevel + 15);
        if (Strength <= 0)
            Strength = 0;
        if (Intelligance <= 0)
            Intelligance = 0;
        if (Stamina <= 0)
            Stamina = 0;
        if (Agility <= 0)
            Agility = 0;
        if (WeapPhysicalDamage <= 0)
            WeapPhysicalDamage = 0;
        if (WeapMagicDamage <= 0)
            WeapMagicDamage = 0;
    }
    public Items createItem(Player player, Pickup pickup)
    {
        Items newItem = Instantiate(pickup.item);

        if (pickup.fixedStats == false)
        {
            switch (newItem.EquipmentType)
            {
                case EquipmentType.Weapon:
                    GenerateStats(minValue, maxValue);
                    newItem.Strength = Strength;
                    newItem.Intelligance = Intelligance;
                    newItem.Stamina = Stamina;
                    newItem.Agility = Agility;
                    newItem.PhysicalDamage = WeapPhysicalDamage;
                    newItem.MagicDamage = WeapMagicDamage;
                    break;
                case EquipmentType.Chest:
                    GenerateStats(minValue, maxValue);
                    newItem.Strength = Strength;
                    newItem.Intelligance = Intelligance;
                    newItem.Stamina = Stamina;
                    newItem.Agility = Agility;
                    break;
                case EquipmentType.Hands:
                    GenerateStats(minValue, maxValue);
                    newItem.Strength = Strength;
                    newItem.Intelligance = Intelligance;
                    newItem.Stamina = Stamina;
                    newItem.Agility = Agility;
                    break;
                case EquipmentType.Boots:
                    GenerateStats(minValue, maxValue);
                    newItem.Strength = Strength;
                    newItem.Intelligance = Intelligance;
                    newItem.Stamina = Stamina;
                    newItem.Agility = Agility;
                    break;
                case EquipmentType.Helmet:
                    GenerateStats(minValue, maxValue);
                    newItem.Strength = Strength;
                    newItem.Intelligance = Intelligance;
                    newItem.Stamina = Stamina;
                    newItem.Agility = Agility;
                    break;
            }
        }

        return newItem;
    }

}