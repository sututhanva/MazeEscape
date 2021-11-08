using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [Header("Maze grid")]
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private MazeTiles mazeTiles;
    [SerializeField] private int column;
    [SerializeField] private int row;
    
    [Header("Player")]
    [SerializeField] private Vector3Int start; 
    private Vector3Int end;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 4;
    
    [Header("Environment")]
    [SerializeField] private Transform endPortal;
    
    [Header("Event")]
    [SerializeField] private GameEvent changeMovingEvent;

    private LineRenderer _lineRenderer;
    private Cell[,] maze;
    private Stack<Cell> linePathFinding;
    private Stack<Cell> linePathTemp;
    private bool isMoving = false;
    private int LevelMission;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        Random rand = new System.Random();
        end = new Vector3Int(rand.Next(0, row), rand.Next(0, column), 0);
        int LevelMission = PlayerPrefs.GetInt("LevelMission");
        maze = MazeGeneration.GenerateMaze(column, row, LevelMission);
        player.position = maze[start.x, start.y].Center;
        endPortal.position = maze[end.x, end.y].Center;
        RenderMaze();
    }
    
    public void FindingPath()
    {

        linePathFinding = Pathfinding.PathFinding(maze,start,end, column, row);
        DrawLine();
    }

    public void MovingToEnd()
    {
        linePathTemp = new Stack<Cell>(new Stack<Cell>(linePathFinding));
        changeMovingEvent.Raise();
        StartCoroutine(MovingWithPathFinding());
    }

    IEnumerator MovingWithPathFinding()
    {
        while (linePathTemp.Count > 0)
        {
            Vector3 start = player.transform.position;
            Cell next = linePathTemp.Pop();
            float travelPercent = 0f;
            while (travelPercent < 1)
            {
                travelPercent += Time.deltaTime * speed;
                player.position = Vector3.Lerp(start, next.Center, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }

        SceneManager.LoadScene("MainMenu");
    }

    private void DrawLine()
    {
        Stack<Cell> linePath = new Stack<Cell>(new Stack<Cell>(linePathFinding));
        _lineRenderer.positionCount = linePath.Count;
        _lineRenderer.numCornerVertices = linePath.Count;
        int i = 0;
        while ( linePath.Count > 0)
        {
            Cell cell = linePath.Pop();
            Vector3 renderPos = cell.Center;
            _lineRenderer.SetPosition(i, renderPos);
            i++;
        }
    }

    private void RenderMaze()
    {
        for (int x = 0; x < column; x++)
        {
            for (int y = 0; y < row; y++)
            {
                var cell = maze[x, y];
                cell.WallState &= ~WallState.VISITED;
                var pos = new Vector3Int( x,  y, 0);
                switch ((int)cell.WallState)
                {
                    // one wall case
                    case 1:
                        _tilemap.SetTile(pos, mazeTiles.l);
                        break;
                    case 2:
                        _tilemap.SetTile(pos, mazeTiles.t);
                        break;
                    case 4:
                        _tilemap.SetTile(pos, mazeTiles.r);
                        break;
                    case 8:
                        _tilemap.SetTile(pos, mazeTiles.b);
                        break;
                    
                    // two wall case
                    case 3:
                        _tilemap.SetTile(pos, mazeTiles.tl);
                        break;
                    case 5:
                        _tilemap.SetTile(pos, mazeTiles.lr);
                        break;
                    case 9:
                        _tilemap.SetTile(pos, mazeTiles.lb);
                        break;
                    case 6:
                        _tilemap.SetTile(pos, mazeTiles.tr);
                        break;
                    case 10:
                        _tilemap.SetTile(pos, mazeTiles.tb);
                        break;
                    case 12:
                        _tilemap.SetTile(pos, mazeTiles.rb);
                        break;

                    // three wall case
                    case 7:
                        _tilemap.SetTile(pos, mazeTiles.ltr);
                        break;
                    case 11:
                        _tilemap.SetTile(pos, mazeTiles.ltb);
                        break;
                    case 13:
                        _tilemap.SetTile(pos, mazeTiles.lbr);
                        break;
                    case 14:
                        _tilemap.SetTile(pos, mazeTiles.trb);
                        break;
                }
            }
        }
    }
    
}
