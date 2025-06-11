using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 인벤토리 UI 관리
/// </summary>
public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab; // 슬롯 UI 프리팹
    [SerializeField] private Transform slotParent; // 레이아웃그룹
    [SerializeField] private List<UISlot> slots;
    
    [SerializeField] private Button returnButton;
    void Reset()
    {
        returnButton = Util.TryGetChildComponent<Button>(this, "Btn_Return");
    }

    private void Start()
    {
        returnButton.onClick.AddListener(() => UIManager.instance.OpenMainMenu(this.gameObject));
    }

    /// <summary>
    /// 인벤토리에 슬롯 생성 후 각 슬롯 아이템데이터 세팅
    /// </summary>
    /// <param name="player"></param>
    public void SetInventory(Character player)
    {
        ClearInventory(); // 기존 슬롯 전부 비활성화

        for (int i = 0; i < player.CharacterInventory.Count; i++)
        {
            UISlot slot;

            // 재사용 가능한 슬롯이 있다면 재사용
            if (i < slots.Count)
            {
                slot = slots[i];
                slot.gameObject.SetActive(true);
            }
            else
            {
                // 부족하면 새로 생성해서 리스트에 추가
                GameObject obj = Instantiate(slotPrefab, slotParent);
                slot = obj.GetComponent<UISlot>();
                slots.Add(slot);
            }

            slot.SetItemData(player.CharacterInventory[i]);
        }

        // 남은 슬롯은 비활성화
        for (int i = player.CharacterInventory.Count; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 인벤토리 슬롯들 전부 끄기 (갱신용)
    /// </summary>
    private void ClearInventory()
    {
        foreach (UISlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}
