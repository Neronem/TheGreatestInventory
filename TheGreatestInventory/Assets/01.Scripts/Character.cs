using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
    [field: SerializeField] public int MaxExp { get; private set; }
    [field: SerializeField] public int Gold { get; private set; }
    [field: SerializeField] public int BaseHealth { get; private set; }
    [field: SerializeField] public int BaseAttack { get; private set; }
    [field: SerializeField] public int BaseDefense { get; private set; }
    [field: SerializeField] public int BaseCritical { get; private set; }

    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Critical { get; private set; }

    [field: SerializeField] public List<ItemData> CharacterInventory { get; private set; }

    public Character(string name, int level, string description, int maxExp, int health, int attack, int defense,
        int gold, int critical, List<ItemData> characterInventory)
    {
        Name = name;
        Level = level;
        Description = description;
        Exp = 0;
        MaxExp = maxExp;
        BaseHealth = health;
        BaseAttack = attack;
        BaseDefense = defense;
        BaseCritical = critical;
        Gold = gold;
        CharacterInventory = characterInventory;
        
        Health = BaseHealth;
        Attack = BaseAttack;
        Defense = BaseDefense;
        Critical = BaseCritical;
    }

    public void AddItem(ItemData item)
    {
        CharacterInventory.Add(item);
    }

    public void EquipOrUnEquipItem(string itemName, bool isEquip)
    {
        ItemData targetItem = CharacterInventory.Find(item => item.ItemName == itemName);

        if (targetItem == null)
        {
            Debug.LogWarning($"아이템 '{itemName}' 인벤토리에 없음!");
            return;
        }
        
        if (isEquip)
        {
            foreach (ItemData itemData in CharacterInventory)
            {
                itemData.UnEquipItem();
            }
            targetItem.EquipItem();
        }
        else
        {
            targetItem.UnEquipItem();
        }
        
        UIManager.instance.Inventory.SetInventory(this);
        RefreshCharacterStatus();
    }

    public void RefreshCharacterStatus()
    {
        Health = BaseHealth;
        Attack = BaseAttack;
        Defense = BaseDefense;
        Critical = BaseCritical;

        foreach (ItemData item in CharacterInventory)
        {
            if (item.IsEquip)
            {
                Health += item.Health;
                Attack += item.Attack;
                Defense += item.Defense;
                Critical += item.Critical;
            }
        }
    }

}