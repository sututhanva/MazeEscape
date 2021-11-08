using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    public Vector3Int pos;
    public WallState WallState;
    public Cell parent;
    
    public float gCost;
    public float hCost;
    public float fCost => gCost + hCost;

    public Vector3 Center => new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0);
}

[Flags]
public enum WallState
{
    LEFT = 1,
    TOP = 2,
    RIGHT = 4,
    DOWN = 8,
    VISITED = 16,
}
