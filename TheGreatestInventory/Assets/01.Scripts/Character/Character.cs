using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터 정보 클래스
/// </summary>
[System.Serializable]
public class Character
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
    [field: SerializeField] public int MaxExp { get; private set; }
    [field: SerializeField] public int Gold { get; private set; }
    
    // 캐릭터 기본 능력치
    [field: SerializeField] public int BaseHealth { get; private set; }
    [field: SerializeField] public int BaseAttack { get; private set; }
    [field: SerializeField] public int BaseDefense { get; private set; }
    [field: SerializeField] public int BaseCritical { get; private set; }

    // 아이템 능력치를 추가한 캐릭터 능력치
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

    /// <summary>
    /// 캐릭터 인벤토리에 아이템 추가
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemData item)
    {
        CharacterInventory.Add(item);
    }

    /// <summary>
    /// 원하는 아이템 장착 후, 캐릭터 능력치 갱신 및 인벤토리 갱신
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="isEquip"></param>
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

    /// <summary>
    /// 캐릭터 능력치 갱신
    /// </summary>
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