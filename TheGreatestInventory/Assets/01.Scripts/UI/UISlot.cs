using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equip;
    
    [SerializeField] private ItemData itemData;

    private void Reset()
    {
        icon = Util.TryGetChildComponent<Image>(this, "Img_Icon");
        equip = Util.TryFindChild(this, "Img_EquipBg");
    }

    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (itemData == null)
        {
            Debug.Log("ItemData is null");
            return;
        }
        
        icon.sprite = itemData.Icon;
        
        if (itemData.IsEquip)
        {
            equip.SetActive(true);
        }
        else
        {
            equip.SetActive(false);
        }
    }
}
