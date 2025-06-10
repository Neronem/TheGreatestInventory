using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI attackInfo;
    [SerializeField] private TextMeshProUGUI defenseInfo;
    [SerializeField] private TextMeshProUGUI healthInfo;
    [SerializeField] private TextMeshProUGUI criticalInfo;

    [SerializeField] private Button returnButton;
    
    // Start is called before the first frame update
    void Reset()
    {
        attackInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_AttackInfo");
        defenseInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_DefenseInfo");
        healthInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_HealthInfo");
        criticalInfo = Util.TryGetChildComponent<TextMeshProUGUI>(this, "Tmp_CriticalInfo");

        returnButton = Util.TryGetChildComponent<Button>(this, "Btn_Return");
    }

    private void Start()
    {
        returnButton.onClick.AddListener(() => UIManager.instance.OpenMainMenu(this.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIStatus(Character character)
    {
        character.RefreshCharacterStatus();
        
        attackInfo.text = character.Attack.ToString();
        defenseInfo.text = character.Defense.ToString();
        healthInfo.text = character.Health.ToString();
        criticalInfo.text = character.Critical.ToString();
    }
}
