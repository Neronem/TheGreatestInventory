using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    void Reset()
    {
        returnButton = Util.TryGetChildComponent<Button>(this, "Btn_Return");
    }

    private void Start()
    {
        returnButton.onClick.AddListener(() => UIManager.instance.OpenMainMenu(this.gameObject));
    }
}
