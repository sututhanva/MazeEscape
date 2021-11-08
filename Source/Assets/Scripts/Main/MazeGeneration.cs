using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;
using Tile = UnityEngine.WSA.Tile;


public class Neighbour
{
    public Vector3Int pos;
    public WallState sharedWall;

    public Neighbour(Vector3Int pos, WallState sharedWall)
    {
        this.pos = pos;
        this.sharedWall = sharedWall;
    }
}

public static class MazeGeneration
{
    public static Cell[,] GenerateMaze(int column, int row, int seed)
    {
        Cell[,] maze = new Cell[column,row];
        WallState fullState = WallState.LEFT | WallState.TOP | WallState.RIGHT | WallState.DOWN;
        for (int x = 0; x < column; x++)
        {
            for (int y = 0; y < row; y++)
            {
                maze[x, y] = new Cell();
                maze[x, y].pos = new Vector3Int(x, y, 0);
                maze[x, y].WallState = fullState;
            }
        }

        maze = GenerateWayToMaze(maze, column, row, seed);
        return maze;
    }

    public static Cell[,] GenerateWayToMaze(Cell[,] mazes, int width,int height, int seed)
    {
        Random random = new System.Random(seed);
        Stack<Vector3Int> posStack = new Stack<Vector3Int>();
        int x = random.Next(0, width);
        int y = random.Next(0, height);
        Vector3Int pos = new Vector3Int(x, y, 0);
        mazes[x, y].WallState |= WallState.VISITED;
        posStack.Push(pos);

        while (posStack.Count > 0)
        {
            Vector3Int current = posStack.Pop();
            List<Neighbour> neighbours = GetUnvisitedNeighbours(current, mazes, width, height);

            if (neighbours.Count > 0)
            {
                posStack.Push(current);
                var randIndex = random.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var nPosition = randomNeighbour.pos;
                mazes[current.x, current.y].WallState &= ~randomNeighbour.sharedWall;
                mazes[nPosition.x, nPosition.y].WallState &= ~GetOppositeWall(randomNeighbour.sharedWall);
                mazes[nPosition.x, nPosition.y].WallState |= WallState.VISITED;
                posStack.Push(nPosition);
            }

        }

        return mazes;
    }

    public static List<Neighbour> GetUnvisitedNeighbours(Vector3Int p, Cell[,] maze, int width, int height)
    {
        List<Neighbour> list = new List<Neighbour>();
        if (p.x > 0) // left
        {
            if (!maze[p.x - 1, p.y].WallState.HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour(new Vector3Int(p.x-1,p.y,0), WallState.LEFT));
            }
        }
        if (p.y < height - 1) // top
        {
            if (!maze[p.x, p.y+1].WallState.HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour(new Vector3Int(p.x,p.y+1,0), WallState.TOP));
            }
        }
        if (p.x < width - 1) // right
        {
            if (!maze[p.x+1, p.y].WallState.HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour(new Vector3Int(p.x+1,p.y,0), WallState.RIGHT));
            }
        }
        if (p.y > 0) // bot
        {
            if (!maze[p.x, p.y-1].WallState.HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour(new Vector3Int(p.x,p.y-1,0), WallState.DOWN));
            }
        }

        return list;
    }

    public static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.TOP: return WallState.DOWN;
            case WallState.DOWN: return WallState.TOP;
            default: return WallState.LEFT;
        }
    }
}
