using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// UI 출력, 전환 매니저
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance; // 싱글톤

    // 각 UI 캔버스들 참조
    [field: SerializeField] public UIMainMenu MainMenu { get; private set; } 
    [field: SerializeField] public UIInventory Inventory { get; private set; } 
    [field: SerializeField] public UIStatus Status { get; private set; }

    // 게임매니저의 플레이어 가져오는 읽기전용 프로퍼티
    private GameManager GM => GameManager.instance;
    private Character player => GM.player;
    
    // Dotween 사용 변수
    [Header("Dotween")]
    // 움직여야할 RectTransform들 참조
    [SerializeField] private RectTransform statusRectTransform; 
    [SerializeField] private RectTransform inventoryRectTransform;
    // 기존 위치 지정
    [SerializeField] private Vector2 statusOriginalPos;
    [SerializeField] private Vector2 inventoryOriginalPos;
    private float screenWidth = Screen.width;
    private float doTweenDuration = 0.5f;
    
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
        
        // UI 기초 세팅
        MainMenu.gameObject.SetActive(true);
        Inventory.gameObject.SetActive(false);
        Status.gameObject.SetActive(false);
    }

    private void Start()
    {
        // 각 UI 위치정보 저장
        statusOriginalPos = statusRectTransform.anchoredPosition;
        inventoryOriginalPos = inventoryRectTransform.anchoredPosition;
    }

    /// <summary>
    /// 돌아가기 버튼에 등록할 메소드
    /// </summary>
    /// <param name="obj"></param>
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
    
    /// <summary>
    /// Dotween으로 우측 -> 좌측으로 이동하는 애니메이션 출력하며 UI 보여줌
    /// </summary>
    /// <param name="rootObject"></param>
    /// <param name="uiRect"></param>
    /// <param name="show"></param>
    /// <param name="originalPos"></param>
    private void ShowUIWithSlide(GameObject rootObject, RectTransform uiRect, bool show, Vector2 originalPos)
    {
        Vector2 hiddenPos = new Vector2(screenWidth, originalPos.y); // 우측 위치정보

        if (show) // UI 보여주기라면
        {
            rootObject.SetActive(true); // UI 활성화
            uiRect.anchoredPosition = hiddenPos; // 숨김 위치에서 시작
            uiRect.DOAnchorPos(originalPos, doTweenDuration) // 기존 위치로 슬라이드
                .SetEase(Ease.OutCubic);
        }
        else // UI 숨기기라면
        {
            uiRect.DOAnchorPos(hiddenPos, doTweenDuration) // 우측 위치로 슬라이드
                .SetEase(Ease.InCubic)
                .OnComplete(() =>
                {
                    rootObject.SetActive(false); // 애니메이션 끝나면 UI 비활성화
                    MainMenu.BtnAppear(); // 버튼 활성화
                });
        }
    }


}


