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
    
    private float screenWidth = Screen.width;
    private float doTweenDuration = 0.5f;
    
    [SerializeField] private RectTransform statusRectTransform;
    [SerializeField] private RectTransform inventoryRectTransform;

    [SerializeField] private Vector2 statusOriginalPos;
    [SerializeField] private Vector2 inventoryOriginalPos;
    
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

        inventoryRectTransform = Util.TryGetChildComponent<RectTransform>(Inventory, "Group_Inventory");
        statusRectTransform = Util.TryGetChildComponent<RectTransform>(Status, "Group_Status");
        
        statusOriginalPos = statusRectTransform.anchoredPosition;
        inventoryOriginalPos = inventoryRectTransform.anchoredPosition;
        
        MainMenu.gameObject.SetActive(true);
        Inventory.gameObject.SetActive(false);
        Status.gameObject.SetActive(false);
    }

    public void OpenMainMenu(GameObject obj)
    {
        if (obj == Status.gameObject)
        {
            ShowUIWithSlide(obj, statusRectTransform, false, statusOriginalPos);
        }
        
        if (obj == Inventory.gameObject)
        {
            ShowUIWithSlide(obj, inventoryRectTransform, false, inventoryOriginalPos);
        }
    }

    public void OpenStatusMenu()
    {
        MainMenu.BtnDisappear();
        Status.SetUIStatus(player);
        
        ShowUIWithSlide(Status.gameObject, statusRectTransform, true, statusOriginalPos);
    }

    public void OpenInventory()
    {
        MainMenu.BtnDisappear();
        Inventory.SetInventory(player);
        
        ShowUIWithSlide(Inventory.gameObject, inventoryRectTransform, true, inventoryOriginalPos);
    }
    
    private void ShowUIWithSlide(GameObject rootObject, RectTransform uiRect, bool show, Vector2 originalPos)
    {
        Vector2 hiddenPos = new Vector2(screenWidth, originalPos.y);

        if (show)
        {
            rootObject.SetActive(true);
            uiRect.anchoredPosition = hiddenPos; // 숨김 위치에서 시작
            uiRect.DOAnchorPos(originalPos, doTweenDuration)
                .SetEase(Ease.OutCubic);
        }
        else
        {
            uiRect.DOAnchorPos(hiddenPos, doTweenDuration)
                .SetEase(Ease.InCubic)
                .OnComplete(() =>
                {
                    rootObject.SetActive(false);
                    MainMenu.BtnAppear();
                });
        }
    }


}


