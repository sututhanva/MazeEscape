using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Tile = UnityEngine.WSA.Tile;

[CreateAssetMenu(menuName = ("Assets/MazeTiles"))]
public class MazeTiles : ScriptableObject
{
    public TileBase l;
    public TileBase t;
    public TileBase r;
    public TileBase b;
    public TileBase tl;
    public TileBase tr;
    public TileBase rb;
    public TileBase lb;
    public TileBase ltr;
    public TileBase lr;
    public TileBase lbr;
    public TileBase ltb;
    public TileBase tb;
    public TileBase trb;
}
