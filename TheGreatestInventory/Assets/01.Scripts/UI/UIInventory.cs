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

    public void InitInventoryUI(Character player)
    {
        foreach (ItemData itemData in player.CharacterInventory)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            UISlot slot = obj.GetComponent<UISlot>();
            slots.Add(slot);

            slot.SetItemData(itemData);
        }
    }

    private void ClearInventory()
    {
        foreach (UISlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
    }
}
