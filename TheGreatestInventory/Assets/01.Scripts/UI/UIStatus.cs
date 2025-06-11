using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Status UI 관리
/// </summary>
public class UIStatus : MonoBehaviour
{
    // 플레이어 능력치 출력할 텍스트들
    [Header("Informations")]
    [SerializeField] private TextMeshProUGUI attackInfo;
    [SerializeField] private TextMeshProUGUI defenseInfo;
    [SerializeField] private TextMeshProUGUI healthInfo;
    [SerializeField] private TextMeshProUGUI criticalInfo;

    [SerializeField] private Button returnButton; // 돌아가기 버튼
    
    void Reset()
    {
        attackInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_AttackInfo");
        defenseInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_DefenseInfo");
        healthInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_HealthInfo");
        criticalInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_CriticalInfo");

        returnButton = Util.TryGetChildComponent<Button>(this, "Btn_Return");
    }

    private void Start()
    { // 돌아가기 버튼에 OpenMainMenu 메소드 등록
        returnButton.onClick.AddListener(() => UIManager.instance.OpenMainMenu(this.gameObject));
    }

    /// <summary>
    /// Character 정보 가져와서 UI에 세팅
    /// </summary>
    /// <param name="character"></param>
    public void SetUIStatus(Character character)
    {
        character.RefreshCharacterStatus();
        
        attackInfo.text = character.Attack.ToString();
        defenseInfo.text = character.Defense.ToString();
        healthInfo.text = character.Health.ToString();
        criticalInfo.text = character.Critical.ToString();
    }
}
