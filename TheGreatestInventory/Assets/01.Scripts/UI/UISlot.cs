using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Button button;
    
    [SerializeField] private Image icon;
    [SerializeField] private GameObject equip;
    
    [SerializeField] private ItemData itemData;

    private void Reset()
    {
        icon = Util.TryGetChildComponent<Image>(this, "Img_Icon");
        equip = Util.TryFindChild(this, "Img_EquipBg");
        button = GetComponent<Button>();
    }

    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        button.onClick.RemoveAllListeners(); // 기존 리스너 제거
        button.onClick.AddListener(() => GameManager.instance.player.EquipOrUnEquipItem(itemData.ItemName, true)); // 새로 등록
        
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
