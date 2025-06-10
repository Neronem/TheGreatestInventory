using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
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

    public void SetInventory(Character player)
    {
        ClearInventory(); // 기존 슬롯 전부 비활성화만

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

    private void ClearInventory()
    {
        foreach (UISlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }
}
