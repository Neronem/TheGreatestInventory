using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [field:SerializeField] public UIMainMenu MainMenu { get; private set; }  
    [field:SerializeField] public UIInventory Inventory { get; private set; }
    [field:SerializeField] public UIStatus Status { get; private set; }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Reset()
    {
        MainMenu = FindObjectOfType<UIMainMenu>(true);
        Inventory = FindObjectOfType<UIInventory>(true);
        Status = FindObjectOfType<UIStatus>(true);
        
        MainMenu.gameObject.SetActive(true);
        Inventory.gameObject.SetActive(false);
        Status.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMainMenu(GameObject obj)
    {
        obj.SetActive(false);
        MainMenu.BtnAppear();
    }

    public void OpenStatusMenu()
    {
        MainMenu.BtnDisappear();
        Status.gameObject.SetActive(true);
    }
    
    public void OpenInventory()
    {
        MainMenu.BtnDisappear();
        Inventory.gameObject.SetActive(true);
    }

    public void InitInventory(Character player)
    {
        Inventory.InitInventoryUI(player);
    }
}
