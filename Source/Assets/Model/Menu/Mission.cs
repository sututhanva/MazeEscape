using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission: ScriptableObject
{
    public int id;
    [Range(0, 3)] 
    public int rate;

    public bool isUnlocked;

    public void Init(int id, int rate, bool isUnlocked)
    {
        this.id = id;
        this.rate = rate;
        this.isUnlocked = isUnlocked;
    }
}
