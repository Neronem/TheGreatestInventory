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
        ShowUIWithSlide(Status.gameObject.GetComponent<RectTransform>(), true);
    }

    public void OpenInventory()
    {
        MainMenu.BtnDisappear();
        Inventory.SetInventory(player);
        ShowUIWithSlide(Inventory.gameObject.GetComponent<RectTransform>(), true);
    }

    // 슬라이드 애니메이션 함수: 활성화/비활성화 포함
    private void ShowUIWithSlide(RectTransform uiRect, bool show)
    {
        float screenWidth = Screen.width;
        float duration = 0.5f;

        if (show)
        {
            uiRect.gameObject.SetActive(true);
            // 오른쪽 밖에서 왼쪽(원위치)으로 슬라이드
            uiRect.anchoredPosition = new Vector2(screenWidth, uiRect.anchoredPosition.y);
            uiRect.DOAnchorPosX(0, duration).SetEase(Ease.OutCubic);
        }
        else
        {
            // 왼쪽에서 오른쪽 밖으로 슬라이드 후 비활성화
            uiRect.DOAnchorPosX(screenWidth, duration).SetEase(Ease.InCubic).OnComplete(() =>
            {
                uiRect.gameObject.SetActive(false);
            });
        }
    }
}
