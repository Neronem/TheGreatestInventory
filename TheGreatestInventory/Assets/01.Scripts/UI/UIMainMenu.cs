using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterLevel;
    [SerializeField] private TextMeshProUGUI characterDescription;
    
    [SerializeField] private TextMeshProUGUI characterGold;
    
    [SerializeField] private Image levelInfoImage;
    [SerializeField] private TextMeshProUGUI levelProcess;
    
    [SerializeField] private Button statusButton;
    [SerializeField] private Button inventoryButton;
    
    // Start is called before the first frame update
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

    public void SetUIMainMenu(Character character)
    {
        characterName.text = character.Name;
        characterLevel.text = character.Level.ToString();
        characterDescription.text = character.Description;
        characterGold.text = character.Gold.ToString();
        levelProcess.text = $"{character.Exp} / {character.MaxExp}";
        levelInfoImage.fillAmount = character.Exp / character.MaxExp;
    }

    public void ButtonAnimation(Transform _transform, Action onComplete = null)
    {
        _transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutBack).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
