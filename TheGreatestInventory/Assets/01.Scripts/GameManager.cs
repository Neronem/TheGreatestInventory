using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Character character;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            character = new Character("낭만용사멋쟁이", 1, "얼큰오이티라미수 \nvs \n제티마라탕", 10, 100, 5, 5, 100, 10);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    
    
}
