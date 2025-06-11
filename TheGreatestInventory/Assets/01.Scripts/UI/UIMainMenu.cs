using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MainMenu UI 관리
/// </summary>
public class UIMainMenu : MonoBehaviour
{
    // 캐릭터 정보 참조
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterLevel;
    [SerializeField] private TextMeshProUGUI characterDescription;
    [SerializeField] private TextMeshProUGUI characterGold;
    
    // 레벨 진행도 표시
    [SerializeField] private Image levelInfoImage;
    [SerializeField] private TextMeshProUGUI levelProcess;
    
    // 각 UI 진입 버튼
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    
    private bool isAnimating = false; 
    
    void Reset()
    {
        characterName = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_CharacterName");
        characterLevel = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_Level");
        characterDescription = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_Description");
        
        characterGold = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_Gold");
        
        levelInfoImage = Util.TryGetChildComponent<Image>(this, "Img_LevelInfo");
        levelProcess = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_LevelProcess");
        
        statusButton = Util.TryGetChildComponent<Button>(this, "Btn_Status");
        inventoryButton = Util.TryGetChildComponent<Button>(this, "Btn_Inventory");
    }

    private void Start()
    {
        // 각 UI 진입 버튼에 이벤트 등록
        statusButton.onClick.AddListener(() =>
        {
            ButtonAnimation(statusButton.transform, () => UIManager.instance.OpenStatusMenu());
        });
        
        inventoryButton.onClick.AddListener(() =>
        {
            ButtonAnimation(inventoryButton.transform, () => UIManager.instance.OpenInventory());
        });
    }

    public void BtnAppear()
    {
        statusButton.gameObject.SetActive(true);
        inventoryButton.gameObject.SetActive(true);
    }
    
    public void BtnDisappear()
    {
        statusButton.gameObject.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// 메인메뉴 출력 정보 갱신
    /// </summary>
    /// <param name="character"></param>
    public void SetUIMainMenu(Character character)
    {
        characterName.text = character.Name;
        characterLevel.text = character.Level.ToString();
        characterDescription.text = character.Description;
        characterGold.text = character.Gold.ToString();
        levelProcess.text = $"{character.Exp} / {character.MaxExp}";
        levelInfoImage.fillAmount = character.Exp / character.MaxExp;
    }

    /// <summary>
    /// 버튼 누를 시 애니메이션 출력, 그후 이벤트 실행
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="onComplete"></param>
    public void ButtonAnimation(Transform _transform, Action onComplete = null)
    {
        if (isAnimating) return; // 중복실행 방지
        isAnimating = true;

        _transform.DOKill();
        _transform.DOScale(1.2f, 0.2f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                isAnimating = false;
                onComplete?.Invoke();
            });
    }
}
