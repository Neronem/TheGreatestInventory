using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
    [field: SerializeField] public int MaxExp { get; private set; }
    [field: SerializeField] public int MaxHp { get; private set; }
    [field: SerializeField] public int CurrentHp { get; private set; }
    [field: SerializeField] public int Attack { get; private set; }
    [field: SerializeField] public int Defense { get; private set; }
    [field: SerializeField] public int Gold { get; private set; }
    [field: SerializeField] public int Critical { get; private set; }

    public Character(string name, int level, string description, int maxExp, int maxHp, int attack, int defense, int gold, int critical)
    {
        Name = name;
        Level = level;
        Description = description;
        Exp = 0;
        MaxExp = maxExp;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Attack = attack;
        Defense = defense;
        Gold = gold;
        Critical = critical;
    }
}