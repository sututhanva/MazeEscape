using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public static class Pathfinding
{
    public static Stack<Cell> PathFinding(Cell[,] mazes, Vector3Int start, Vector3Int end, int column, int rows)
    {
        Cell startCell = mazes[start.x, start.y];
        Cell endCell = mazes[end.x, end.y];
        List<Cell> openList = new List<Cell>();
        Stack<Cell> closeStack = new Stack<Cell>();
        Stack<Cell> pathFinding = new Stack<Cell>();
        openList.Add(startCell);
        while (openList.Count > 0)
        {
            var current = openList[0];
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].fCost < current.fCost || openList[i].fCost == current.fCost)
                {
                    if (openList[i].hCost < current.hCost)
                        current = openList[i];
                }
            }

            openList.Remove(current);
            closeStack.Push(current);
            if (current == endCell)
            {
                pathFinding = GetFindingPath(startCell, endCell);
                Debug.Log(pathFinding.Count);
                return pathFinding;
            };
            List<Cell> neighbours = GetNeighbours(current, mazes, column, rows);
            foreach (var neighbour in neighbours)
            {
                if (closeStack.Contains(neighbour))
                {
                    continue;
                }

                float gcost = current.gCost + DistancePos(current.pos, neighbour.pos);
                if (gcost < neighbour.gCost || !openList.Contains(neighbour))
                {
                    neighbour.gCost = gcost;
                    neighbour.hCost = DistancePos(neighbour.pos, endCell.pos);
                    neighbour.parent = current;
                    if(!openList.Contains(neighbour))
                        openList.Add(neighbour);
                }
            }
        }

        return null;
    }

    public static Stack<Cell> GetFindingPath(Cell startCell, Cell endCell)
    {
        Stack<Cell> path = new Stack<Cell>();
        Cell currentCell = endCell;

        while (currentCell != startCell)
        {
            path.Push(currentCell);
            currentCell = currentCell.parent;
        }
        path.Push(currentCell);

        return path;
    }

    public static List<Cell> GetNeighbours(Cell cell, Cell[,] maze, int column, int row)
    {
        List<Cell> list = new List<Cell>();
        WallState wayState = ~cell.WallState;
        Vector3Int p = cell.pos;
        if (p.x < column - 1)
        {
            if (wayState.HasFlag(WallState.RIGHT))
            {
                list.Add(maze[p.x + 1, p.y]);
            }
        }

        if (p.y > 0)
        {
            if (wayState.HasFlag(WallState.DOWN))
            {
                list.Add(maze[p.x, p.y - 1]);
            }
        }

        if (p.x > 0)
        {
            if (wayState.HasFlag(WallState.LEFT))
            {
                list.Add(maze[p.x - 1, p.y]);
            }
        }

        if (p.y < row - 1)
        {
            if (wayState.HasFlag(WallState.TOP))
            {
                list.Add(maze[p.x, p.y + 1]);
            }
        }

        return list;
    }

    public static float DistancePos(Vector3Int startPos, Vector3Int endPos)
    {
        Vector3 currentToNeighbour = endPos - startPos;
        float distance = Vector3.Magnitude(currentToNeighbour);
        return distance;
    }
}