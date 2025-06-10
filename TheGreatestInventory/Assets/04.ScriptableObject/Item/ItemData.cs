using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    [field:SerializeField] public string ItemName { get; private set; }
    [field:SerializeField] public Sprite Icon { get; private set; }
    [field:SerializeField] public bool IsEquip { get; private set; }

    public void EquipItem()
    {
        IsEquip = true;
    }

    public void UnEquipItem()
    {
        IsEquip = false;
    }
}
