using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 슬롯의 동작을 정의함
/// </summary>
public class UISlot : MonoBehaviour
{
    [SerializeField] private Button button; // 누르면 장착
    
    [SerializeField] private Image icon; // 아이템 데이터의 이미지 출력
    [SerializeField] private GameObject equip; // 장착중인지 표시
    
    [SerializeField] private ItemData itemData; // 슬롯마다 보관하는 아이템 데이터

    private void Reset()
    {
        icon = Util.TryGetChildComponent<Image>(this, "Img_Icon");
        equip = Util.TryFindChild(this, "Img_EquipBg");
        button = GetComponent<Button>();
    }

    /// <summary>
    /// 슬롯에 아이템데이터를 넣고 UI를 갱신함
    /// </summary>
    /// <param name="itemData"></param>
    public void SetItemData(ItemData itemData)
    {
        this.itemData = itemData;
        button.onClick.RemoveAllListeners(); // 기존 리스너 제거
        
        bool toggleEquip = !itemData.IsEquip; // 이미 장착되어 있으면 해제
        button.onClick.AddListener(() => GameManager.instance.player.EquipOrUnEquipItem(itemData.ItemName, toggleEquip)); // 아이템 장착 또는 해제 이벤트 등록
        
        RefreshUI();
    }
    
    /// <summary>
    /// 슬롯 UI 갱신
    /// </summary>
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
