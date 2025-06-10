using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [field: SerializeField] public UIMainMenu MainMenu { get; private set; }
    [field: SerializeField] public UIInventory Inventory { get; private set; }
    [field: SerializeField] public UIStatus Status { get; private set; }

    private GameManager GM => GameManager.instance;
    private Character player => GM.player;

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

    void Reset()
    {
        MainMenu = FindObjectOfType<UIMainMenu>(true);
        Inventory = FindObjectOfType<UIInventory>(true);
        Status = FindObjectOfType<UIStatus>(true);

        MainMenu.gameObject.SetActive(true);
        Inventory.gameObject.SetActive(false);
        Status.gameObject.SetActive(false);
    }

    public void OpenMainMenu(GameObject obj)
    {
        obj.SetActive(false);
        MainMenu.BtnAppear();
    }

    public void OpenStatusMenu()
    {
        MainMenu.BtnDisappear();
        Status.SetUIStatus(player);
        Status.gameObject.SetActive(true);
    }

    public void OpenInventory()
    {
        MainMenu.BtnDisappear();
        Inventory.SetInventory(player);
        Inventory.gameObject.SetActive(true);
    }
}
